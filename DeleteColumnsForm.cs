using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;

namespace simpleGIS
{
    public partial class DeleteColumnsForm : Form
    {
        public DeleteColumnsForm()
        {
            InitializeComponent();
        }

        #region 字段

        List<string> _ColumnsToDelete;

        #endregion

        #region 属性

        public List <string > ColumnsToDelete
        {
            get { return _ColumnsToDelete; }
        }

        #endregion


        #region 方法

        //导入字段名列表
        public void GetColumns(List <string> Columns)
        {
            for(int i=0;i<Columns.Count; i++)
            {
                checkedListBox1.Items.Add(Columns[i]);
            }
        }

        #endregion 

        //加载
        private void DeleteColumns_Load(object sender, EventArgs e)
        {
            _ColumnsToDelete = new List<string>();
        }

        //选择要素改变
        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //清空
            _ColumnsToDelete.Clear();

            //更新删除队列
            for(int i=0;i<checkedListBox1 .CheckedItems.Count;i++)
            {
                _ColumnsToDelete.Add(checkedListBox1.CheckedItems[i].ToString());
            } 
        }

        //确定
        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        //取消
        private void btnCencel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
