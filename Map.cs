﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.Remoting.Messaging;

namespace simpleGIS
{
    [Serializable]
    public class Map
    {
        #region 字段

        private double DpiX { get; set; } //屏幕横向分辨率
        private double DpiY { get; set; } //屏幕纵向分辨率

        #endregion 

        #region 属性

        public List<Layer> Layers { get; set; }   // 地图的所有图层
        public double OffsetX { get; set; }     // 地图显示范围左上角的地图X坐标
        public double OffsetY { get; set; }     // 地图显示范围左上角的地图Y坐标
        public double MapScale { get; set; }     // 地图的比例尺，比例尺为1:MapScale
        public RectangleD Box { get; set; }     // 全地图的外包矩形
        public int SelectedLayer { get; set; }     // 选定图层的下标


        #endregion

        #region 方法


        //构造函数
        public Map(Graphics g)
        {
            Layers = new List<Layer>();
            OffsetX = 0;
            OffsetY = 0;
            MapScale = 1;
            Box = new RectangleD();
            SelectedLayer = -1;
            DpiX = (double)g.DpiX;
            DpiY = (double)g.DpiY;

        }
        /// <summary>
        /// 更新矩形选择盒
        /// </summary>
        public void RefreshBox()
        {
            try
            {
                if (Layers.Count > 0)
                {
                    //只有一个图层
                    if (Layers.Count == 1) { Box = new RectangleD(Layers[0].Box); }

                    //有多个图层
                    else
                    {
                        RectangleD sBox = new RectangleD(Layers[0].Box);
                        for (int i = 0; i < Layers.Count; i++)
                        {
                            if (Layers[i].Box.MinX < sBox.MinX) { sBox.MinX = Layers[i].Box.MinX; }
                            if (Layers[i].Box.MinY < sBox.MinY) { sBox.MinY = Layers[i].Box.MinY; }
                            if (Layers[i].Box.MaxX > sBox.MaxX) { sBox.MaxX = Layers[i].Box.MaxX; }
                            if (Layers[i].Box.MaxY > sBox.MaxY) { sBox.MaxY = Layers[i].Box.MaxY; }
                        }
                        Box = new RectangleD(sBox);
                    }
                }
            }
            catch { throw new Exception(); }
        }
        /// <summary>
        /// 将地图坐标转换为屏幕坐标
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public PointD FromMapPoint(PointD point)
        {
            PointD sPoint = new PointD();
            Graphics g;
            sPoint.X = (point.X - OffsetX) * DpiX * 39.37 / MapScale;
            sPoint.Y = (OffsetY - point.Y) * DpiY * 39.37 / MapScale;
            return sPoint;
        }


        /// <summary>
        /// 将屏幕坐标转换为地图坐标
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public PointD ToMapPoint(PointD point)
        {
            PointD sPoint = new PointD();
            sPoint.X = point.X * MapScale / (39.37 * DpiX) + OffsetX;
            sPoint.Y = OffsetY - point.Y * MapScale / (39.37 * DpiY);
            return sPoint;
        }


        /// <summary>
        /// 将地图距离转换为屏幕距离
        /// </summary>
        /// <param name="dis"></param>
        /// <returns></returns>
        public double FromMapDistance(double dis) { return dis * 39.37 * DpiX / MapScale; }


        /// <summary>
        /// 将屏幕距离转换为地图距离
        /// </summary>
        /// <param name="dis">屏幕距离对应的DPI</param>
        /// <returns></returns>
        public double ToMapDistance(double dis) { return dis * MapScale / (DpiX * 39.37); }


        /// <summary>
        /// 将地图的显示范围左上角移动到地图坐标的x, y处
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void PanTo(double x, double y)
        {
            OffsetX = x;
            OffsetY = y;
        }


        /// <summary>
        /// 地图的比例尺设为mapscale
        /// </summary>
        /// <param name="mapscale"></param>
        public void SetMapScale(double mapscale) { MapScale = mapscale; }


