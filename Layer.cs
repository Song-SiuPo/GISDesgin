using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace simpleGIS
{
    public class Layer
    {
        #region 属性

        public string Name { get; set; }   //图层名字
        public bool Visible { get; set; }    //图层是否可见
        public bool IsEdit { get; set; }    //图层是否在编辑状态
        public List<Geometry> Features { get; set; }   //要素集的几何数据
        public Type FeatureType { get; set; }   //要素集Features中元素的几何类型
        public DataTable Table { get; set; }   //要素集的属性数据
        public List<int> SelectedItems{ get; set; }   //选中的要素，int记录其ID
        public RectangleD Box { get; set; }   //图层的外包矩形
        public Renderer Renderer { get; set; }   //图层的渲染类
        public bool LabelVisible { get; set; }   //注记是否可见
        public LabelStyle LabelStyle { get; set; }   //图层的注记风格

        #endregion

        #region 方法

        /// <summary>
        /// 构造函数
        /// </summary>
        public Layer()
        {
            Name = "new_layer";  //默认名称为‘new_layer’
            Visible = true;
            IsEdit = false;
            FeatureType = typeof(PointD);   //默认类型为点
            Box = new RectangleD();
            Renderer = new Renderer();
            LabelVisible = false;
            LabelStyle = new LabelStyle();
            Table.Columns.Add("ID");
            SelectedItems = new List<int>();
        }

        public Layer (Type type)
        {
            Name = "new_layer";  //默认名称为‘new_layer’
            Visible = true;
            IsEdit = false;
            FeatureType = type;
            Box = new RectangleD();
            Renderer = new Renderer();
            LabelVisible = false;
            LabelStyle = new LabelStyle();
            Table.Columns.Add("ID");
            SelectedItems = new List<int>();
        }
        public Layer (string name ,Type type)
        {
            Name = name; 
            Visible = true;
            IsEdit = false;
            FeatureType = type;   //默认类型为点
            Box = new RectangleD();
            Renderer = new Renderer();
            LabelVisible = false;
            LabelStyle = new LabelStyle();
            Table.Columns.Add("ID");
            SelectedItems = new List<int>();
        }


        /// <summary>
        /// 增加几何体
        /// </summary>
        /// <param name="feature"></param>

        public void AddFeature(Geometry feature)
        {
            try
            {
                feature.ID = Features.Count + 1;  //给feature赋ID，默认为当前数组大小加一
                Features.Add(feature);     //数据添加该feature
                Table.Rows.Add(Table.NewRow());
                Table.Rows[feature.ID]["ID"] = feature.ID;   //更新属性表
            }
            catch
            {
                MessageBox.Show("默认增加几何体函数出错");
            }
        }

        public void AddFeature(Geometry feature, DataTable row)
        {
            feature.ID = Features.Count+1;  //给feature赋ID，默认为当前数组大小加一
            try
            {
                Features.Add(feature);     //数组添加该feature
                Table.Rows.Add(row);
                Table.Rows[feature.ID]["ID"] = feature.ID;  //更新属性表
            }
            catch
            {
                MessageBox.Show("添加集合体函数出错");
            }
        }

        //删除指定ID的几何体（并更新属性表）
        public void DelFeature(int id)
        {
            //找到对应位置，删除几何和表中的对应feature
            for(int i=0;i<Features.Count; i++)
            {
                if(Features [i].ID == id)
                {
                    Features.RemoveAt(i);
                    Table.Rows.RemoveAt(i);
                    break;
                }
            }

            //更新id
            for (int i=0;i<Features.Count; i++)
            {
                Features[i].ID = i;
                Table.Rows[i]["ID"] = i;
            }
        }

        //通过属性查询语句查询（SelectedMode是枚举类），并按照SelectedMode更新选中的要素SelectedItems
        public void QuerySQL(string sql, SelectedMode mode)
        {
            //定义变量
            string Property = ""; //属性名
            string Operator = ""; //操作符
            string value = ""; //属性
            List<int> selectID = new List<int>();  //选择的feature ID

            List <char> sql_list = sql.ToList ();
            int mark = 0;
            
            //解析sql，属性名，操作符
            for (; mark < sql_list.Count; mark++)
            {
                if (sql_list[mark] == ' ') { continue; }  //空格，忽略
                else if (sql_list[mark] != '<' | sql_list[mark] != '>' |
                    sql_list[mark] != '=') { Property += sql_list[mark]; }   //没读到操作符，继续
                else if (sql_list[mark] == '<' | sql_list[mark] == '>' |
                    sql_list[mark] == '=')                                    //读到操作符，记录，停止
                {
                    Operator += sql_list[mark];
                    break;
                }
            }

            //解析sql，属性值
            for (;mark < sql_list.Count; mark++)
            {
                if(sql_list[mark] == ' ') { continue; }
                else { value += sql_list[mark]; }
            }

            if(!Table .Columns.Contains(Property)) { MessageBox.Show("不存在属性。"); }
            else
            {
                Type type = Table.Columns[Property].GetType();  //获得属性类型
                if(Operator == "=")   //操作符为‘=’
                {
                    for (int i = 0; i < Table.Rows.Count; i++)
                    {
                        if (Table.Rows[i][Property] == Convert.ChangeType(value, type))  //找到，添加
                            selectID.Add(i);
                    }
                }

                else if (Operator == ">")   //操作符为‘>’
                {
                    try
                    {
                        for (int i = 0; i < Table.Rows.Count; i++)
                        {
                            if ((double)Table.Rows[i][Property] > Convert.ToDouble(value))  //找到，添加
                                selectID.Add(i);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("属性值判别出错，请检查您的sql语句。");
                    }
                }

                else if (Operator == "<")   //操作符为‘<’
                {
                    try
                    {
                        for (int i = 0; i < Table.Rows.Count; i++)
                        {
                            if ((double)Table.Rows[i][Property] < Convert.ToDouble(value))  //找到，添加
                                selectID.Add(i);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("属性值判别出错，请检查您的sql语句。");
                    }
                }
            }

            //按照selectmode，更新选择表

            //选取新的对象
            if (Enum.GetName(typeof(SelectedMode), mode) == "New")
            {
                SelectedItems.Clear();
                SelectedItems.CopyTo(selectID.ToArray());
            }

            //在原基础上添加新选择的对象
            else if (Enum.GetName(typeof(SelectedMode), mode) == "Add")
            {
                for (int i=0;i<selectID.Count; i++)
                {
                    if(!SelectedItems.Exists (t => t==selectID[i])){ SelectedItems.Add(selectID[i]); }
                }
            }

            //在原基础上删除新选择的对象
            else if (Enum.GetName(typeof(SelectedMode), mode) == "Delete")
            {
                for (int i = 0; i < selectID.Count; i++)
                {
                    if (SelectedItems.Exists(t => t == selectID[i])) { SelectedItems.Remove(selectID[i]); }
                }
            }

            //在原基础上删除新选择的对象
            else if (Enum.GetName(typeof(SelectedMode), mode) == "Delete")
            {
                for (int i = 0; i < selectID.Count; i++)
                {
                    if (SelectedItems.Exists(t => t == selectID[i])) { SelectedItems.Remove(selectID[i]); }
                }
            }

            //在原基础上选择对象和新对象的交集
            else if (Enum.GetName(typeof(SelectedMode), mode) == "Intersect")
            {
                for (int i = 0; i < SelectedItems .Count; i++)
                {
                    if (selectID.Exists(t => t == SelectedItems[i])) { SelectedItems.Remove(selectID[i]); }
                }
            }
        }
        #endregion

    }
}
