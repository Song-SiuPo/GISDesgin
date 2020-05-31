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
    public partial class Form4 : Form
    {
        #region 属性

        /// <summary>
        /// 图层的名字
        /// </summary>
        public string LayerName { get; private set; }

        /// <summary>
        /// 图层类型
        /// </summary>
        public Type LayerType { get; private set; }

        #endregion

        public Form4()
        {
            InitializeComponent();

            LayerName = textBox1.Text;
            LayerType = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (LayerName == "" || LayerName.All((char c) => c == ' '))
            {
                MessageBox.Show(this, "输入的图层名不能为空。", "图层名错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (LayerType == null)
            {
                MessageBox.Show(this, "请选择图层类型", "图层类型错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else { DialogResult = DialogResult.OK; }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            LayerName = textBox1.Text;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox3.SelectedIndex)
            {
                case 0:
                    LayerType = typeof(PointD);
                    break;
                case 1:
                    LayerType = typeof(Polyline);
                    break;
                case 2:
                    LayerType = typeof(Polygon);
                    break;
                case 3:
                    LayerType = typeof(MultiPolyline);
                    break;
                case 4:
                    LayerType = typeof(MultiPolygon);
                    break;
                default:
                    LayerType = null;
                    break;
            }
        }
    }
}