        /// <summary>
        /// 以指定点为中心，指定缩放系数缩放
        /// </summary>
        /// <param name="point"></param>
        /// <param name="ratio"></param>
        public void ZoomByCenter(PointD center, double ratio)
        {
            //更改比例尺
            double sDisplayScale;
            sDisplayScale = MapScale / ratio;
            MapScale = sDisplayScale;

            //更改投影屏幕左上角坐标
            double sOffsetX, sOffsetY;//
            sOffsetX = OffsetX + (1 - 1 / ratio) * (center.X - OffsetX);
            sOffsetY = OffsetY + (1 - 1 / ratio) * (center.Y - OffsetY);
            OffsetX = sOffsetX;
            OffsetY = sOffsetY;
        }

        /// <summary>
        /// 地图全屏显示
        /// </summary>
        public void FullScreen(double width,double height,RectangleD box)
        {
            if((box .MaxX -box .MinX) >1e-6 && (box.MaxY -box .MinY) > 1e-6)
            {
                //重置offset
                OffsetX = box.MinX;
                OffsetY = box.MaxY;

                //重置比例尺
                double scale_x = (box.MaxX - box.MinX) * (DpiX * 39.37) / width;
                double scale_y = (box.MaxY - box.MinY) * (DpiY * 39.37) / height;
                MapScale = Math.Max(scale_x, scale_y);
            }

        }


        //添加一个图层
        public void AddLayer(Layer layer) { Layers.Insert(0, layer); }


        //删除指定下标的图层
        public void DelLayer(int index)
        {
            Layers.RemoveAt(index);
            if(SelectedLayer == index) { SelectedLayer = -1; }
        }


        //清除所有图层
        public void ClearLayers()
        {
            Layers.Clear();
            SelectedLayer = -1;
        }


        //将指定下标的图层设为选定图层
        public void SelectLayer(int index)
        {
            SelectedLayer = index;
        }


        //将当前选定的图层上移一层
        public void MoveUpLayer(int layerID)
        {
            if (layerID > 0)
            {
                //交换顺序
                Layer layer = Layers[layerID - 1];
                Layers[layerID - 1] = Layers[layerID];
                Layers[layerID] = layer;

                //选择图层处理
                if(SelectedLayer == layerID) { SelectedLayer -= 1; }
                else if(SelectedLayer ==layerID - 1) { SelectedLayer += 1; }
            }

            else { MessageBox.Show("已至最顶层"); }
        }


        //将当前选定图层下移一层
        public void MoveDownLayer(int layerID)
        {
            if (SelectedLayer < Layers.Count - 1)
            {
                //交换顺序
                Layer layer = Layers[layerID + 1];
                Layers[layerID + 1] = Layers[layerID];
                Layers[layerID] = layer;

                //选择图层处理
                if (SelectedLayer == layerID) { SelectedLayer += 1; }
                else if (SelectedLayer == layerID + 1) { SelectedLayer -= 1; }
            }
            else { MessageBox.Show("已至最底层"); }
        }



        /// <summary>
        /// 将当前显示内容绘制到Graphic中
        /// </summary>
        /// <param name="g"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void Render(Graphics g, int width, int height)
        {
            //获得地图显示的box
            double mapWidth = ToMapDistance((double)width);
            double mapHeight = ToMapDistance((double)height);
            RectangleD DrawBox = new RectangleD();
            DrawBox.MinX = OffsetX;
            DrawBox.MaxX = OffsetX + mapWidth;
            DrawBox.MinY = OffsetY - mapHeight;
            DrawBox.MaxY = OffsetY;


            if (Layers.Count > 0)
            {
                for (int i = Layers.Count - 1; i >= 0; i--)
                {
                    //判断绘制的图层与绘制图框是否相交
                    RectangleD box = Layers[i].Box;
                    if (DrawBox.IsPointOn(new PointD(box.MinX, box.MinY)) |
                        DrawBox.IsPointOn(new PointD(box.MinX, box.MaxY)) |
                        DrawBox.IsPointOn(new PointD(box.MaxX, box.MinY)) |
                        DrawBox.IsPointOn(new PointD(box.MaxX, box.MaxY)))
                    { RenderSingleLayer(g, Layers[i]); }
                    if (DrawBox.MaxX < box.MinX || DrawBox.MinX > box.MaxX ||
                        DrawBox.MaxY < box.MinY || DrawBox.MinY > box.MaxY)
                    { continue; }
                    RenderSingleLayer(g, Layers[i]);

                }
            }
        }
        #endregion

