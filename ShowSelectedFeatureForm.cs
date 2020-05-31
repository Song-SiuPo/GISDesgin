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
    /// <summary>
    /// 鼠标选择要素后，显示被选要素的框
    /// </summary>
    public partial class ShowSelectedFeatureForm : Form
    {
        // 数据
        private Map map;
        private DataTable table = new DataTable();
        private int doubleSelectedItem = -1;

        #region 属性

        /// <summary>
        /// 获取双重选择的要素ID
        /// </summary>
        public int DoubleSelectedItem { get => doubleSelectedItem; }

        public Map LinkMap { set => map = value; }

        #endregion

        #region 方法

        public ShowSelectedFeatureForm(Map _map)
        {
            InitializeComponent();

            map = _map;
            table.Columns.Add("字段", typeof(string));
            table.Columns.Add("属性值", typeof(string));
            dataGridView1.DataSource = table;
        }

        /// <summary>
        /// 刷新选择的要素
        /// </summary>
        public void RenewSelectedItem()
        {
            listBox1.Items.Clear();
            table.Rows.Clear();
            if (map.SelectedLayer == -1) { return; }
            foreach (int item in map.Layers[map.SelectedLayer].SelectedItems)
            {
                listBox1.Items.Add(item);
            }
        }


        #endregion

        #region 事件

        public delegate void SimpleHandler(object sender);

        /// <summary>
        /// 双重选择对象发生改变
        /// </summary>
        public event SimpleHandler DoubleSelectedChanged;

        #endregion

        #region 事件处理

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            table.Rows.Clear();
            // 因清空listbox而改变
            if (listBox1.SelectedIndex == -1)
            {
                doubleSelectedItem = -1;
            }
            else
            {
                doubleSelectedItem = (int)listBox1.SelectedItem;
                DataTable layerTable = map.Layers[map.SelectedLayer].Table;
                DataRow row = null;
                // 找到对应要素
                foreach (DataRow dataRow in map.Layers[map.SelectedLayer].Table.Rows)
                {
                    if ((int)row[0] == doubleSelectedItem)
                    { row = dataRow; break; }
                }
                // 更新显示的Table
                if (row != null)
                {
                    for (int i = 0; i < layerTable.Columns.Count; i++)
                    {
                        DataRow newRow = table.NewRow();
                        newRow[0] = layerTable.Columns[i].ColumnName;
                        newRow[1] = row[i].ToString();
                        table.Rows.Add(newRow);
                    }
                }
            }
            DoubleSelectedChanged?.Invoke(this);
        }

        #endregion
    }
}
