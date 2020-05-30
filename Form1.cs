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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        #region 窗体和控件事件处理

        //CTRL快捷键实现
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (ModifierKeys == Keys.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.N:
                        tsButtonNew.PerformClick();
                        break;
                    case Keys.O:
                        tsButtonOpen.PerformClick();
                        break;
                    case Keys.S:
                        tsButtonSave.PerformClick();
                        break;
                    case Keys.P:
                        tsButtonSavePic.PerformClick();
                        break;
                }
            }

        }

        //文件-新建空白地图
        private void menuItemNewMap_Click(object sender, EventArgs e)
        {

        }

        //文件-打开
        private void menuItemOpen_Click(object sender, EventArgs e)
        {

        }

        //文件-保存
        private void menuItemSave_Click(object sender, EventArgs e)
        {

        }

        //文件-输出为图片
        private void menuItemSavePic_Click(object sender, EventArgs e)
        {

        }

        //编辑-编辑模式
        private void menuItemEditMode_Click(object sender, EventArgs e)
        {

        }

        //编辑-绘制新几何体
        private void menuItemNewGeo_Click(object sender, EventArgs e)
        {

        }

        //编辑-编辑几何体
        private void menuItemEditGeo_Click(object sender, EventArgs e)
        {

        }

        //图层-创建新图层
        private void menuItemNewLayer_Click(object sender, EventArgs e)
        {

        }

        //图层-删除当前图层
        private void menuItemDelLayer_Click(object sender, EventArgs e)
        {

        }

        //图层-打开属性表
        private void menuItemLayerTable_Click(object sender, EventArgs e)
        {

        }

        //图层-设置图层属性
        private void menuItemLayerAttr_Click(object sender, EventArgs e)
        {

        }

        //图层-图层上移
        private void menuItemLayerUp_Click(object sender, EventArgs e)
        {

        }

        //图层-图层下移
        private void menuItemLayerDown_Click(object sender, EventArgs e)
        {

        }

        //选择-鼠标选择几何体
        private void menuItemSelectMouse_Click(object sender, EventArgs e)
        {

        }

        //图层-查询语句选择
        private void menuItemSelectStr_Click(object sender, EventArgs e)
        {

        }

        //图层-选择模式-创建新选择内容
        private void menuItemSelectNew_Click(object sender, EventArgs e)
        {

        }

        //图层-选择模式-与当前选择求并集
        private void menuItemSelectUnion_Click(object sender, EventArgs e)
        {

        }

        //图层-选择模式-从当前选择中去除
        private void menuItemSelectDel_Click(object sender, EventArgs e)
        {

        }

        //图层-选择模式-与当前选择求交集
        private void menuItemSelectIntersect_Click(object sender, EventArgs e)
        {

        }

        //新建
        private void tsButtonNew_Click(object sender, EventArgs e)
        {

        }

        //打开
        private void tsButtonOpen_Click(object sender, EventArgs e)
        {

        }

        //保存地图
        private void tsButtonSave_Click(object sender, EventArgs e)
        {

        }

        //保存为图片
        private void tsButtonSavePic_Click(object sender, EventArgs e)
        {

        }

        //鼠标指针
        private void tsButtonOperateNone_Click(object sender, EventArgs e)
        {

        }

        //漫游
        private void tsButtonPan_Click(object sender, EventArgs e)
        {

        }

        //放大
        private void tsButtonZoomIn_Click(object sender, EventArgs e)
        {

        }

        //缩小
        private void tsButtonZoomOut_Click(object sender, EventArgs e)
        {

        }

        //固定比例缩小
        private void tsButtonZoomScale_Click(object sender, EventArgs e)
        {

        }

        //创建新图层
        private void tsButtonNewLayer_Click(object sender, EventArgs e)
        {

        }

        //选择要素
        private void tsButtonSelect_Click(object sender, EventArgs e)
        {

        }

        //编辑模式
        private void tsButtonEdit_Click(object sender, EventArgs e)
        {

        }

        //创建新要素
        private void tsButtonNewGeo_Click(object sender, EventArgs e)
        {

        }

        //编辑要素几何
        private void tsButtonEditGeo_Click(object sender, EventArgs e)
        {

        }

        #endregion


    }
}
