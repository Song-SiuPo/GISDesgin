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
    public abstract class Symbol
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

        private static readonly float sqrt3 = (float)Math.Sqrt(3);

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

        /// <summary>
        /// 构造点符号
        /// </summary>
        /// <param name="_pointType">点的符号类型，1~8</param>
        /// <param name="_color">点的颜色</param>
        /// <param name="_size">点的大小，单位像素</param>
        public PointSymbol(int _pointType, Color _color, float _size)
        {
            pointType = _pointType;
            color = _color;
            size = _size;
        }

        /// <summary>
        /// 在绘图上绘制该点符号
        /// </summary>
        /// <param name="g">GDI绘图对象</param>
        /// <param name="x">点中心的屏幕x坐标</param>
        /// <param name="y">点中心的屏幕y坐标</param>
        public void DrawPoint(Graphics g, float x, float y)
        {
            Pen pen = new Pen(color);
            SolidBrush brush = new SolidBrush(color);
            switch (pointType)
            {
                // 空心圆
                case 1:
                    g.DrawEllipse(pen, x - size / 2, y - size / 2, size, size);
                    break;
                // 实心圆
                case 2:
                    g.FillEllipse(brush, x - size / 2, y - size / 2, size, size);
                    break;
                // 空心矩形
                case 3:
                    g.DrawRectangle(pen, x - size / 2, y - size / 2, size, size);
                    break;
                // 实心矩形
                case 4:
                    g.FillRectangle(brush, x - size / 2, y - size / 2, size, size);
                    break;
                // 空心三角
                case 5:
                    PointF[] pointFs = new PointF[3]
                        { new PointF(x, y - size / sqrt3),
                            new PointF(x - size / 2, y + size / 2 / sqrt3),
                            new PointF(x + size / 2, y + size / 2 / sqrt3) };
                    g.DrawPolygon(pen, pointFs);
                    break;
                // 实心三角
                case 6:
                    PointF[] points = new PointF[3]
                        { new PointF(x, y - size / sqrt3),
                            new PointF(x - size / 2, y + size / 2 / sqrt3),
                            new PointF(x + size / 2, y + size / 2 / sqrt3) };
                    g.FillPolygon(brush, points);
                    break;
                // 圈点
                case 7:
                    g.DrawEllipse(pen, x - size / 2, y - size / 2, size, size);
                    g.FillEllipse(brush, x - size / 6, y - size / 6, size / 3, size / 3);
                    break;
                // 双空心圈
                case 8:
                    g.DrawEllipse(pen, x - size / 2, y - size / 2, size, size);
                    g.DrawEllipse(pen, x - size / 4, y - size / 4, size / 2, size / 2);
                    break;
            }
        }
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

        /// <summary>
        /// 构造线符号
        /// </summary>
        /// <param name="pen">线的样式</param>
        public LineSymbol(Pen pen)
        {
            style = pen;
        }
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

        public PolygonSymbol(Pen _outline, SolidBrush _fill)
        {
            outLine = _outline;
            fill = _fill;
        }
    }
}