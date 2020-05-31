using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using simpleGIS.Properties;

namespace simpleGIS
{
    /// <summary>
    /// 地图控件类
    /// </summary>
    public partial class MapControl : UserControl
    {
        #region 字段
        private Map map;                        // 地图对象
        private OperationType mapOperation;     // 当前地图操作类型
        private SelectedMode selectedmode;      // 地图选择对象的模式
        private bool needRefresh = false;       // 是否需要更新cache
        private bool needSave = false;          // 是否需要保存数据
        private Bitmap cache;   // 当前地图显示范围的缓冲图片，不包括跟踪层和已选对象层
        private int doubleSelectedItem = -1;    // 双重选择的对象（在要素选择结果窗口内再选择）

        // 编辑相关
        private List<PointD> trackingPoints;    // Track模式新绘制的节点集合(地图坐标)
        private bool isHole;                    // 标记trackingPoints是否为多多边形的洞
        private Geometry[] editGeometries;      // Edit模式正在编辑的几何体，[0]为节点，[1]为线或面，[2]为多线面
        private int addVertexLoc = -1;          // 编辑模式插入节点时，插入的位置

        // 鼠标相关
        private PointF mouseLoc = new PointF();         // 记录鼠标当前位置
        private PointF mouseDownLoc = new PointF();     // 鼠标左键按下时的位置
        private PointF mouseRDownLoc = new PointF();    // 鼠标右键按下位置
        private readonly Cursor zoomInCursor = new Cursor(Resources.ZoomIn.Handle);
        private readonly Cursor zoomOutCursor = new Cursor(Resources.ZoomOut.Handle);
        private readonly Cursor panCursor = new Cursor(Resources.PanUp.Handle);
        private readonly Cursor crossCursor = new Cursor(Resources.Cross.Handle);

        // 常量
        private const double ZoomRatio = 1.2;   // 缩放系数
        private const float AttractRadius = 5;  // 吸引区半径
        #endregion

        public MapControl()
        {
            cache = new Bitmap(Width, Height);
            InitializeComponent();

            Graphics g = Graphics.FromHwnd(Handle);
            map = new Map(g);
            g.Dispose();
            selectedmode = SelectedMode.New;
            mapOperation = OperationType.None;
            trackingPoints = new List<PointD>();
            editGeometries = new Geometry[3];
            MouseWheel += MapControl_MouseWheel;
        }

        #region 属性

        /// <summary>
        /// 获取控件的地图对象
        /// </summary>
        public Map Map { get => map; }

        /// <summary>
        /// 获取或设置控件的当前工具（操作类型）
        /// </summary>
        public OperationType OperationType
        {
            get => mapOperation;
            set
            {
                if (mapOperation != value)
                {
                    if (value == OperationType.Edit || value == OperationType.Track)
                    {
                        if (map.SelectedLayer == -1 || map.Layers[map.SelectedLayer].IsEdit == false)
                        { throw new Exception("错误：试图编辑未开启编辑模式的图层"); }
                    }
                    mapOperation = value;
                    ClearEditTrack();
                    ChangeCursor();
                    OperationTypeChanged?.Invoke(this);
                }
            }
        }

        /// <summary>
        /// 获取或设置对象的选择模式
        /// </summary>
        public SelectedMode SelectedMode
        {
            get => selectedmode;
            set { selectedmode = value; SelectedModeChanged?.Invoke(this); }
        }

        /// <summary>
        /// 获取或设置是否需要保存标识，用于关闭前弹窗提示
        /// </summary>
        public bool NeedSave { get => needSave; set => needSave = value; }

        /// <summary>
        /// 获取或设置双重选择的对象
        /// </summary>
        public int DoubleSelectedItem { get => doubleSelectedItem;
            set => doubleSelectedItem = value; }

        #endregion

        #region 方法

        public void NewMap()
        {
            Graphics g = Graphics.FromHwnd(Handle);
            map = new Map(g);
            g.Dispose();
            needRefresh = true;
            needSave = false;
            OperationType = OperationType.None;
        }

        /// <summary>
        /// 加载文件，并生成新的Map对象
        /// </summary>
        /// <param name="path">文件路径</param>
        public void OpenFile(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                map = (Map)bf.Deserialize(fs);
            }
            catch (Exception e)
            {
                fs.Dispose();
                throw new FileLoadException("错误：map数据文件读取失败", e);
            }
            fs.Dispose();
            needRefresh = true;
        }

