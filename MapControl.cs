using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace simpleGIS
{
    /// <summary>
    /// 地图控件类
    /// </summary>
    public partial class MapControl : UserControl
    {
        #region 字段
        private Map map = new Map();            // 地图对象
        private OperationType mapOperation;     // 当前地图操作类型
        private SelectedMode selectedmode;      // 地图选择对象的模式
        private bool needRefresh = false;       // 是否需要更新cache
        private bool needSave = false;          // 是否需要保存数据
        private Bitmap cache;   // 当前地图显示范围的缓冲图片，不包括跟踪层和已选对象层

        private PointF mouseLoc = new PointF();             // 记录鼠标当前位置
        private PointF mouseDownLoc = new PointF();         // 鼠标左键按下时的位置
        #endregion
        private const double ZoomRatio = 1.2;   // 缩放系数
        private const float AttractRadius = 4;   // 吸引区半径

        public MapControl()
        {
            cache = new Bitmap(Width, Height);
            selectedmode = SelectedMode.New;
            mapOperation = OperationType.None;
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
        public OperationType OperationType { get => mapOperation; set => mapOperation = value; }

        /// <summary>
        /// 获取或设置对象的选择模式
        /// </summary>
        public SelectedMode SelectedMode { get => selectedmode; set => selectedmode = value; }

        /// <summary>
        /// 获取或设置是否需要保存标识，用于关闭前弹窗提示
        /// </summary>
        public bool NeedSave { get => needSave; set => needSave = value; }

        #endregion

        #region 方法

        public void NewMap()
        {
            map = new Map();
            needRefresh = true;
            needSave = false;
        }

        /// <summary>
        /// 加载文件，并生成新的Map对象
        /// </summary>
        /// <param name="path">文件路径</param>
        public void OpenFile(string path)
        {

        }

        /// <summary>
        /// 将map对象保存至指定路径
        /// </summary>
        /// <param name="path">文件保存路径</param>
        public void SaveFile(string path)
        {

        }

        /// <summary>
        /// 将当前地图画面输出成栅格图像
        /// </summary>
        /// <param name="path">图像保存路径</param>
        public void SaveBitmap(string path)
        {
            cache.Save(path);
        }

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

        }

        /// <summary>
        /// 绘制已选择的几何体
        /// </summary>
        /// <param name="g">GDI绘图对象</param>
        private void DrawSelectedFeatures(Graphics g)
        {

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
            DrawTrackingLayer(e.Graphics);
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
            if (e.Button == MouseButtons.Left)
            {
                switch (mapOperation)
                {
                    case OperationType.Edit:
                        break;
                    case OperationType.Track:
                        if (e.Clicks == 1)
                        {

                        }
                        break;
                    case OperationType.ZoomIn:
                        map.ZoomByCenter(map.ToMapPoint(new PointD(e.X, e.Y)), ZoomRatio);
                        needRefresh = true;
                        Refresh();
                        break;
                    case OperationType.ZoomOut:
                        map.ZoomByCenter(map.ToMapPoint(new PointD(e.X, e.Y)), 1 / ZoomRatio);
                        needRefresh = true;
                        Refresh();
                        break;
                }
                mouseDownLoc = e.Location;
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
                        Refresh();
                        Graphics g = Graphics.FromHwnd(Handle);
                        Pen boxPen = new Pen(Color.DarkGreen, 2);
                        float minx = Math.Min(mouseDownLoc.X, e.X),
                            miny = Math.Min(mouseDownLoc.Y, e.Y),
                            maxx = Math.Max(mouseDownLoc.X, e.X),
                            maxy = Math.Max(mouseDownLoc.Y, e.Y);
                        g.DrawRectangle(boxPen, minx, miny, maxx - minx, maxy - miny);
                        g.Dispose();
                        break;
                    case OperationType.Track:
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
                        if (map.Layers.Count == 0)
                        {
                            break;
                        }
                        Layer layer = map.Layers[map.SelectedLayer];
                        List<int> selectedItems = new List<int>();   // 本次选择的要素
                        // 从已选择图层中搜索
                        if (mouseDownLoc.X == e.X && mouseDownLoc.Y == e.Y)
                        {
                            PointD p = map.ToMapPoint(new PointD(e.X, e.Y));
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
                                endP = map.ToMapPoint(new PointD(e.X, e.Y));
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
                        HashSet<int> set = new HashSet<int>(layer.SeletedItems);
                        switch (selectedmode)
                        {
                            case SelectedMode.New:
                                layer.SeletedItems = selectedItems;
                                break;
                            case SelectedMode.Add:
                                set.UnionWith(selectedItems);
                                layer.SeletedItems = new List<int>(set);
                                break;
                            case SelectedMode.Delete:
                                set.ExceptWith(selectedItems);
                                layer.SeletedItems = new List<int>(set);
                                break;
                            case SelectedMode.Intersect:
                                set.IntersectWith(selectedItems);
                                layer.SeletedItems = new List<int>(set);
                                break;
                        }
                        Refresh();
                        break;
                }
            }
        }

        // 鼠标双击
        private void MapControl_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                switch (mapOperation)
                {
                    case OperationType.Track:
                        break;
                }
            }
        }

        // 滑轮滚动
        private void MapControl_MouseWheel(object sender, MouseEventArgs e)
        {
            PointD screenCenter = new PointD(e.X, e.Y);     // 控件中心点
            if (e.Delta > 0)
            {
                map.ZoomByCenter(map.ToMapPoint(screenCenter), ZoomRatio);
                needRefresh = true;
                Refresh();
            }
            else
            {
                map.ZoomByCenter(map.ToMapPoint(screenCenter), 1 / ZoomRatio);
                needRefresh = true;
                Refresh();
            }
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
