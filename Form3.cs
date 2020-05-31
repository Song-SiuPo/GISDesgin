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
        private Layer OriginalLayer; //原始图层

        #endregion

        #region 方法

        /// <summary>
        /// 从图层导入属性表
        /// </summary>
        /// <param name="layer"></param>
        public void FromLayerImportTable(Layer layer)
        {
            sLayer = layer;
            OriginalLayer = new Layer(layer);
            sTable = layer.Table.Clone();
            this.Text = layer.Name.ToString()+"属性表";
        }

        /// <summary>
        /// 主窗体选择要素后刷新属性表
        /// </summary>
        public void RefreshSelectFeature()
        {
            //datagridview清空选择
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                row.Selected = false;
            }

            //datagridview更新选择
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                //如果在选择范围
                if (sLayer.SelectedItems.Contains((int)row.Cells["ID"].Value))
                {
                    row.Selected = true;
                }
            }
        }

        #endregion

        #region 事件

        public delegate void SelectFeatureChangedHandle(object sender);

        /// <summary>
        /// 选择要素发生了改变
        /// </summary>
        public event SelectFeatureChangedHandle SelectFeatureChanged;


        public delegate void FeatureBeenDeletedHandle(object sender);

        /// <summary>
        /// 有要素被删除
        /// </summary>
        public event FeatureBeenDeletedHandle FeatureBeenDeleted;

        #endregion

        #region 窗体事件处理

        //开始编辑
        private void tmiStartEdit_Click(object sender, EventArgs e)
        {
            tmiEdit.Enabled = true;
            dataGridView1.ReadOnly =false;  //表开始编辑
            sLayer.IsEdit = true;   //图层开始编辑
        }

        //结束编辑
        private void tmiStopEdit_Click(object sender, EventArgs e)
        {
            tmiEdit.Enabled = false;
            dataGridView1.ReadOnly = true;
            sLayer.IsEdit = false;
        }

        //保存编辑
        private void tmiSaveEdit_Click(object sender, EventArgs e)
        {
            sLayer.Table = sTable.Copy();
        }

        //取消编辑
        private void tmiCancelEdit_Click(object sender, EventArgs e)
        {
            //图层、属性表都恢复到之前的状态
            sLayer = new Layer(OriginalLayer);
            sTable = sLayer.Table.Copy();
        }


        //增加字段
        private void tmiAddColumn_Click(object sender, EventArgs e)
        {
            AddFieldFrom af = new AddFieldFrom();

            //录入结束
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
        }

        //窗体加载
        private void Form3_Load(object sender, EventArgs e)
        {
            sLayer = new Layer();
            //创建临时表，设置datagridview数据源为临时表
            sTable = new DataTable();
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = sTable;

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
            foreach(DataGridViewRow row in  dataGridView1.Rows)
            {
                //如果在选择范围
                if (sLayer.SelectedItems.Contains((int)row.Cells ["ID"].Value))
                {
                    row.Selected = true;
                }
            }

            //触发联动事件,form1显示选择要素
            SelectFeatureChanged?.Invoke(this);
        }

        //删除行
        private void tmiDeleteRow_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in dataGridView1 .SelectedRows)
            {
                //数据表删除对应行
                dataGridView1.Rows.Remove(row);

                //图层删除对应集合体
                int id = (int)row.Cells["ID"].Value;
                sLayer.DelFeature(id);

                //触发委托事件
                FeatureBeenDeleted?.Invoke(this);
            }
        }

        //选择要素改变
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            //清除之前的要素
            sLayer.SelectedItems.Clear();

            //遍历，找到选择的要素
            foreach(DataGridViewRow row in dataGridView1 .SelectedRows)
            {
                int ID = (int)row.Cells["ID"].Value;
                sLayer.SelectedItems.Add(ID);
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

        //删除列操作
        private void 删除列ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //新建删除窗体
            DeleteColumnsForm dcfrm = new DeleteColumnsForm();

            //得到列名列表，传给删除窗体
            List<string> columns = new List<string>();
            foreach (DataColumn co in sTable.Columns)
            {
                columns.Add(co.ToString());
            }
            dcfrm.GetColumns(columns);

            //窗体操作完成
            if (dcfrm.ShowDialog(this) == DialogResult.OK)
            {
                //删除返回的列名
                List<string> deleteitems = dcfrm.ColumnsToDelete;
                foreach (string item in deleteitems)
                {
                    sTable.Columns.Remove(item);
                }
            }
        }
        #endregion
    }
}
