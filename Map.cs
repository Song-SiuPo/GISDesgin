using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.Remoting.Messaging;

namespace simpleGIS
{
    public class Map
    {
        #region 属性

        public List<Layer> Layers { get; set; }   // 地图的所有图层
        public double OffsetX { get; set; }     // 地图显示范围左上角的地图X坐标
        public double OffsetY { get; set; }     // 地图显示范围左上角的地图Y坐标
        public double MapScale { get; set; }     // 地图的比例尺，比例尺为1:MapScale
        public RectangleD Box { get; set; }     // 全地图的外包矩形
        public int SelectedLayer { get; set; }     // 选定图层的下标
        public double DpiX { get; set; } //屏幕横向分辨率
        public double DpiY { get; set; } //屏幕纵向分辨率

        #endregion

        #region 方法


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
        public double FromMapDistance(double dis) { return dis / MapScale; }


        /// <summary>
        /// 将屏幕距离转换为地图距离
        /// </summary>
        /// <param name="dis"></param>
        /// <returns></returns>
        public double ToMapDistance(double dis) { return dis * MapScale; }


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
        /// 将当前显示内容绘制到Graphic中
        /// </summary>
        /// <param name="g"></param>
        public void Render(Graphics g)
        {
            if (Layers.Count > 0)
            {
                for (int i = Layers.Count - 1; i >= 0; i--) { RenderSingleLayer(g, Layers[i]); }
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
            if(index <Layers .Count & index > 0)
            {
                SelectedLayer = index;
            }
        }


        //将当前选定的图层上移一层
        public void MoveUpLayer(int layerID)
        {
            if (layerID > 0)
            {
                //交换顺序
                Layer layer = new Layer(Layers[layerID - 1]);
                Layers[layerID - 1] = new Layer(Layers[layerID]);
                Layers[layerID] = new Layer(layer);

            }

            else { MessageBox.Show("已至最顶层"); }
        }


        //将当前选定图层下移一层
        public void MoveDownLayer(int layerID)
        {
            if (SelectedLayer < Layers.Count - 1)
            {
                //交换顺序
                Layer layer = new Layer(Layers[layerID + 1]);
                Layers[layerID + 1] = new Layer(Layers[layerID]);
                Layers[layerID] = new Layer(layer);
            }
            else { MessageBox.Show("已至最底层"); }
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
            if (layer.Features.Count > 0)  //保证图层有元素
            {
                Type renderType = layer.Renderer.GetType();  //获得渲染类型
                if (renderType == typeof(SimpleRenderer)) { RenderAsSimpleRenderer(g, layer); }
                else if (renderType == typeof(UniqueValueRenderer)) { RenderAsUniqueValueRenderer(g, layer); }
                else if (renderType == typeof(ClassBreaksRenderer)) { RenderAsClassBreaksRenderer(g, layer); }
            }
        }


        //简单渲染单个图层
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


        //唯一值渲染单个图层
        private void RenderAsUniqueValueRenderer(Graphics g, Layer layer)
        {
            UniqueValueRenderer uRenderer = layer.Renderer as UniqueValueRenderer; // 强转，使图层渲染器为唯一值渲染器
            string field = uRenderer.Field;


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


        //分级渲染单个图层
        private void RenderAsClassBreaksRenderer(Graphics g, Layer layer)
        {
            ClassBreaksRenderer cRenderer = layer.Renderer as ClassBreaksRenderer; // 强转，使图层渲染器为唯一值渲染器
            string field = cRenderer.Field;


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


            else if (layer.FeatureType == typeof(Polygon))
            {
                for (int i = 0; i < layer.Features.Count; i++)
                {
                    double value = Convert.ToDouble(layer.Table.Rows[i][field]);  //获取feature的相应值
                    PolygonSymbol pSymbol =cuRenderer.FindSymbol(value) as PolygonSymbol;  //获取符号类型

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
    }
}
