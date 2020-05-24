using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/// <summary>
/// 几何相关类+外包矩形类
/// </summary>


namespace simpleGIS
{
  /// <summary>
  /// 矩形类——外包矩形、框选矩形
  /// </summary>
    public class RectangleD
    {
        #region 属性
        public double MinX { get; set; }
        public double MinY { get; set; }
        public double MaxX { get; set; }
        public double MaxY { get; set; }
        #endregion
    }
   
    
    /// <summary>
   /// 几何类——基类
   /// </summary>
    public abstract class Geometry
    {
        protected bool needRenewBox = false;
        protected RectangleD box;
        
        #region 属性
        public int ID { get; set; }//唯一值-标明对象id
        public RectangleD Box { get { if (needRenewBox)
                { RenewBox(); }
                return box; }
             set => box = value; }//记录外包矩形
        #endregion

        #region 方法
        /// <summary>
        /// 图形整体平移
        /// </summary>
        /// <param name="deltaX">水平方向移动量</param>
        /// <param name="deltaY">竖直方向移动量</param>
        public abstract void Move(double deltaX, double deltaY);

        /// <summary>
        /// 点选——该point是否能选中该几何体
        /// </summary>
        /// <param name="point">点击点坐标</param>
        /// <returns></returns>
        public abstract bool IsPointOn(PointD point);

        /// <summary>
        /// 框选——该box是否能选中该几何体
        /// </summary>
        /// <param name="box">矩形</param>
        /// <returns></returns>
        public abstract bool IsWithinBox(RectangleD box);

        /// <summary>
        /// 返回Geo的外包矩形——不同类实现方法可能会有所差异
        /// </summary>
        /// <returns></returns>
        public abstract RectangleD OutRectangle();

        #endregion

        #region 私有函数
        /// <summary>
        /// 更新外包矩形
        /// </summary>
        protected abstract void RenewBox();
        #endregion
    }


    /// <summary>
    /// 子类——点
    /// </summary>
    public class PointD:Geometry
    {
        #region 属性
        public double X { get; set; }//点的横坐标
        public double Y { get; set; }
        #endregion

    }
   
    
    /// <summary>
   /// 子类——线
   /// </summary>
    public class Polyline:Geometry
    {
        #region 属性
        public PointD[] Data { get; set; }
        #endregion
    }
  
    
    /// <summary>
   /// 子类——多边形
   /// </summary>
    public class Polygon:Geometry
    {
        #region 属性
        public PointD[] Data { get; set; }
        #endregion
    }
   
    
    
    /// <summary>
    /// 子类——多线
    /// </summary>
    public class MultiPolyline:Geometry
    {
        #region 属性
        public Polyline[] Data { get; set; }
        #endregion
    }
   
    
    
    /// <summary>
    /// 子类——多多边形
    /// </summary>
    public class MultiPolygon:Geometry
    {
        #region 属性
        public Polygon[] Data { get; set; }
        #endregion
    }
}