        #region 私有函数

        /// <summary>
        /// 渲染单个图层
        /// </summary>
        /// <param name="g"></param>
        /// <param name="layer"></param>
        private void RenderSingleLayer(Graphics g, Layer layer)
        {
            try
            {
                //绘制图层,保证图层可见且有元素
                if (layer.Visible == true && layer.Features.Count > 0)
                {
                    Type renderType = layer.Renderer.GetType();  //获得渲染类型
                    if (renderType == typeof(SimpleRenderer)) { RenderAsSimpleRenderer(g, layer); }
                    else if (renderType == typeof(UniqueValueRenderer)) { RenderAsUniqueValueRenderer(g, layer); }
                    else if (renderType == typeof(ClassBreaksRenderer)) { RenderAsClassBreaksRenderer(g, layer); }
                }

                //绘制注记，保证图层、注记可见且图层有元素
                if (layer.Visible && layer.LabelVisible && layer.Features.Count > 0)
                {
                    RenderLabel(g, layer);
                }
            }
            catch { }

        }


        /// <summary>
        /// 简单渲染单个图层
        /// </summary>
        /// <param name="g"></param>
        /// <param name="layer"></param>
        private void RenderAsSimpleRenderer(Graphics g, Layer layer)
        {
            
            SimpleRenderer sRenderer = layer.Renderer as SimpleRenderer; // 强转，使图层渲染器为唯一值渲染器

            //图层类型为点
            if (layer.FeatureType == typeof(PointD))
            {
                PointSymbol pSymbol = sRenderer.Symbol as PointSymbol;  //强转，使符号为点符号
                for (int i = 0; i < layer.Features.Count; i++)
                {
                    PointD point = layer.Features[i] as PointD;

                    //转换为屏幕坐标,绘制
                    PointD screenPoint = FromMapPoint(point);
                    PointF DrawPoint = new PointF((float)screenPoint.X, (float)screenPoint.Y);
                    pSymbol.DrawPoint(g, DrawPoint);
                }
            }

            //图层类型为折线
            else if (layer.FeatureType == typeof(Polyline))
            {
                LineSymbol lSymbol = sRenderer.Symbol as LineSymbol;  //强转，使符号为线符号
                for (int i = 0; i < layer.Features.Count; i++)
                {
                    Polyline line = layer.Features[i] as Polyline;  //获得折线
                    List<PointF> screenLine = new List<PointF>();

                    for(int j = 0;j<line.Data.Count; j++)
                    {
                        PointD point = FromMapPoint(line.Data[j]);   //坐标转换
                        screenLine.Add(new PointF((float)point.X, (float)point.Y));  //添加到绘制队列
                    }
                    lSymbol.DrawLine(g, screenLine.ToArray());  //绘制
                }
            }

            //图层类型为复合折线
            else if (layer.FeatureType == typeof(MultiPolyline))
            {
                LineSymbol lSymbol = sRenderer.Symbol as LineSymbol;  //强转，使符号为线符号
                for (int i = 0; i < layer.Features.Count; i++)
                {
                    MultiPolyline lines = layer.Features[i] as MultiPolyline;  //获得复合折线
                    for (int j = 0; j < lines.Data.Count; j++)
                    {
                        Polyline line = lines.Data[j];
                        List<PointF> screenLine = new List<PointF>();
                        for (int k = 0; k < line.Data.Count; k++)
                        {
                            PointD point = FromMapPoint(line.Data[k]);   //坐标转换
                            screenLine.Add(new PointF((float)point.X, (float)point.Y));  //添加到绘制队列
                        }
                        lSymbol.DrawLine(g, screenLine.ToArray());  //绘制
                    }
                }
            }

            //图层类型为多边形
            else if (layer.FeatureType == typeof(Polygon))
            {
                PolygonSymbol pSymbol = sRenderer.Symbol as PolygonSymbol;  //强转，使符号为面符号
                for (int i = 0; i < layer.Features.Count; i++)
                {
                    Polygon polygon = layer.Features[i] as Polygon;  //获得折线
                    List<PointF> screenPolygon = new List<PointF>();

                    for (int j = 0; j < polygon.Data.Count; j++)
                    {
                        PointD point = FromMapPoint(polygon.Data[j]);   //坐标转换
                        screenPolygon.Add(new PointF((float)point.X, (float)point.Y));  //添加到绘制队列
                    }
                    pSymbol.DrawPolygon(g, screenPolygon.ToArray());  //绘制
                }

            }

            //图层类型为复合多边形
            else if (layer.FeatureType == typeof(MultiPolygon))
            {
                PolygonSymbol pSymbol = sRenderer.Symbol as PolygonSymbol;  //强转，使符号为线符号
                for (int i = 0; i < layer.Features.Count; i++)
                {
                    MultiPolygon polygons = layer.Features[i] as MultiPolygon;  //获得复合折线
                    for (int j = 0; j < polygons.Data.Count; j++)
                    {
                        Polygon polygon = polygons.Data[j];
                        List<PointF> screenPolygon = new List<PointF>();
                        for (int k = 0; k < polygon.Data.Count; k++)
                        {
                            PointD point = FromMapPoint(polygon.Data[k]);   //坐标转换
                            screenPolygon.Add(new PointF((float)point.X, (float)point.Y));  //添加到绘制队列
                        }
                        pSymbol.DrawPolygon(g, screenPolygon.ToArray());  //绘制
                    }
                }
            }
        }


