using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace simpleGIS
{
    /// <summary>
    /// 符号标记类
    /// </summary>
    public class Symbol
    {

    }

    /// <summary>
    /// 点标记类
    /// </summary>
    public class PointSymbol : Symbol
    {
        #region 字段

        private int pointType;
        private Color color;
        private float size;

        #endregion

        #region 属性

        /// <summary>
        /// 点符号的具体形状
        /// </summary>
        public int PointType { get => pointType; set => pointType = value; }
        
        /// <summary>
        /// 点的颜色
        /// </summary>
        public Color Color { get => color; set => color = value; }
        
        /// <summary>
        /// 点的大小（单位为像素）
        /// </summary>
        public float Size { get => size; set => size = value; }

        #endregion
    }

    /// <summary>
    /// 线标记类
    /// </summary>
    public class LineSymbol : Symbol
    {
        #region 字段

        private Pen style;

        #endregion

        #region 属性

        /// <summary>
        /// 线的符号类型，.Net自带，里面有线型，颜色，宽度
        /// </summary>
        public Pen Style { get => style; set => style = value; }

        #endregion
    }

    /// <summary>
    /// 面标记类
    /// </summary>
    public class PolygonSymbol : Symbol
    {
        #region 字段

        private Pen outLine;
        private SolidBrush fill;

        #endregion

        #region 属性

        /// <summary>
        /// 多边形的边界样式
        /// </summary>
        public Pen OutLine { get => outLine; set => outLine = value; }

        /// <summary>
        /// 多边形内部填充样式
        /// </summary>
        public SolidBrush Fill { get => fill; set => fill = value; }

        #endregion
    }
}