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
        #region 属性

        public int PointType { get; set; }  // 点符号的具体形状
        public Color Color { get; set; }    // 点的颜色
        public float Size { get; set; }     // 点的大小（单位为像素）

        #endregion
    }

    /// <summary>
    /// 线标记类
    /// </summary>
    public class LineSymbol : Symbol
    {
        #region 属性

        public Pen Style { get; set; }  // 线的符号类型，.Net自带，里面有线型，颜色，宽度

        #endregion
    }

    /// <summary>
    /// 面标记类
    /// </summary>
    public class PolygonSymbol : Symbol
    {
        #region 属性

        public Pen Outline { get; set; }        // 多边形的边界样式
        public SolidBrush Fill { get; set; }    // 多边形内部填充样式

        #endregion
    }
}