        /// <summary>
        /// 唯一值渲染单个图层
        /// </summary>
        /// <param name="g"></param>
        /// <param name="layer"></param>
        private void RenderAsUniqueValueRenderer(Graphics g, Layer layer)
        {
            UniqueValueRenderer uRenderer = layer.Renderer as UniqueValueRenderer; // 强转，使图层渲染器为唯一值渲染器
            string field = uRenderer.Field;

            //图层为点图层
            if (layer.FeatureType == typeof(PointD))
            {
                for (int i=0;i<layer .Features.Count; i++)
                {
                    string value = layer.Table.Rows[i][field].ToString();  //获取feature的相应值
                    PointSymbol pSymbol = uRenderer.FindSymbol(value) as PointSymbol;  //获取符号类型
                    PointD point = layer.Features[i] as PointD;   //获取id为i的点

                    //转换为屏幕坐标,绘制
                    PointD screenPoint = FromMapPoint(point);
                    PointF DrawPoint = new PointF((float)screenPoint.X, (float)screenPoint.Y);
                    pSymbol.DrawPoint(g, DrawPoint);
                }
            }

            //图层为折线图层
            else if (layer.FeatureType == typeof(Polyline))
            {
                for(int i = 0;i<layer.Features.Count; i++)
                {
                    string value = layer.Table.Rows[i][field].ToString();  //获取feature的相应值
                    LineSymbol lSymbol = uRenderer.FindSymbol(value) as LineSymbol;  //获取符号类型

                    Polyline line = layer.Features[i] as Polyline;  //获得折线
                    List<PointF> screenLine = new List<PointF>();

                    for (int j = 0; j < line.Data.Count; j++)
                    {
                        PointD point = FromMapPoint(line.Data[j]);   //坐标转换
                        screenLine.Add(new PointF((float)point.X, (float)point.Y));  //添加到绘制队列
                    }
                    lSymbol.DrawLine(g, screenLine.ToArray());  //绘制
                }
            }

            //图层为复合折线图层
            else if (layer.FeatureType == typeof(MultiPolyline))
            {
                for(int i=0;i<layer .Features.Count; i++)
                {
                    string value = layer.Table.Rows[i][field].ToString();  //获取feature的相应值
                    LineSymbol lSymbol = uRenderer.FindSymbol(value) as LineSymbol;  //获取符号类型

                    MultiPolyline lines = layer.Features[i] as MultiPolyline;  //获得复合折线
                    for (int j = 0; j < lines.Data.Count; j++)
                    {
                        Polyline line = lines.Data[j];
                        List<PointF> screenLine = new List<PointF>();
                        for (int k = 0; k < line.Data.Count; k++)
                        {
                            PointD point = FromMapPoint(line.Data[k]);   //坐标转换
                            screenLine.Add(new PointF((float)point.X, (float)point.Y));  //添加到绘制队列
                        }
                        lSymbol.DrawLine(g, screenLine.ToArray());  //绘制
                    }
                }
            }

            //图层为多边形图层
            else if (layer.FeatureType == typeof(Polygon))
            {
                for (int i = 0; i < layer.Features.Count; i++)
                {
                    string value = layer.Table.Rows[i][field].ToString();  //获取feature的相应值
                    PolygonSymbol pSymbol = uRenderer.FindSymbol(value) as PolygonSymbol;  //获取符号类型

                    Polygon polygon = layer.Features[i] as Polygon;  //获得折线
                    List<PointF> screenPolygon = new List<PointF>();

                    for (int j = 0; j < polygon.Data.Count; j++)
                    {
                        PointD point = FromMapPoint(polygon.Data[j]);   //坐标转换
                        screenPolygon.Add(new PointF((float)point.X, (float)point.Y));  //添加到绘制队列
                    }
                    pSymbol.DrawPolygon(g, screenPolygon.ToArray());  //绘制
                }
            }

            //图层为复合多边形图层
            else if (layer.FeatureType == typeof(MultiPolygon))
            {
                for (int i = 0; i < layer.Features.Count; i++)
                {
                    string value = layer.Table.Rows[i][field].ToString();  //获取feature的相应值
                    PolygonSymbol pSymbol = uRenderer.FindSymbol(value) as PolygonSymbol;  //获取符号类型

                    MultiPolygon polygons = layer.Features[i] as MultiPolygon;  //获得复合折线
                    for (int j = 0; j < polygons.Data.Count; j++)
                    {
                        Polygon polygon = polygons.Data[j];
                        List<PointF> screenPolygon = new List<PointF>();
                        for (int k = 0; k < polygon.Data.Count; k++)
                        {
                            PointD point = FromMapPoint(polygon.Data[k]);   //坐标转换
                            screenPolygon.Add(new PointF((float)point.X, (float)point.Y));  //添加到绘制队列
                        }
                        pSymbol.DrawPolygon(g, screenPolygon.ToArray());  //绘制
                    }
                }
            }
        }


