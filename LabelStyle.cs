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
        #region 属性

        public string Field { get; set; }     // 注记绑定字段
        public Font Font { get; set; }     // 注记的字体，.Net自带，内部有字体类型，大小，其他特性
        public Color Color { get; set; }     // 注记字符的颜色

        #endregion
    }
}