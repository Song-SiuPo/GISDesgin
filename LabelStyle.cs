using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace simpleGIS
{
    /// <summary>
    /// 注记风格类
    /// </summary>
    public class LabelStyle
    {
        #region 字段

        private string field;
        private Font font;
        private Color color;

        #endregion

        #region 属性

        /// <summary>
        /// 注记绑定字段
        /// </summary>
        public string Field { get => field; set => field = value; }

        /// <summary>
        /// 注记的字体，.Net自带，内部有字体类型，大小，其他特性
        /// </summary>
        public Font Font { get => font; set => font = value; }

        /// <summary>
        /// 注记字符的颜色
        /// </summary>
        public Color Color { get => color; set => color = value; }

        #endregion

        #region 构造函数

        public LabelStyle()
        { }

        /// <summary>
        /// 创建新的注记风格
        /// </summary>
        /// <param name="_field">注记绑定字段</param>
        /// <param name="_font">注记字体</param>
        /// <param name="_color">注记颜色</param>
        public LabelStyle(string _field, Font _font, Color _color)
        {
            field = _field;
            font = _font;
            color = _color;
        }

        #endregion

    }
}