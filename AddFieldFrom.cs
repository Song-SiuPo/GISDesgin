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
            set { _Field = value; }
        }

        public Type Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        #endregion


        #region 窗体事件处理
        private void btnOK_Click(object sender, EventArgs e)
        {
            //当字段不为空并且选择了类型
            if(tbxField .Text != "" && cbxType .SelectedItem != null)
            {
                //获得字段名
                Field = tbxField.Text;

                //获得字段类型
                if (cbxType.SelectedItem.ToString() == "int")
                    Type = typeof(int);
                else if (cbxType.SelectedItem.ToString() == "float")
                    Type = typeof(float);
                else if (cbxType.SelectedItem.ToString() == "double")
                    Type = typeof(double);
                else if (cbxType.SelectedItem.ToString() == "string")
                    Type = typeof(string);

                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void AddFieldFrom_Load(object sender, EventArgs e)
        {
            cbxType.Items.Add("int");
            cbxType.Items.Add("float");
            cbxType.Items.Add("double");
            cbxType.Items.Add("string");
        }

        #endregion


    }
}