        /// <summary>
        /// 分级渲染单个图层
        /// </summary>
        /// <param name="g"></param>
        /// <param name="layer"></param>
        private void RenderAsClassBreaksRenderer(Graphics g, Layer layer)
        {
            ClassBreaksRenderer cRenderer = layer.Renderer as ClassBreaksRenderer; // 强转，使图层渲染器为分级渲染器
            string field = cRenderer.Field;

            //图层为点图层
            if (layer.FeatureType == typeof(PointD))
            {
                for (int i = 0; i < layer.Features.Count; i++)
                {
                    double value = Convert.ToDouble(layer.Table.Rows[i][field]);  //获取feature的相应值
                    PointSymbol pSymbol = cRenderer.FindSymbol(value) as PointSymbol;  //获取符号类型
                    PointD point = layer.Features[i] as PointD;   //获取id为i的点

                    //转换为屏幕坐标,绘制
                    PointD screenPoint = FromMapPoint(point);
                    PointF DrawPoint = new PointF((float)screenPoint.X, (float)screenPoint.Y);
                    pSymbol.DrawPoint(g, DrawPoint);
                }
            }

            //图层为折线图层
            else if (layer.FeatureType == typeof(Polyline))
            {
                for (int i = 0; i < layer.Features.Count; i++)
                {
                    double value = Convert.ToDouble(layer.Table.Rows[i][field]);  //获取feature的相应值
                    LineSymbol lSymbol = cRenderer.FindSymbol(value) as LineSymbol;  //获取符号类型

                    Polyline line = layer.Features[i] as Polyline;  //获得折线
                    List<PointF> screenLine = new List<PointF>();

                    for (int j = 0; j < line.Data.Count; j++)
                    {
                        PointD point = FromMapPoint(line.Data[j]);   //坐标转换
                        screenLine.Add(new PointF((float)point.X, (float)point.Y));  //添加到绘制队列
                    }
                    lSymbol.DrawLine(g, screenLine.ToArray());  //绘制
                }
            }

            //图层为复合折线图层
            else if (layer.FeatureType == typeof(MultiPolyline))
            {
                for (int i = 0; i < layer.Features.Count; i++)
                {
                    double value = Convert.ToDouble(layer.Table.Rows[i][field]);  //获取feature的相应值
                    LineSymbol lSymbol = cRenderer.FindSymbol(value) as LineSymbol;  //获取符号类型

                    MultiPolyline lines = layer.Features[i] as MultiPolyline;  //获得复合折线
                    for (int j = 0; j < lines.Data.Count; j++)
                    {
                        Polyline line = lines.Data[j];
                        List<PointF> screenLine = new List<PointF>();
                        for (int k = 0; k < line.Data.Count; k++)
                        {
                            PointD point = FromMapPoint(line.Data[k]);   //坐标转换
                            screenLine.Add(new PointF((float)point.X, (float)point.Y));  //添加到绘制队列
                        }
                        lSymbol.DrawLine(g, screenLine.ToArray());  //绘制
                    }
                }
            }

            //图层为多边形图层
            else if (layer.FeatureType == typeof(Polygon))
            {
                for (int i = 0; i < layer.Features.Count; i++)
                {
                    double value = Convert.ToDouble(layer.Table.Rows[i][field]);  //获取feature的相应值
                    PolygonSymbol pSymbol =cRenderer.FindSymbol(value) as PolygonSymbol;  //获取符号类型

                    Polygon polygon = layer.Features[i] as Polygon;  //获得折线
                    List<PointF> screenPolygon = new List<PointF>();

                    for (int j = 0; j < polygon.Data.Count; j++)
                    {
                        PointD point = FromMapPoint(polygon.Data[j]);   //坐标转换
                        screenPolygon.Add(new PointF((float)point.X, (float)point.Y));  //添加到绘制队列
                    }
                    pSymbol.DrawPolygon(g, screenPolygon.ToArray());  //绘制
                }
            }

            //图层为复合折线图层
            else if (layer.FeatureType == typeof(MultiPolygon))
            {
                for (int i = 0; i < layer.Features.Count; i++)
                {
                    double value = Convert.ToDouble(layer.Table.Rows[i][field]);  //获取feature的相应值
                    PolygonSymbol pSymbol = cRenderer.FindSymbol(value) as PolygonSymbol;  //获取符号类型

                    MultiPolygon polygons = layer.Features[i] as MultiPolygon;  //获得复合折线
                    for (int j = 0; j < polygons.Data.Count; j++)
                    {
                        Polygon polygon = polygons.Data[j];
                        List<PointF> screenPolygon = new List<PointF>();
                        for (int k = 0; k < polygon.Data.Count; k++)
                        {
                            PointD point = FromMapPoint(polygon.Data[k]);   //坐标转换
                            screenPolygon.Add(new PointF((float)point.X, (float)point.Y));  //添加到绘制队列
                        }
                        pSymbol.DrawPolygon(g, screenPolygon.ToArray());  //绘制
                    }
                }
            }
        }


