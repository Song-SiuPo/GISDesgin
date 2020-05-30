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
    public partial class AddFieldFrom : Form
    {
        public AddFieldFrom()
        {
            InitializeComponent();
        }

        #region 字段

        string _Field;  //属性名称
        Type _Type;  //属性类型

        #endregion

        #region 属性

        public string Field
        {
            get { return _Field; }
        }

        public Type Type
        {
            get { return _Type; }
        }

        #endregion


        #region 窗体事件处理
        private void btnOK_Click(object sender, EventArgs e)
        {
            //当字段不为空并且选择了类型
            if(tbxField .Text != "" && cbxType .SelectedItem != null)
            {
                //获得字段名
                _Field = tbxField.Text;

                //获得字段类型
                if (cbxType.SelectedItem.ToString() == "int")
                    _Type = typeof(int);
                else if (cbxType.SelectedItem.ToString() == "float")
                    _Type = typeof(float);
                else if (cbxType.SelectedItem.ToString() == "double")
                    _Type = typeof(double);
                else if (cbxType.SelectedItem.ToString() == "string")
                    _Type = typeof(string);

                this.DialogResult = DialogResult.OK;
            }
            else { MessageBox.Show("字段名或属性为空。"); }
        }

        //确定
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        //加载
        private void AddFieldFrom_Load(object sender, EventArgs e)
        {
            cbxType.Items.Add("int");
            cbxType.Items.Add("float");
            cbxType.Items.Add("double");
            cbxType.Items.Add("string");

            //默认，文本字段，字段名为‘new_field’
            _Field = "new_field";
            _Type = typeof(string);
            cbxType.SelectedItem = cbxType.Items.IndexOf("string");
            tbxField.Text = "new_Field";
        }

        #endregion


    }
}
