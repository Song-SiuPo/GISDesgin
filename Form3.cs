using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;

namespace simpleGIS
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        #region 字段

        private DataTable sTable; //属性表
        private Layer sLayer; //操作图层
        private bool EditCapable; //是否可编辑

        #endregion

        #region 方法

        /// <summary>
        /// 从图层导入属性表
        /// </summary>
        /// <param name="layer"></param>
        public void FromLayerImportTable(Layer layer)
        {
            sLayer = layer;
            sTable = layer.Table.Clone();
        }

        /// <summary>
        /// 保存属性表
        /// </summary>
        /// <returns></returns>
        public void SetTable() { sLayer.Table = sTable; }


        /// <summary>
        /// 重新读取已选择图层，更新选择要素
        /// </summary>
        public void ReReadLayer() { }
        #endregion

        #region 事件

        public delegate void SelectFeatureChangedHandle(object sender);

        /// <summary>
        /// 选择要素发生了改变
        /// </summary>
        public event SelectFeatureChangedHandle SelectFeatureChanged;

        #endregion

        #region 窗体事件处理

        //开始编辑
        private void tmiStartEdit_Click(object sender, EventArgs e)
        {
            tmiEdit.Enabled = true;
            EditCapable = true;  //编辑状态改为可以
            dataGridView1.ReadOnly =false;  //表开始编辑
            sLayer.IsEdit = true;   //图层开始编辑
        }

        //结束编辑
        private void tmiStopEdit_Click(object sender, EventArgs e)
        {
            tmiEdit.Enabled = false;
            EditCapable = false;
            dataGridView1.ReadOnly = true;
        }

        //保存编辑
        private void tmiSaveEdit_Click(object sender, EventArgs e)
        {
            sLayer.Table = sTable.Copy ();
        }

        //取消编辑
        private void tmiCancelEdit_Click(object sender, EventArgs e)
        {
            sTable = sLayer.Table.Copy();
            dataGridView1.DataSource = sTable;
        }


        //增加字段
        private void tmiAddColumn_Click(object sender, EventArgs e)
        {
            if (EditCapable)
            {
                AddFieldFrom af = new AddFieldFrom();

                //默认
                af.Field = "new_Field";
                af.Type = typeof(string);

                if (af.ShowDialog(this) == DialogResult.OK)
                {
                    //临时表增加字段
                    string NewField = af.Field;
                    Type NewFieldType = af.Type;
                    sTable.Columns.Add(NewField, NewFieldType);

                    //更新datagridview
                    dataGridView1.DataSource = sTable;
                    dataGridView1.Refresh();
                }

                //为表头添加右击菜单
                if (dataGridView1.Columns.Count > 0)
                {
                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        dataGridView1.Columns[i].HeaderCell.ContextMenuStrip = contextMenuStrip1;
                    }
                }
            }
        }

        //窗体加载
        private void Form3_Load(object sender, EventArgs e)
        {
            sLayer = new Layer();
            //创建临时表，设置datagridview数据源为临时表
            sTable = new DataTable();
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = sTable;

            //为表头添加右击菜单
            if(dataGridView1 .Columns .Count > 0)
            {
                for(int i=0;i<dataGridView1 .Columns.Count; i++)
                {
                    dataGridView1.Columns[i].HeaderCell.ContextMenuStrip = contextMenuStrip1;
                }
            }

            //快捷图标
            stpSearch.Click += btnSearch_Click;
            btnStartEdit.Click += tmiStartEdit_Click;
            btnSaveEdit.Click += tmiSaveEdit_Click;

        }

        //查询要素
        private void btnSearch_Click(object sender, EventArgs e)
        {
            //查询图层对应的要素
            sLayer.QuerySQL(tbxSQL.Text, SelectedMode.New);

            //datagridview清空选择
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                row.Selected = false;
            }

            //datagridview更新选择
            for (int i = 0; i < sLayer.SelectedItems.Count; i++)
            {
                dataGridView1.Rows[sLayer.SelectedItems[i]].Selected = true;
            }

            //触发联动事件,form1显示选择要素
            SelectFeatureChanged?.Invoke(this);
        }

        //删除行
        private void tmiDeleteRow_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in dataGridView1 .SelectedRows)
            {
                dataGridView1.Rows.Remove(row);
            }
        }

        //选择要素改变
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            //清除之前的要素
            sLayer.SelectedItems.Clear();

            //遍历，找到选择的要素
            for(int i=0;i<sTable .Rows.Count; i++)
            {
                if(dataGridView1 .Rows [i].Selected)
                {
                    int ID = (int)dataGridView1.Rows[i].Cells["ID"].Value;
                    sLayer.SelectedItems.Add(ID);
                }
            }

            //联动，form1显示选择的要素
            SelectFeatureChanged?.Invoke(this);
        }

        //清除选择
        private void 清除选择ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in dataGridView1.SelectedRows)
            {
                row.Selected = false;
            }
        }

        //全选
        private void 全选ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.SelectAll();
        }

        #endregion

        #region 私有函数


        #endregion



        private void 删除列ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteColumnsForm dcfrm = new DeleteColumnsForm();
            List<string> columns = new List<string>();
            foreach(DataColumn co in sTable .Columns)
            {
                columns.Add(co.ToString());
            }
            dcfrm.GetColumns(columns);
            if (dcfrm.ShowDialog(this) == DialogResult.OK)
            {
                List<string> deleteitems = dcfrm.ColumnsToDelete;
                foreach(string item in deleteitems)
                {
                    sTable.Columns.Remove(item);
                }
            }
        }
    }
}