        /// <summary>
        /// 将map对象保存至指定路径
        /// </summary>
        /// <param name="path">文件保存路径</param>
        public void SaveFile(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            // 保存数据把已选择对象去除
            List<int>[] selectedItems = new List<int>[map.Layers.Count];
            for (int i = 0; i < map.Layers.Count; i++)
            {
                selectedItems[i] = map.Layers[i].SelectedItems;
                map.Layers[i].SelectedItems = new List<int>();
            }
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, map);
            fs.Dispose();
            needSave = false;
            // 恢复已选择数据
            for (int i = 0; i < map.Layers.Count; i++)
            {
                map.Layers[i].SelectedItems = selectedItems[i];
            }
        }

        /// <summary>
        /// 将当前地图画面输出成栅格图像
        /// </summary>
        /// <param name="path">图像保存路径</param>
        public void SaveBitmap(string path)
        {
            cache.Save(path);
        }

        /// <summary>
        /// 清空当前编辑状态（跟踪几何体和编辑几何体）
        /// </summary>
        public void ClearEditTrack()
        {
            trackingPoints.Clear();
            editGeometries[0] = editGeometries[1] = editGeometries[2] = null;
        }

        /// <summary>
        /// 设置标记，要更新地理底图的画面（不包括已选择高亮，编辑跟踪对象）
        /// </summary>
        public void SetNeedRefreshBase()
        {
            needRefresh = true;
        }

        #endregion

        #region 事件

        public delegate void SimpleHandler(object sender);

        /// <summary>
        /// 控件的工具（操作类型）发生变化之后
        /// </summary>
        public event SimpleHandler OperationTypeChanged;

        /// <summary>
        /// 控件的对象的选择模式发生变化之后
        /// </summary>
        public event SimpleHandler SelectedModeChanged;

        /// <summary>
        /// 图层的被选择要素发生变化之后
        /// </summary>
        public event SimpleHandler SelectedFeatureChanged;

        #endregion

        #region 私有函数

        /// <summary>
        /// 更新Cache对象，即缓冲图片层（地图的基础层）
        /// </summary>
        private void RedrawMap()
        {
            Graphics g = Graphics.FromImage(cache);
            g.Clear(BackColor);
            map.Render(g);
            needRefresh = false;
        }

        /// <summary>
        /// 重新绘制跟踪层
        /// </summary>
        /// <param name="g">GDI绘图对象</param>
        private void DrawTrackingLayer(Graphics g)
        {
            if (map.SelectedLayer == -1) { return; }

            Layer layer = map.Layers[map.SelectedLayer];
            PointF[] screenPs = new PointF[trackingPoints.Count + 1];
            // 转换至屏幕坐标
            for (int i = 0; i < trackingPoints.Count; i++)
            {
                PointD screenPD = map.FromMapPoint(trackingPoints[i]);
                screenPs[i] = new PointF((float)screenPD.X, (float)screenPD.Y);
            }
            screenPs[trackingPoints.Count] = mouseLoc;
            // 绘制轮廓
            Pen linePen = new Pen(Color.Green, 2);
            if (layer.FeatureType == typeof(Polyline) ||
                layer.FeatureType == typeof(MultiPolyline) &&
                screenPs.Length > 1)
            {
                g.DrawLines(linePen, screenPs);
            }
            else if (layer.FeatureType == typeof(Polyline) ||
                layer.FeatureType == typeof(MultiPolyline))
            {
                if (screenPs.Length > 2)
                { g.DrawPolygon(linePen, screenPs); }
                else { g.DrawLines(linePen, screenPs); }
            }
            // 绘制顶点
            for (int i = 0; i < trackingPoints.Count - 1; i++)
            {
                g.FillRectangle(Brushes.Green, screenPs[i].X - 4, screenPs[i].Y - 4, 9, 9);
            }
            g.FillRectangle(Brushes.Red, screenPs[screenPs.Length - 2].X - 4,
                screenPs[screenPs.Length - 2].Y - 4, 9, 9);
            linePen.Dispose();
        }

        /// <summary>
        /// 绘制已选择的几何体
        /// </summary>
        /// <param name="g">GDI绘图对象</param>
        private void DrawSelectedGeometries(Graphics g)
        {
            Pen highLight = new Pen(Color.Cyan, 3);
            SolidBrush brush = new SolidBrush(Color.Yellow);
            Pen doublePen = new Pen(Color.Yellow, 5);
            Layer layer = map.Layers[map.SelectedLayer];
            List<Geometry> selecedGeo = new List<Geometry>();
            Geometry doubleSelected = null;
            // 查找满足条件的几何体
            HashSet<int> indexSet = new HashSet<int>(layer.SelectedItems);
            foreach (Geometry geo in layer.Features)
            {
                if (indexSet.Count == 0) { break; }
                if (indexSet.Contains(geo.ID))
                {
                    selecedGeo.Add(geo);
                    if (geo.ID == doubleSelectedItem)
                    { doubleSelected = geo; }
                    indexSet.Remove(geo.ID);
                }
            }
            // 绘制几何体
            // 点：绘制一个蓝色圈
            if (layer.FeatureType == typeof(PointD))
            {
                if (doubleSelected != null)
                {
                    PointD screenP = map.FromMapPoint((PointD)doubleSelected);
                    g.FillEllipse(brush, (float)screenP.X - 4, (float)screenP.Y - 4, 9, 9);
                }
                foreach (Geometry geo in selecedGeo)
                {
                    PointD screenP = map.FromMapPoint((PointD)geo);
                    g.DrawEllipse(highLight, (float)screenP.X - 4, (float)screenP.Y - 4, 9, 9);
                }
            }
            // 线和面：绘制边界
            else if (layer.FeatureType == typeof(Polyline) ||
                layer.FeatureType == typeof(Polygon))
            {
                if (doubleSelected != null)
                {
                    PointF[] points = FromMapPointArray(doubleSelected);
                    if (layer.FeatureType == typeof(Polyline))
                    {
                        g.DrawLines(doublePen, points);
                    }
                    else
                    {
                        g.FillPolygon(brush, points);
                    }
                }
                foreach (Geometry geo in selecedGeo)
                {
                    PointF[] points = FromMapPointArray(geo);
                    if (layer.FeatureType == typeof(Polyline))
                    {
                        g.DrawLines(highLight, points);
                    }
                    else
                    {
                        g.DrawPolygon(highLight, points);
                    }
                }
            }
            // 复合图形：绘制全部边界
            else if (layer.FeatureType == typeof(MultiPolyline))
            {
                if (doubleSelected != null)
                {
                    foreach (Polyline line in ((MultiPolyline)doubleSelected).Data)
                    {
                        PointF[] points = FromMapPointArray(line);
                        g.DrawLines(doublePen, points);
                    }
                }
                foreach (Geometry geo in selecedGeo)
                {
                    foreach (Polyline line in ((MultiPolyline)geo).Data)
                    {
                        PointF[] points = FromMapPointArray(line);
                        g.DrawLines(highLight, points);
                    }
                }
            }
            else
            {
                if (doubleSelected != null)
                {
                    foreach (Polygon polygon in ((MultiPolygon)doubleSelected).Data)
                    {
                        PointF[] points = FromMapPointArray(polygon);
                        g.FillPolygon(brush, points);
                    }
                }
                foreach (Geometry geo in selecedGeo)
                {
                    foreach (Polygon polygon in ((MultiPolygon)geo).Data)
                    {
                        PointF[] points = FromMapPointArray(polygon);
                        g.DrawPolygon(highLight, points);
                    }
                }
            }

            highLight.Dispose();
            brush.Dispose();
            doublePen.Dispose();
        }

        /// <summary>
        /// 绘制正在编辑的几何体
        /// </summary>
        /// <param name="g">GDI绘图对象</param>
        private void DrawEditGeometry(Graphics g)
        {
            // 复合折线、多边形，绘制其他部分
            if (editGeometries[2] != null)
            {
                if (editGeometries[2].GetType() == typeof(MultiPolyline))
                {
                    foreach (Polyline polyline in ((MultiPolyline)editGeometries[2]).Data)
                    {
                        if (object.ReferenceEquals(polyline, editGeometries[1]))
                        { continue; }
                        g.DrawLines(Pens.DarkGreen, FromMapPointArray(polyline));
                    }
                }
                else
                {
                    foreach (Polygon polygon in ((MultiPolygon)editGeometries[2]).Data)
                    {
                        if (object.ReferenceEquals(polygon, editGeometries[1]))
                        { continue; }
                        g.DrawPolygon(Pens.DarkGreen, FromMapPointArray(polygon));
                    }
                }
            }
            // 绘制可能的其他节点
            if (editGeometries[1] != null)
            {
                PointF[] screenPs = FromMapPointArray(editGeometries[1]);
                foreach (PointF p in screenPs)
                {
                    g.FillRectangle(Brushes.Green, p.X - 4, p.Y - 4, 9, 9);
                }
                Pen linePen = new Pen(Color.Green, 2);
                if (editGeometries[1].GetType() == typeof(Polyline))
                { g.DrawLines(linePen, screenPs); }
                else { g.DrawPolygon(linePen, screenPs); }
                linePen.Dispose();
            }
            // 绘制可能的正操作节点
            if (editGeometries[0] != null)
            {
                PointD screenP = map.FromMapPoint((PointD)editGeometries[0]);
                g.FillRectangle(Brushes.Red, (float)screenP.X - 4, (float)screenP.Y - 4, 9, 9);
            }
        }

        /// <summary>
        /// 把点集合型的几何体（Polyline或Polygon）从地图坐标转换成屏幕坐标
        /// </summary>
        /// <param name="geo">几何体（必须是Polyline或Polygon）</param>
        /// <returns>屏幕坐标的点集合</returns>
        private PointF[] FromMapPointArray(Geometry geo)
        {
            if (geo.GetType() != typeof(Polyline) &&
                geo.GetType() != typeof(Polygon))
            {
                throw new ArgumentException("geo对象必须是Polyline或Polygon", "geo");
            }
            IList<PointD> mapPoints;
            if (geo.GetType() == typeof(Polyline))
            { mapPoints = ((Polyline)geo).Data; }
            else { mapPoints = ((Polygon)geo).Data; }
            PointF[] screenPs = new PointF[mapPoints.Count];
            for (int i = 0; i < screenPs.Length; i++)
            {
                PointD screenP = map.FromMapPoint(mapPoints[i]);
                screenPs[i] = new PointF((float)screenP.X, (float)screenP.Y);
            }
            return screenPs;
        }

        /// <summary>
        /// 开始选择几何体
        /// </summary>
        /// <param name="x">鼠标x坐标</param>
        /// <param name="y">鼠标y坐标</param>
        private void SelectFeature(int x, int y)
        {
            if (map.SelectedLayer == -1)
            {
                return ;
            }
            Layer layer = map.Layers[map.SelectedLayer];
            List<int> selectedItems = new List<int>();   // 本次选择的要素
            // 从已选择图层中搜索
            if (mouseDownLoc.X == x && mouseDownLoc.Y == y)
            {
                PointD p = map.ToMapPoint(new PointD(x, y));
                double distance = map.ToMapDistance(AttractRadius);
                foreach (Geometry geometry in layer.Features)
                {
                    if (geometry.IsPointOn(p, distance))
                    {
                        selectedItems.Add(geometry.ID);
                    }
                }
            }
            else
            {
                PointD startP = map.ToMapPoint(new PointD(mouseDownLoc.X, mouseDownLoc.Y)),
                    endP = map.ToMapPoint(new PointD(x, y));
                double minX = Math.Min(startP.X, endP.X),
                    minY = Math.Min(startP.Y, endP.Y),
                    maxX = Math.Max(startP.X, endP.X),
                    maxY = Math.Max(startP.Y, endP.Y);
                RectangleD rect = new RectangleD(minX, minY, maxX, maxY);
                foreach (Geometry geometry in layer.Features)
                {
                    if (geometry.IsWithinBox(rect))
                    {
                        selectedItems.Add(geometry.ID);
                    }
                }
            }
            // 更改选择的对象
            HashSet<int> set = new HashSet<int>(layer.SelectedItems);
            switch (selectedmode)
            {
                case SelectedMode.New:
                    layer.SelectedItems = selectedItems;
                    break;
                case SelectedMode.Add:
                    set.UnionWith(selectedItems);
                    layer.SelectedItems = new List<int>(set);
                    break;
                case SelectedMode.Delete:
                    set.ExceptWith(selectedItems);
                    layer.SelectedItems = new List<int>(set);
                    break;
                case SelectedMode.Intersect:
                    set.IntersectWith(selectedItems);
                    layer.SelectedItems = new List<int>(set);
                    break;
            }
            SelectedFeatureChanged?.Invoke(this);
        }

        /// <summary>
        /// 开始编辑几何体，左键点击选择要编辑的对象
        /// </summary>
        /// <param name="x">鼠标x坐标</param>
        /// <param name="y">鼠标y坐标</param>
        private void StartEditGeometry(int x, int y)
        {
            if (map.SelectedLayer == -1) { return; }
            Layer layer = map.Layers[map.SelectedLayer];
            PointD mouseMapP = map.ToMapPoint(new PointD(x, y));
            double buffer = map.ToMapDistance(AttractRadius);

            // 初始化3层几何体的搜索遍历范围
            List<Geometry>[] ienumList = new List<Geometry>[3];   // 下标从0~2优先级下降
            // 点：直接搜索图层
            if (layer.FeatureType == typeof(PointD))
            {
                ienumList[0] = layer.Features;
            }
            // 线和面：若已选择线面，则找顶点，否则找线面
            else if (layer.FeatureType == typeof(Polyline) ||
                layer.FeatureType == typeof(Polygon))
            {
                ienumList[1] = layer.Features;
                if (editGeometries[1] != null)
                {
                    if (editGeometries[1].GetType() == typeof(Polyline))
                    {
                        ienumList[0] = new List<Geometry>(((Polyline)editGeometries[1]).Data);
                    }
                    else
                    {
                        ienumList[0] = new List<Geometry>(((Polygon)editGeometries[1]).Data);
                    }
                }
            }
            // 多线和多面：若已选择线面，则找顶点，否则找多线多面
            else if (layer.FeatureType == typeof(MultiPolyline) ||
                layer.FeatureType == typeof(MultiPolygon))
            {
                ienumList[2] = layer.Features;
                if (editGeometries[1] != null)
                {
                    if (layer.FeatureType == typeof(MultiPolyline))
                    {
                        ienumList[0] = new List<Geometry>(((Polyline)editGeometries[1]).Data);
                    }
                    else
                    {
                        ienumList[0] = new List<Geometry>(((Polygon)editGeometries[1]).Data);
                    }
                }
            }

            // 根据优先级搜索需编辑的对象
            for (int i = 0; i < 3; i++)
            {
                if (ienumList[i] == null) { continue; }
                editGeometries[i] = null;
                foreach (Geometry geo in ienumList[i])
                {
                    if (geo.IsPointOn(mouseMapP, buffer))
                    {
                        editGeometries[i] = geo;
                        break;
                    }
                }
                // 该层已经找到对象，结束
                if (editGeometries[i] != null) { break; }
            }
            // 复合几何体要精确到Polyline或Polygon
            if (editGeometries[2] != null)
            {
                if (editGeometries[2].GetType() == typeof(MultiPolyline))
                {
                    ienumList[1] = new List<Geometry>(((MultiPolyline)editGeometries[2]).Data);
                }
                else
                {
                    ienumList[1] = new List<Geometry>(((MultiPolygon)editGeometries[2]).Data);
                }
                editGeometries[1] = null;
                foreach (Geometry geo in ienumList[1])
                {
                    if (geo.IsPointOn(mouseMapP, buffer))
                    {
                        editGeometries[1] = geo;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 移动编辑的几何体，用于move
        /// </summary>
        /// <param name="x">鼠标x坐标</param>
        /// <param name="y">鼠标y坐标</param>
        private void MoveEditGeometry(int x, int y)
        {
            PointD prePoint = map.ToMapPoint(new PointD(mouseLoc.X, mouseLoc.Y));
            PointD curPoint = map.ToMapPoint(new PointD(x, y));
            bool moved = false;
            for (int i = 0; i < 3; i++)
            {
                if (editGeometries[i] == null) { continue; }
                if (!moved)
                {
                    editGeometries[i].Move(curPoint.X - prePoint.X, curPoint.Y - prePoint.Y);
                    moved = true;
                }
                if (moved)
                { editGeometries[i].NeedRenewBox(); }
            }
            if (moved)
            {
                map.Layers[map.SelectedLayer].RefreshBox();
                map.RefreshBox();
            }
        }

        /// <summary>
        /// 在已选择Polyline或者Polygon时，右键点击选择顶点，放入editGeometries[0]
        /// </summary>
        /// <param name="x">鼠标x坐标</param>
        /// <param name="y">鼠标y坐标</param>
        private void ChooseVertex(int x, int y)
        {
            addVertexLoc = -1;
            if (editGeometries[1] == null)
            { return; }
            PointD mapP = map.ToMapPoint(new PointD(x, y));
            double buffer = map.ToMapDistance(AttractRadius);
            // 测试原选择的顶点是否仍然选择
            if (editGeometries[0] != null &&
                editGeometries[0].IsPointOn(mapP, buffer))
            { return; }

            // 未选择原来的顶点，从对应的Polyline或Polygon中选
            IList<PointD> points;
            if (editGeometries[1].GetType() == typeof(Polyline))
            { points = ((Polyline)editGeometries[1]).Data; }
            else { points = ((Polygon)editGeometries[1]).Data; }
            // 循环查找哪个顶点满足选择要求
            foreach (PointD geo in points)
            {
                if (geo.IsPointOn(mapP, buffer))
                {
                    editGeometries[0] = geo;
                    return;
                }
            }

            // 没有节点被选中，尝试找到插入顶点的位置
            int loopNum = editGeometries[1].GetType() == typeof(Polyline) ?
                points.Count - 1 : points.Count;
            for (int i = 0; i < loopNum; i++)
            {
                int nextI = i == points.Count - 1 ? 0 : i + 1;
                PointD a = new PointD(mapP.X - points[i].X, mapP.Y - points[i].Y);
                PointD b = new PointD(points[nextI].X - points[i].X,
                    points[nextI].Y - points[i].Y);
                double relaLoc = (a.X * b.X + a.Y * b.Y) / (b.X * b.X + b.Y * b.Y); // 垂足相对线段的位置
                double dist = buffer * 2;
                if (relaLoc > 0 && relaLoc < 1)
                {
                    dist = Math.Abs(a.X * b.Y - b.X * a.Y) / Math.Sqrt(b.X * b.X + b.Y * b.Y);
                }
                if (dist < buffer)
                {
                    addVertexLoc = i + 1;
                    break;
                }
            }
        }

        /// <summary>
        /// 结束track模式
        /// </summary>
        private void FinishTrack()
        {
            Layer layer = map.Layers[map.SelectedLayer];
            if (mapOperation == OperationType.Track)
            {
                // 生成的是线
                if ((layer.FeatureType == typeof(Polyline) ||
                    layer.FeatureType == typeof(MultiPolyline)) &&
                    trackingPoints.Count > 1)
                {
                    Polyline line = new Polyline(trackingPoints);
                    if (layer.FeatureType == typeof(Polyline))
                    {
                        layer.AddFeature(line);
                    }
                    else if (editGeometries[2] != null)
                    {
                        ((MultiPolyline)editGeometries[2]).Data.Add(line);
                        editGeometries[2].NeedRenewBox();
                        editGeometries[1] = line;
                        mapOperation = OperationType.Edit;
                    }
                    else
                    {
                        MultiPolyline multiPolyline = new MultiPolyline(new Polyline[] { line });
                        layer.AddFeature(multiPolyline);
                    }
                    map.Layers[map.SelectedLayer].RefreshBox();
                    map.RefreshBox();
                    needRefresh = true;
                    needSave = true;
                }
                // 生成的是面
                else if ((layer.FeatureType == typeof(Polygon) ||
                    layer.FeatureType == typeof(MultiPolygon)) &&
                    trackingPoints.Count > 2)
                {
                    Polygon polygon = new Polygon(trackingPoints);
                    if (layer.FeatureType == typeof(Polygon))
                    {
                        layer.AddFeature(polygon);
                    }
                    else if (editGeometries[2] != null)
                    {
                        ((MultiPolygon)editGeometries[2]).Data.Add(polygon);
                        editGeometries[2].NeedRenewBox();
                        editGeometries[1] = polygon;
                        // TODO：标记是洞还是实体多边形
                        mapOperation = OperationType.Edit;
                    }
                    else
                    {
                        MultiPolygon multiPolygon = new MultiPolygon(new Polygon[] { polygon });
                        layer.AddFeature(multiPolygon);
                    }
                    map.Layers[map.SelectedLayer].RefreshBox();
                    map.RefreshBox();
                    needRefresh = true;
                    needSave = true;
                }
                if (mapOperation != OperationType.Edit) { mapOperation = OperationType.None; }
                trackingPoints.Clear();
                ChangeCursor();
                OperationTypeChanged.Invoke(this);
            }
        }

        /// <summary>
        /// 编辑模式下，更新右键菜单各项目的可见性
        /// </summary>
        private void RenewEditStripVisible()
        {
            Type layerType = map.Layers[map.SelectedLayer].FeatureType;
            // 更新“添加节点”“删除节点”
            addVertexToolStripMenuItem.Visible = addVertexLoc != -1;
            delVertexToolStripMenuItem.Visible = false;
            if (editGeometries[0] != null && editGeometries[1] != null)
            {
                if (editGeometries[1].GetType() == typeof(Polyline) &&
                    ((Polyline)editGeometries[1]).Data.Count > 2)
                { delVertexToolStripMenuItem.Visible = true; }
                else if (editGeometries[1].GetType() == typeof(Polygon) &&
                    ((Polygon)editGeometries[1]).Data.Count > 3)
                { delVertexToolStripMenuItem.Visible = true; }
            }
            // 更新“删除几何”“添加部件”“删除部件”
            if (layerType == typeof(Point))
            {
                delGeoToolStripMenuItem.Visible = editGeometries[0] != null;
                addPartToolStripMenuItem.Visible = false;
                delPartToolStripMenuItem.Visible = false;
            }
            else if (layerType == typeof(Polyline) || layerType == typeof(Polygon))
            {
                delGeoToolStripMenuItem.Visible = editGeometries[1] != null;
                addPartToolStripMenuItem.Visible = false;
                delPartToolStripMenuItem.Visible = false;
            }
            else
            {
                delGeoToolStripMenuItem.Visible = editGeometries[2] != null;
                addPartToolStripMenuItem.Visible = editGeometries[2] != null;
                if (editGeometries[2].GetType() == typeof(MultiPolyline))
                {
                    delPartToolStripMenuItem.Visible =
                       editGeometries[1] != null &&
                       ((MultiPolyline)editGeometries[2]).Data.Count > 1;

                }
                else
                {
                    delPartToolStripMenuItem.Visible =
                       editGeometries[1] != null &&
                       ((MultiPolygon)editGeometries[2]).Data.Count > 1;
                }
            }
        }

        // mapOperation模式改变时，更改光标
        private void ChangeCursor()
        {
            switch (mapOperation)
            {
                case OperationType.ZoomIn:
                    Cursor = zoomInCursor;
                    break;
                case OperationType.ZoomOut:
                    Cursor = zoomOutCursor;
                    break;
                case OperationType.Track:
                    Cursor = Cursors.Cross;
                    break;
                default:
                    Cursor = Cursors.Arrow;
                    break;
            }
        }

        #endregion

        #region 响应事件处理

        // 重新绘制控件
        private void MapControl_Paint(object sender, PaintEventArgs e)
        {
            if (needRefresh)
            {
                RedrawMap();
            }
            e.Graphics.DrawImage(cache, 0, 0);
            if (map.SelectedLayer != -1)
            {
                DrawSelectedGeometries(e.Graphics);
            }
            if (mapOperation == OperationType.Edit)
            {
                DrawEditGeometry(e.Graphics);
            }
            else if (mapOperation == OperationType.Track)
            {
                DrawTrackingLayer(e.Graphics);
            }
        }

        // 控件大小改变
        private void MapControl_SizeChanged(object sender, EventArgs e)
        {
            if (Width != cache.Width || Height != cache.Height)
            {
                cache.Dispose();
                cache = new Bitmap(Width, Height);
                needRefresh = true;
            }
        }

        // 鼠标按下
        private void MapControl_MouseDown(object sender, MouseEventArgs e)
        {
            PointD mapPoint = map.ToMapPoint(new PointD(e.X, e.Y));
            // 鼠标左键
            if (e.Button == MouseButtons.Left)
            {
                switch (mapOperation)
                {
                    case OperationType.Edit:
                        StartEditGeometry(e.X, e.Y);
                        foreach(Geometry geo in editGeometries)
                        {
                            if (geo != null) { Cursor = Cursors.NoMove2D; break; }
                        }
                        break;
                    case OperationType.Track:
                        if (e.Clicks == 1)
                        {
                            // 添加点直接添加
                            Layer layer = map.Layers[map.SelectedLayer];
                            if (layer.FeatureType == typeof(PointD))
                            {
                                layer.AddFeature(mapPoint);
                                layer.RefreshBox();
                                map.RefreshBox();
                                needRefresh = true;
                                needSave = true;
                            }
                            else
                            {
                                trackingPoints.Add(mapPoint);
                            }
                            Refresh();
                        }
                        break;
                    case OperationType.ZoomIn:
                        map.ZoomByCenter(mapPoint, ZoomRatio);
                        needRefresh = true;
                        Refresh();
                        break;
                    case OperationType.ZoomOut:
                        map.ZoomByCenter(mapPoint, 1 / ZoomRatio);
                        needRefresh = true;
                        Refresh();
                        break;
                }
                mouseDownLoc = e.Location;
            }
            // 鼠标右键
            else if (e.Button == MouseButtons.Right)
            {
                switch (mapOperation)
                {
                    case OperationType.Track:
                        Type layerType = map.Layers[map.SelectedLayer].FeatureType;
                        delLastVertexToolStripMenuItem.Visible = trackingPoints.Count != 0;
                        trackNewPartToolStripMenuItem.Visible =
                            (layerType == typeof(MultiPolyline) && trackingPoints.Count > 1) ||
                            (layerType == typeof(MultiPolygon) && trackingPoints.Count > 2);
                        trackContexStrip.Show(this, e.X, e.Y);
                        break;
                    case OperationType.Edit:
                        ChooseVertex(e.X, e.Y);
                        RenewEditStripVisible();
                        editContextStrip.Show(this, e.X, e.Y);
                        break;
                }
                mouseRDownLoc = e.Location;
            }
        }

        // 鼠标移动
        private void MapControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                switch (mapOperation)
                {
                    case OperationType.Edit:
                        MoveEditGeometry(e.X, e.Y);
                        needRefresh = true;
                        needSave = true;
                        Refresh();
                        break;
                    case OperationType.Pan:
                        PointD prePoint = map.ToMapPoint(new PointD(mouseLoc.X, mouseLoc.Y));
                        PointD curPoint = map.ToMapPoint(new PointD(e.X, e.Y));
                        map.OffsetX += prePoint.X - curPoint.X;
                        map.OffsetY += prePoint.Y - curPoint.Y;
                        needRefresh = true;
                        Refresh();
                        break;
                    case OperationType.Select:
                        // 绘制选择框
                        Refresh();
                        Graphics g = Graphics.FromHwnd(Handle);
                        Pen boxPen = new Pen(Color.DarkBlue, 2);
                        float minx = Math.Min(mouseDownLoc.X, e.X),
                            miny = Math.Min(mouseDownLoc.Y, e.Y),
                            maxx = Math.Max(mouseDownLoc.X, e.X),
                            maxy = Math.Max(mouseDownLoc.Y, e.Y);
                        g.DrawRectangle(boxPen, minx, miny, maxx - minx, maxy - miny);
                        boxPen.Dispose();
                        g.Dispose();
                        break;
                    case OperationType.Track:
                        Refresh();
                        break;
                }
            }
            mouseLoc = e.Location;
        }

        // 鼠标松开
        private void MapControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                switch (mapOperation)
                {
                    case OperationType.Select:
                        SelectFeature(e.X, e.Y);
                        Refresh();
                        break;
                    case OperationType.Edit:
                        Cursor = Cursors.Arrow;
                        break;
                }
            }
        }

        // 鼠标双击
        private void MapControl_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                FinishTrack();
                Refresh();
            }
        }

        // 滑轮滚动
        private void MapControl_MouseWheel(object sender, MouseEventArgs e)
        {
            PointD zoomCenter = map.ToMapPoint(new PointD(e.X, e.Y));     // 控件中心点
            if (e.Delta > 0)
            {
                map.ZoomByCenter(zoomCenter, ZoomRatio);
                needRefresh = true;
                Refresh();
            }
            else
            {
                map.ZoomByCenter(zoomCenter, 1 / ZoomRatio);
                needRefresh = true;
                Refresh();
            }
        }

        // 以下为右键菜单处理
        // Edit模式插入顶点
        private void addVertexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PointD p = map.ToMapPoint(new PointD(mouseRDownLoc.X, mouseRDownLoc.Y));
            if (editGeometries[1].GetType() == typeof(Polyline))
            {
                ((Polyline)editGeometries[1]).Data.Add(p);
            }
            else
            {
                ((Polygon)editGeometries[1]).Data.Add(p);
            }
            needRefresh = true;
            needSave = true;
            editGeometries[1].NeedRenewBox();
            if (editGeometries[2] != null)
            { editGeometries[2].NeedRenewBox(); }
            map.Layers[map.SelectedLayer].RefreshBox();
            map.RefreshBox();
            Refresh();
        }

        // Edit模式删除选择的顶点
        private void delVertexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (editGeometries[1].GetType() == typeof(Polyline))
            {
                ((Polyline)editGeometries[1]).Data.Remove((PointD)editGeometries[0]);
            }
            else
            {
                ((Polygon)editGeometries[1]).Data.Remove((PointD)editGeometries[0]);
            }
            editGeometries[0] = null;
            editGeometries[1].NeedRenewBox();
            if (editGeometries[2] != null) { editGeometries[2].NeedRenewBox(); }
            map.Layers[map.SelectedLayer].RefreshBox();
            map.RefreshBox();
            needRefresh = true;
            needSave = true;
            Refresh();
        }

        // Edit模式删除选择的几何体
        private void delGeoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Layer layer = map.Layers[map.SelectedLayer];
            if (layer.FeatureType == typeof(PointD))
            {
                layer.DelFeature(editGeometries[0].ID);
            }
            else if (layer.FeatureType == typeof(Polyline) ||
                layer.FeatureType == typeof(Polygon))
            {
                layer.DelFeature(editGeometries[1].ID);
                editGeometries[1] =  null;
            }
            else
            {
                layer.DelFeature(editGeometries[2].ID);
                editGeometries[1] = editGeometries[2] = null;
            }
            editGeometries[0] = null;
            needRefresh = true;
            needSave = true;
            Refresh();
        }

        // Edit模式复合几何添加部件
        private void addPartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            trackingPoints.Clear();
            mapOperation = OperationType.Track;
            ChangeCursor();
            OperationTypeChanged?.Invoke(this);
        }

        // Edit模式复合几何删除部件
        private void delPartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (editGeometries[2].GetType() == typeof(MultiPolyline))
            {
                ((MultiPolyline)editGeometries[2]).Data.Remove((Polyline)editGeometries[1]);
                editGeometries[1] = ((MultiPolyline)editGeometries[2]).Data[0];
            }
            else
            {
                ((MultiPolygon)editGeometries[2]).Data.Remove((Polygon)editGeometries[1]);
                editGeometries[1] = ((MultiPolygon)editGeometries[2]).Data[0];
            }
            editGeometries[2].NeedRenewBox();
            editGeometries[0] = null;
            map.Layers[map.SelectedLayer].RefreshBox();
            map.RefreshBox();
            needRefresh = true;
            needSave = true;
            Refresh();
        }

        // track模式删除上个顶点
        private void delLastVertexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (trackingPoints.Count != 0)
            {
                trackingPoints.RemoveAt(trackingPoints.Count - 1);
                Refresh();
            }
        }

        // track模式结束绘制
        private void finishTrackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FinishTrack();
            Refresh();
        }

        // track模式清除绘制
        private void clearTrackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OperationType = OperationType.None;
            Refresh();
        }

        // track复合几何体添加新部分
        private void trackNewPartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Layer layer = map.Layers[map.SelectedLayer];
            // MultiPolyline的添加
            if (layer.FeatureType == typeof(MultiPolyline))
            {
                Polyline line = new Polyline(trackingPoints);
                // 是第一次绘制该复合几何
                if (editGeometries[2] == null)
                {
                    editGeometries[2] = new MultiPolyline(new Polyline[] { line });
                    layer.AddFeature(editGeometries[2]);
                }
                editGeometries[1] = line;
            }
            // MultiPolygon的添加
            else
            {
                Polygon polygon = new Polygon(trackingPoints);
                if (editGeometries[2] == null)
                {
                    editGeometries[2] = new MultiPolygon(new Polygon[] { polygon });
                    layer.AddFeature(editGeometries[2]);
                }
                editGeometries[1] = polygon;
            }
            layer.RefreshBox();
            map.RefreshBox();
            trackingPoints.Clear();
            needRefresh = true;
            needSave = true;
            Refresh();
        }

        #endregion
    }

    /// <summary>
    /// 地图选择对象的模式
    /// </summary>
    public enum SelectedMode
    {
        /// <summary>
        /// 选取新的对象
        /// </summary>
        New,
        /// <summary>
        /// 在原选择基础上添加新选择的对象
        /// </summary>
        Add,
        /// <summary>
        /// 在原选择基础上删除新选择的对象
        /// </summary>
        Delete,
        /// <summary>
        /// 取原选择对象和新选择对象的交集
        /// </summary>
        Intersect
    }

    /// <summary>
    /// 地图的编辑状态的枚举，表示当前使用的工具
    /// </summary>
    public enum OperationType
    {
        /// <summary>
        /// 无状态
        /// </summary>
        None,

        /// <summary>
        /// 地图放大
        /// </summary>
        ZoomIn,

        /// <summary>
        /// 地图缩小
        /// </summary>
        ZoomOut,

        /// <summary>
        /// 地图漫游
        /// </summary>
        Pan,

        /// <summary>
        /// 绘制新的对象
        /// </summary>
        Track,

        /// <summary>
        /// 编辑对象的几何属性（节点）
        /// </summary>
        Edit,

        /// <summary>
        /// 选择对象
        /// </summary>
        Select
    }
}
