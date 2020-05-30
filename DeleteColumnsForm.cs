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

        public void GetColumns(List <string> Columns)
        {
            for(int i=0;i<Columns.Count; i++)
            {
                checkedListBox1.Items.Add(Columns[i]);
            }
        }

        #endregion 

        private void DeleteColumns_Load(object sender, EventArgs e)
        {
            _ColumnsToDelete = new List<string>();
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _ColumnsToDelete.Clear();
            for(int i=0;i<checkedListBox1 .CheckedItems.Count;i++)
            {
                _ColumnsToDelete.Add(checkedListBox1.CheckedItems[i].ToString());
            } 
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnCencel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