        private void RenderLabel(Graphics g, Layer layer)
        {
            //获取标注的风格
            LabelStyle lStyle = layer.LabelStyle;
            string sField = lStyle.Field;
            Font sFont = lStyle.Font;
            Brush sBrush = new SolidBrush(lStyle.Color);

            try
            {
                //点绘制注记
                if (layer.FeatureType == typeof(PointD))
                {
                    for (int i = 0; i < layer.Features.Count; i++)
                    {
                        //获取注记位置
                        PointD LabelLocation = new PointD();
                        LabelLocation.X = (layer.Features[i].Box.MinX + layer.Features[i].Box.MaxX) / 2;
                        LabelLocation.Y = (layer.Features[i].Box.MinY + layer.Features[i].Box.MaxY) / 2;
                        LabelLocation = FromMapPoint(LabelLocation);
                        PointF screenLocation = new PointF();
                        screenLocation.X = (float)LabelLocation.X;
                        screenLocation.Y = (float)LabelLocation.Y;

                        //获取注记文本大小，调整注记中心
                        string labelStr = layer.Table.Rows[i][sField].ToString();
                        SizeF labelSize = g.MeasureString(labelStr, sFont);
                        screenLocation.X -= labelSize.Width / 2;
                        screenLocation.Y -= labelSize.Height / 2;
                        screenLocation.Y -= 8;

                        //绘图
                        g.DrawString(labelStr, sFont, sBrush, screenLocation);
                    }
                }

                //折线绘制注记
                else if(layer .FeatureType == typeof (Polyline))
                {
                    for(int i=0;i<layer .Features.Count; i++)
                    {
                        Polyline polyline = layer.Features[i] as Polyline;
                        int midindex = (int)(polyline.Data.Count/2);

                        //获取注记位置,为下标中点对应点
                        PointD LabelLocation = new PointD();
                        LabelLocation.X = polyline.Data[midindex].X;
                        LabelLocation.Y = polyline.Data[midindex].Y;
                        LabelLocation = FromMapPoint(LabelLocation);
                        PointF screenLocation = new PointF();
                        screenLocation.X = (float)LabelLocation.X;
                        screenLocation.Y = (float)LabelLocation.Y;

                        //获取注记文本大小，调整注记中心
                        string labelStr = layer.Table.Rows[i][sField].ToString();
                        SizeF labelSize = g.MeasureString(labelStr, sFont);
                        screenLocation.X -= labelSize.Width / 2;
                        screenLocation.Y -= labelSize.Height / 2;

                        //绘图
                        g.DrawString(labelStr, sFont, sBrush, screenLocation);
                    }
                }

                //复合折线绘制注记
                else if(layer.FeatureType == typeof(MultiPolyline))
                {
                    for (int i = 0; i < layer.Features.Count; i++)
                    {
                        MultiPolyline multiline = layer.Features[i] as MultiPolyline;
                        int midindex = (int)(multiline.Data[0].Data.Count/2);
                        //获取注记位置
                        PointD LabelLocation = new PointD();
                        LabelLocation.X = multiline.Data[0].Data[midindex].X;
                        LabelLocation.Y = multiline.Data[0].Data[midindex].Y;
                        LabelLocation = FromMapPoint(LabelLocation);
                        PointF screenLocation = new PointF();
                        screenLocation.X = (float)LabelLocation.X;
                        screenLocation.Y = (float)LabelLocation.Y;

                        //获取注记文本大小，调整注记中心
                        string labelStr = layer.Table.Rows[i][sField].ToString();
                        SizeF labelSize = g.MeasureString(labelStr, sFont);
                        screenLocation.X -= labelSize.Width / 2;
                        screenLocation.Y -= labelSize.Height / 2;

                        //绘图
                        g.DrawString(labelStr, sFont, sBrush, screenLocation);
                    }
                }

                //面、多面的绘制注记
                else
                {
                    for (int i = 0; i < layer.Features.Count; i++)
                    {
                        //获取注记位置
                        PointD LabelLocation = new PointD();
                        LabelLocation.X = (layer.Features[i].Box.MinX + layer.Features[i].Box.MaxX) / 2;
                        LabelLocation.Y = (layer.Features[i].Box.MinY + layer.Features[i].Box.MaxY) / 2;
                        LabelLocation = FromMapPoint(LabelLocation);
                        PointF screenLocation = new PointF();
                        screenLocation.X = (float)LabelLocation.X;
                        screenLocation.Y = (float)LabelLocation.Y;

                        //获取注记文本大小，调整注记中心
                        string labelStr = layer.Table.Rows[i][sField].ToString();
                        SizeF labelSize = g.MeasureString(labelStr, sFont);
                        screenLocation.X -= labelSize.Width / 2;
                        screenLocation.Y -= labelSize.Height / 2;

                        //绘图
                        g.DrawString(labelStr, sFont, sBrush, screenLocation);
                    }
                }
            }
            catch { throw new Exception(); }
        }

        #endregion 
    }
}
