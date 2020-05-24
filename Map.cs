using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

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

        #endregion

        #region 方法


        /// <summary>
        /// 将地图坐标转换为屏幕坐标
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public PointD FromMapPoint(PointD point) { }


        /// <summary>
        /// 将屏幕坐标转换为地图坐标
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public PointD ToMapPoint(PointD point) { }


        /// <summary>
        /// 将地图距离转换为屏幕距离
        /// </summary>
        /// <param name="dis"></param>
        /// <returns></returns>
        public double FromMapDistance(double dis) { }


        /// <summary>
        /// 将屏幕距离转换为地图距离
        /// </summary>
        /// <param name="dis"></param>
        /// <returns></returns>
        public double ToMapDistance(double dis) { }


        /// <summary>
        /// 将地图的显示范围左上角移动到地图坐标的x, y处
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void PanTo(double x, double y) { }


        /// <summary>
        /// 地图的比例尺设为mapscale
        /// </summary>
        /// <param name="mapscale"></param>
        public void SetMapScale(double mapscale) { }


        /// <summary>
        /// 以指定点为中心，指定缩放系数缩放
        /// </summary>
        /// <param name="point"></param>
        /// <param name="ratio"></param>
        public void ZoomByCenter(PointD point, double ratio) { }


        /// <summary>
        /// 将当前显示内容绘制到Graphic中
        /// </summary>
        /// <param name="g"></param>
        public void Render(Graphics g) { }


        //添加一个图层
        public void AddLayer(Layer layer) { }


        //删除指定下标的图层
        public void DelLayer(int index) { }


        //清除所有图层
        public void ClearLayers() { }


        //将指定下标的图层设为选定图层
        public void SelectLayer(int index) { }


        //将当前选定的图层上移一层
        public void MoveUpLayer() { }


        //将当前选定图层下移一层
        public void MoveDownLayer() { }

        #endregion 
    }
}
