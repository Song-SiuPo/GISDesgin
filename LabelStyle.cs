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
    }
}