using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace simpleGIS
{
    [Serializable]
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
            Renderer = new SimpleRenderer();
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
            Renderer = new SimpleRenderer();
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
            Renderer = new SimpleRenderer();
            LabelVisible = false;
            LabelStyle = new LabelStyle();
            Table.Columns.Add("ID");
            SelectedItems = new List<int>();
        }

        public Layer (Layer layer)
        {
            Name = layer .Name;
            Visible = layer .Visible;
            IsEdit = layer .IsEdit;
            FeatureType = layer.FeatureType;
            Box = new RectangleD(layer .Box);
            Renderer = layer .Renderer;
            LabelVisible = layer .LabelVisible;
            LabelStyle = layer .LabelStyle;
            Table = layer.Table.Clone();
            SelectedItems = new List<int>(layer.SelectedItems);
        }

        /// <summary>
        /// 更新图层外包矩形
        /// </summary>
        public void RefreshBox()
        {
            try
            {
                //有一个以上的元素
                if(Features .Count > 0)
                {
                    //仅有一个元素
                    if(Features .Count == 1) { Box = new RectangleD (Features [0].Box); }

                    //有一个以上的元素
                    else
                    {
                        RectangleD sBox = new RectangleD(Features[0].Box);  //用来记录临时的外包矩形盒
                        for(int i=1; i<Features.Count; i++)
                        {
                            if (Features[i].Box.MinX < sBox.MinX) { sBox.MinX = Features[i].Box.MinX; }
                            if (Features[i].Box.MinY < sBox.MinY) { sBox.MinY = Features[i].Box.MinY; }
                            if (Features[i].Box.MaxX > sBox.MaxX) { sBox.MaxX = Features[i].Box.MaxX; }
                            if (Features[i].Box.MaxY > sBox.MaxY) { sBox.MaxY = Features[i].Box.MaxY; }
                        }
                        Box = new RectangleD(sBox);
                    }
                }
            }
            catch
            {
                throw new Exception();
            }
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
                    //选择表、feature表、属性表分别删除该元素
                    if(SelectedItems .Contains(id)) { SelectedItems.Remove(id); }
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
            //获得满足条件的feature
            DataRow[] selectedRows = Table.Select(sql);

            //获得满足条件feature的id
            List<int> selectedID = new List<int>();
            for (int i=0;i<selectedRows.Length; i++)
            {
                selectedID.Add((int)selectedRows[i]["ID"]);
            }


            //按照selectmode，更新选择表

            //选取新的对象
            if (mode == SelectedMode.New)
            {
                SelectedItems.Clear();
                SelectedItems = new List<int>(selectedID);
            }

            //在原基础上添加新选择的对象
            else if (mode == SelectedMode.Add)
            {
                HashSet<int> hash1 = new HashSet<int>(SelectedItems);
                HashSet<int> hash2 = new HashSet<int>(selectedID);
                hash1.Union(hash2);
                SelectedItems = new List<int>(hash1); 
            }

            //在原基础上删除新选择的对象
            else if (mode == SelectedMode.Delete)
            {
                HashSet<int> hash1 = new HashSet<int>(SelectedItems);
                HashSet<int> hash2 = new HashSet<int>(selectedID);
                hash1.ExceptWith(hash2);
                SelectedItems = new List<int>(hash1);
            }

            //在原基础上选择对象和新对象的交集
            else if (mode == SelectedMode.Intersect)
            {
                HashSet<int> hash1 = new HashSet<int>(SelectedItems);
                HashSet<int> hash2 = new HashSet<int>(selectedID);
                hash1.IntersectWith(hash2);
                SelectedItems = new List<int>(hash1);
            }
        }
        #endregion

    }
}
