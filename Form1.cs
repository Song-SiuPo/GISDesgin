using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace simpleGIS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            clboxLayersUpdata();
        }

        #region 私有函数

        //clboxLayers数据同步
        private void clboxLayersUpdata()
        {
            clboxLayers.Items.Clear();
            for (int i = 0; i < mapControl1.Map.Layers.Count; i++)
            {
                clboxLayers.Items.Add(mapControl1.Map.Layers[i]);
                clboxLayers.SetItemChecked(i, mapControl1.Map.Layers[i].Visible);
            }
            clboxLayers.Refresh();
        }

        //clboxLayers
        private void clboxLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Selected
            mapControl1.Map.SelectLayer(clboxLayers.SelectedIndex);
            //Checked
            mapControl1.Map.Layers[clboxLayers.SelectedIndex].Visible =
                clboxLayers.GetItemChecked(clboxLayers.SelectedIndex);
        }

        #endregion

        #region 窗体和控件事件处理

        //文件-新建空白地图
        private void menuItemNewMap_Click(object sender, EventArgs e)
        {
            if (mapControl1.NeedSave)
                MessageBox.Show(this, "当前有未保存的更改。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                mapControl1.NewMap();
                menuItemSave.PerformClick();
                mapControl1.Refresh();
            }
        }

        //文件-打开
        private void menuItemOpen_Click(object sender, EventArgs e)
        {
            if (mapControl1.NeedSave)
                MessageBox.Show(this, "当前有未保存的更改。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "SimpleGIS文件(*.spgis*)|*.spgis|所有文件(*.*)|*.*";
                openFileDialog.RestoreDirectory = true;
                openFileDialog.Multiselect = false;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string openFileName = openFileDialog.FileName.ToString();
                    mapControl1.OpenFile(openFileName);
                    mapControl1.Refresh();
                }
            }

        }

        //文件-保存
        private void menuItemSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "SimpleGIS文件(*.spgis*)|*.spgis|所有文件(*.*)|*.*";
            saveFileDialog.AddExtension = true;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string saveFileName = saveFileDialog.FileName.ToString();
                mapControl1.SaveFile(saveFileName);
            }
        }

        //文件-输出为图片
        private void menuItemSavePic_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "BMP文件(*.BMP*)|*.BMP|JPG文件(*.jpg*)|*.jpg|PNG文件(*.png*)|*.png|所有文件(*.*)|*.*";
            saveFileDialog.AddExtension = true;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string saveFileName = saveFileDialog.FileName.ToString();
                mapControl1.SaveBitmap(saveFileName);
            }
        }


        //编辑-编辑模式
        private void menuItemEditMode_Click(object sender, EventArgs e)
        {
            //bool judge = menuItemEditMode
            if (menuItemEditMode.Checked == true)//checkonclick
            {
                menuItemEditGeo.Enabled = true;
                menuItemNewGeo.Enabled = true;
                //tsButtonEdit_Click(this, e);
                tsButtonEdit.Checked = true;
                tsButtonEditGeo.Enabled = true;
                tsButtonNewGeo.Enabled = true;

            }
            else if (menuItemEditMode.Checked == false)
            {
                menuItemEditGeo.Enabled = false;
                menuItemNewGeo.Enabled = false;
                tsButtonEdit.Checked = false;
                tsButtonEditGeo.Enabled = false;
                tsButtonNewGeo.Enabled = false;

            }
            //Refresh();
        }

        //编辑-绘制新几何体
        private void menuItemNewGeo_Click(object sender, EventArgs e)
        {
            mapControl1.OperationType = OperationType.Track;
        }

        //编辑-编辑几何体
        private void menuItemEditGeo_Click(object sender, EventArgs e)
        {
            mapControl1.OperationType = OperationType.Edit;
        }

        //图层-创建新图层
        private void menuItemNewLayer_Click(object sender, EventArgs e)
        {
            mapControl1.Map.AddLayer(new Layer());
            clboxLayersUpdata();
            mapControl1.Refresh();
        }

        //图层-删除当前图层
        private void menuItemDelLayer_Click(object sender, EventArgs e)
        {
            mapControl1.Map.DelLayer(mapControl1.Map.SelectedLayer);
            clboxLayers.Items.RemoveAt(mapControl1.Map.SelectedLayer);
            mapControl1.Refresh();
        }

        //图层-打开属性表
        private void menuItemLayerTable_Click(object sender, EventArgs e)
        {
            Form3 frm3 = new Form3();
            int id = mapControl1.Map.SelectedLayer;
            frm3.FromLayerImportTable(mapControl1.Map.Layers[id]);
            if (frm3.ShowDialog(this) == DialogResult.OK)
                mapControl1.Refresh();
            frm3.Dispose();
        }

        //图层-设置图层属性
        private void menuItemLayerAttr_Click(object sender, EventArgs e)
        {
            int id = mapControl1.Map.SelectedLayer;
            Form2 frm2 = new Form2(mapControl1.Map.Layers[id]);
            if (frm2.ShowDialog(this) == DialogResult.OK)
                mapControl1.Refresh();
            frm2.Dispose();
        }

        //图层-图层上移
        private void menuItemLayerUp_Click(object sender, EventArgs e)
        {
            mapControl1.Map.MoveUpLayer(mapControl1.Map.SelectedLayer);
            clboxLayersUpdata();
            mapControl1.Refresh();
        }

        //图层-图层下移
        private void menuItemLayerDown_Click(object sender, EventArgs e)
        {
            mapControl1.Map.MoveDownLayer(mapControl1.Map.SelectedLayer);
            clboxLayersUpdata();
            mapControl1.Refresh();
        }

        //选择-鼠标选择几何体
        private void menuItemSelectMouse_Click(object sender, EventArgs e)
        {
            mapControl1.OperationType = OperationType.Select;
        }

        //图层-查询语句选择
        private void menuItemSelectStr_Click(object sender, EventArgs e)
        {
            int id = mapControl1.Map.SelectedLayer;
            Layer nowlayer = mapControl1.Map.Layers[id];
            String SQLstr = Interaction.InputBox("请输入sql语句查询", "查询语句进行选择", "null", -1, -1);
            //Console.WriteLine(SQLstr);
            nowlayer.QuerySQL(SQLstr, mapControl1.SelectedMode);
        }

        //图层-选择模式-创建新选择内容
        private void menuItemSelectNew_Click(object sender, EventArgs e)
        {
            mapControl1.SelectedMode = SelectedMode.New;
        }

        //图层-选择模式-与当前选择求并集
        private void menuItemSelectUnion_Click(object sender, EventArgs e)
        {
            mapControl1.SelectedMode = SelectedMode.Add;
        }

        //图层-选择模式-从当前选择中去除
        private void menuItemSelectDel_Click(object sender, EventArgs e)
        {
            mapControl1.SelectedMode = SelectedMode.Delete;
        }

        //图层-选择模式-与当前选择求交集
        private void menuItemSelectIntersect_Click(object sender, EventArgs e)
        {
            mapControl1.SelectedMode = SelectedMode.Intersect;
        }

        //新建
        private void tsButtonNew_Click(object sender, EventArgs e)
        {
            menuItemNewMap.PerformClick();
        }

        //打开
        private void tsButtonOpen_Click(object sender, EventArgs e)
        {
            menuItemOpen.PerformClick();
        }

        //保存地图
        private void tsButtonSave_Click(object sender, EventArgs e)
        {
            menuItemSave.PerformClick();
        }

        //保存为图片
        private void tsButtonSavePic_Click(object sender, EventArgs e)
        {
            menuItemSavePic.PerformClick();
        }

        //鼠标指针
        private void tsButtonOperateNone_Click(object sender, EventArgs e)
        {
            mapControl1.OperationType = OperationType.None;
        }

        //漫游
        private void tsButtonPan_Click(object sender, EventArgs e)
        {
            mapControl1.OperationType = OperationType.Pan;
        }

        //放大
        private void tsButtonZoomIn_Click(object sender, EventArgs e)
        {
            mapControl1.OperationType = OperationType.ZoomIn;
        }

        //缩小
        private void tsButtonZoomOut_Click(object sender, EventArgs e)
        {
            mapControl1.OperationType = OperationType.ZoomOut;
        }

        //全屏显示--弃
        private void tsButtonZoomScale_Click(object sender, EventArgs e)
        {
            //PointD CenterPoint = new PointD(mapControl1.Width / 2, mapControl1.Height / 2);
            //mapControl1.Map.ZoomByCenter(CenterPoint, 1.2f);

            //mapControl1.Refresh();
            /*
            RectangleD box = mapControl1.Map.Layers[mapControl1.Map.SelectedLayer].Box;
            PointD centerPoint = new PointD();
            centerPoint.X = (double)(box.MinX + box.MaxX) / 2;
            centerPoint.Y = (double)(box.MinY + box.MaxY) / 2;
            double xdis = mapControl1.Map.ToMapDistance(this.Width);
            double ydis = mapControl1.Map.ToMapDistance(this.Height);
            */
        }

        //创建新图层
        private void tsButtonNewLayer_Click(object sender, EventArgs e)
        {
            menuItemNewLayer.PerformClick();
        }

        //选择要素
        private void tsButtonSelect_Click(object sender, EventArgs e)
        {
            mapControl1.OperationType = OperationType.Select;
        }

        //编辑模式
        private void tsButtonEdit_Click(object sender, EventArgs e)
        {
            if (tsButtonEdit.Checked)
            {
                tsButtonEditGeo.Enabled = true;
                tsButtonNewGeo.Enabled = true;
                menuItemEditMode.Checked = true;
                menuItemEditGeo.Enabled = true;
                menuItemNewGeo.Enabled = true;

            }
            else
            {
                tsButtonEditGeo.Enabled = false;
                tsButtonNewGeo.Enabled = false;
                menuItemEditMode.Checked = false;
                menuItemEditGeo.Enabled = false;
                menuItemNewGeo.Enabled = false;
            }
        }

        //创建新要素
        private void tsButtonNewGeo_Click(object sender, EventArgs e)
        {
            mapControl1.OperationType = OperationType.Track;
        }

        //编辑要素几何
        private void tsButtonEditGeo_Click(object sender, EventArgs e)
        {
            mapControl1.OperationType = OperationType.Edit;
        }


        #endregion

    }
}
