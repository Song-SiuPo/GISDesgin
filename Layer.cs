using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

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
        public List<int> SeletedItems{ get; set; }   //选中的要素，int记录其ID
        public RectangleD Box { get; set; }   //图层的外包矩形
        public Renderer Renderer { get; set; }   //图层的渲染类
        public bool LabelVisible { get; set; }   //注记是否可见
        public LabelStyle LabelStyle { get; set; }   //图层的注记风格

        #endregion

        #region 方法

        //增加几何体（记得属性表也要加）
        public void AddFeature(Geometry feature) { }

        //删除指定ID的几何体（并更新属性表）
        public void DelFeature(int id) { }

        //通过属性查询语句查询（SelectedMode是枚举类），并按照SelectedMode更新选中的要素SelectedItems
        public void QuerySQL(string sql, SelectedMode mode) { }
        #endregion

    }
}
