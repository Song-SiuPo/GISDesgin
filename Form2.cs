using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace simpleGIS
{
    public partial class Form2 : Form
    {
        #region 属性

        /// <summary>
        /// 获取图层的名字
        /// </summary>
        public string LayerName { get => txtBoxLayerName.Text; }

        /// <summary>
        /// 获取图层是否可见
        /// </summary>
        public bool LayerVisible { get => checkBoxVisible.Checked; }

        #endregion

        /// <summary>
        /// 生成图层属性对话框
        /// </summary>
        /// <param name="layer">图层</param>
        public Form2(/*Layer layer*/)
        {
            InitializeComponent();

            // 将Layer的属性值移到界面上
            // 常规
            /*
            txtBoxLayerName.Text = layer.Name;
            checkBoxLabelVisible.Checked = layer.Visible;

            // 渲染
            if (layer.Renderer.GetType() == typeof(SimpleRenderer))
            {
                rbSimple.Checked = true;
            }
            else if (layer.Renderer.GetType() == typeof(UniqueValueRenderer))
            {
                rbUniqueValue.Checked = true;
            }*/
            panelColumns.Visible = true;
            panelBreakPoints.Visible = true;
            groupPolyline.Visible = true;
        }
    }
}
