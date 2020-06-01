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
        // 显示选择对象的窗口
        private ShowSelectedFeatureForm showFeatureForm;

        //显示图层的属性表
        private Form3 frm3;

        public Form1()
        {
            InitializeComponent();
            clboxLayersUpdata();

            tslScale.Text = "比例尺:  1:" + mapControl1.Map.MapScale.ToString("G8");
        }

        #region 私有函数

        //clboxLayers数据同步
        private void clboxLayersUpdata()
        {
            int index = mapControl1.Map.SelectedLayer;
            clboxLayers.Items.Clear();
                for (int i = 0; i < mapControl1.Map.Layers.Count; i++)
            { 
                clboxLayers.Items.Add(mapControl1.Map.Layers[i].Name);
                clboxLayers.SetItemChecked(i, mapControl1.Map.Layers[i].Visible);
            }
            clboxLayers.SelectedIndex = index;
            clboxLayers.Refresh();
        }

        //clboxLayers_Select
        private void clboxLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            mapControl1.Map.SelectLayer(clboxLayers.SelectedIndex);
            if (clboxLayers.SelectedIndex == -1)
            {
                menuItemEditMode.Enabled = false;
                menuItemDelLayer.Enabled = false;
                menuItemLayerTable.Enabled = false;
                menuItemLayerAttr.Enabled = false;
                menuItemLayerUp.Enabled = false;
                menuItemLayerDown.Enabled = false;
                menuItemSelectMouse.Enabled = false;
                menuItemSelectStr.Enabled = false;
                tsButtonZoomScale.Enabled = false;
                tsButtonEdit.Enabled = false;
            }
            else
            {
                Layer layer = mapControl1.Map.Layers[mapControl1.Map.SelectedLayer];
                menuItemDelLayer.Enabled = true;
                menuItemLayerTable.Enabled = true;
                menuItemLayerAttr.Enabled = true;
                menuItemSelectStr.Enabled = true;
                tsButtonZoomScale.Enabled = true;
                menuItemEditMode.Enabled = layer.Visible;
                menuItemSelectMouse.Enabled = layer.Visible;
                tsButtonEdit.Enabled = layer.Visible;
                menuItemLayerUp.Enabled = mapControl1.Map.SelectedLayer != 0;
                menuItemLayerDown.Enabled = mapControl1.Map.SelectedLayer != mapControl1.Map.Layers.Count - 1;
                mapControl1.Invalidate();
            }
            if (showFeatureForm != null && !showFeatureForm.IsDisposed)
            { showFeatureForm.RenewSelectedItem(); }
        }

        //clboxLayers_Check
        private void clboxLayers_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (mapControl1.Map.Layers[e.Index].Visible != (e.NewValue == CheckState.Checked))
            {
                mapControl1.Map.Layers[e.Index].Visible = (e.NewValue == CheckState.Checked);
                mapControl1.SetNeedRefreshBase();
                mapControl1.Refresh();
            }
            if (e.Index == mapControl1.Map.SelectedLayer)
            {
                Layer layer = mapControl1.Map.Layers[e.Index];
                menuItemEditMode.Enabled = layer.Visible;
                menuItemSelectMouse.Enabled = layer.Visible;
                tsButtonEdit.Enabled = layer.Visible;
            }
        }

        // 弹出选择文件框并保存文件
        private bool TrySaveFile(IWin32Window window)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "SimpleGIS文件(*.spgis*)|*.spgis|所有文件(*.*)|*.*";
            saveFileDialog.AddExtension = true;
            bool result = false;
            if (saveFileDialog.ShowDialog(window) == DialogResult.OK)
            {
                string saveFileName = saveFileDialog.FileName.ToString();
                mapControl1.SaveFile(saveFileName);
                result = true;
            }
            saveFileDialog.Dispose();
            return result;
        }

        //属性表选择几何体，控件响应
        private void RefreshSelectFeatureOfMap(object sender)
        {
            if (showFeatureForm != null && !showFeatureForm.IsDisposed)
            {
                showFeatureForm.RenewSelectedItem();
            }
            mapControl1.Refresh();
        }

        //属性表删除集合体，控件重绘
        private void RefreshAfterDelete(object sender)
        {
            if (showFeatureForm != null && !showFeatureForm.IsDisposed)
            {
                showFeatureForm.RenewSelectedItem();
            }
            mapControl1.SetNeedRefreshBase();
            mapControl1.Refresh();
        }

        //控件选择几何体，属性表响应
        private void RefreshSelectedFromMapControl(object sender)
        {
            if(frm3 != null && !frm3.IsDisposed) { frm3.RefreshSelectFeature(); }
            if (showFeatureForm != null && !showFeatureForm.IsDisposed)
            {
                showFeatureForm.RenewSelectedItem();
            }
        }

        private void ShowFeatureForm_DoubleSelectedChanged(object sender)
        {
            mapControl1.DoubleSelectedItem = showFeatureForm.DoubleSelectedItem;
            mapControl1.Refresh();
        }

        #endregion

        #region 窗体和控件事件处理

        //文件-新建空白地图
        private void menuItemNewMap_Click(object sender, EventArgs e)
        {
            if (mapControl1.NeedSave)
            {
                DialogResult result = MessageBox.Show(this, "当前有未保存的更改，是否保存？", "保存地图",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Asterisk);
                bool try_result = true;
                if (result == DialogResult.Yes)
                {
                    try_result = TrySaveFile(this);
                }
                if (result == DialogResult.Cancel || try_result == false) { return; }
            }
            mapControl1.NewMap();
            if (showFeatureForm != null && !showFeatureForm.IsDisposed)
            {
                showFeatureForm.Close();
                showFeatureForm.Dispose();
            }
            clboxLayersUpdata();
            mapControl1.Refresh();
        }

        //文件-打开
        private void menuItemOpen_Click(object sender, EventArgs e)
        {
            if (mapControl1.NeedSave)
            {
                DialogResult result = MessageBox.Show(this, "当前有未保存的更改，是否保存？", "保存地图",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Asterisk);
                bool try_result = true;
                if (result == DialogResult.Yes)
                {
                    try_result = TrySaveFile(this);
                }
                if (result == DialogResult.Cancel || try_result == false) { return; }
            }
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "SimpleGIS文件(*.spgis*)|*.spgis|所有文件(*.*)|*.*";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string openFileName = openFileDialog.FileName.ToString();
                try
                {
                    mapControl1.OpenFile(openFileName);
                    if (showFeatureForm != null && !showFeatureForm.IsDisposed)
                    {
                        showFeatureForm.Close();
                        showFeatureForm.Dispose();
                    }
                    mapControl1.Refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, "错误", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            openFileDialog.Dispose();
            clboxLayersUpdata();
        }

        //文件-保存
        private void menuItemSave_Click(object sender, EventArgs e)
        {
            TrySaveFile(this);
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
            saveFileDialog.Dispose();
        }


        //编辑-编辑模式
        private void menuItemEditMode_Click(object sender, EventArgs e)
        {
            //bool judge = menuItemEditMode
            if (mapControl1.Map.SelectedLayer == -1)
            {
                throw new Exception("错误：没有可以编辑的图层");
            }
            Layer layer = mapControl1.Map.Layers[mapControl1.Map.SelectedLayer];
            layer.IsEdit = !layer.IsEdit;
            if (layer.IsEdit)//checkonclick
            {
                menuItemEditGeo.Enabled = true;
                menuItemNewGeo.Enabled = true;
                //tsButtonEdit_Click(this, e);
                tsButtonEdit.Checked = true;
                menuItemEdit.Checked = true;
                tsButtonEditGeo.Enabled = true;
                tsButtonNewGeo.Enabled = true;
                clboxLayers.Enabled = false;
            }
            else
            {
                menuItemEditGeo.Enabled = false;
                menuItemNewGeo.Enabled = false;
                tsButtonEdit.Checked = false;
                menuItemEdit.Checked = false;
                tsButtonEditGeo.Enabled = false;
                tsButtonNewGeo.Enabled = false;
                clboxLayers.Enabled = true;
                if (mapControl1.OperationType == OperationType.Edit ||
                    mapControl1.OperationType == OperationType.Track)
                { mapControl1.OperationType = OperationType.None; }
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
            Form4 form4 = new Form4();
            if (form4.ShowDialog(this) == DialogResult.OK)
            {
                mapControl1.Map.AddLayer(new Layer(form4.LayerName, form4.LayerType));
                clboxLayersUpdata();
                mapControl1.SetNeedRefreshBase();
                mapControl1.Refresh();
            }
            form4.Dispose();
        }

        //图层-删除当前图层
        private void menuItemDelLayer_Click(object sender, EventArgs e)
        {
            clboxLayers.Items.RemoveAt(mapControl1.Map.SelectedLayer);
            mapControl1.Map.DelLayer(mapControl1.Map.SelectedLayer);
            mapControl1.SetNeedRefreshBase();
            mapControl1.Refresh();
        }

        //图层-打开属性表
        private void menuItemLayerTable_Click(object sender, EventArgs e)
        {
            //form3没有创建
            if(frm3 == null)
            {
                //新建，并绑定事件
                frm3 = new Form3();
                frm3.SelectFeatureChanged += RefreshSelectFeatureOfMap;
                frm3.FeatureBeenDeleted += RefreshAfterDelete;

                //传入图层，打开窗口
                int id = mapControl1.Map.SelectedLayer;
                frm3.FromLayerImportTable(mapControl1.Map.Layers[id]);
                frm3.Show();
            }

            //form3被创建但已关闭
            else if (frm3!=null && frm3.IsDisposed)
            {
                //重建，并绑定事件
                frm3.Dispose();
                frm3 = new Form3();
                frm3.SelectFeatureChanged += RefreshSelectFeatureOfMap;
                frm3.FeatureBeenDeleted += RefreshAfterDelete;

                //传入图层打开窗口
                int id = mapControl1.Map.SelectedLayer;
                frm3.FromLayerImportTable(mapControl1.Map.Layers[id]);
                frm3.Show();
            }

            //form3已创建并已打开
            else if(frm3!=null && !frm3.IsDisposed)
            {
                //传入图层，打开窗口
                int id = mapControl1.Map.SelectedLayer;
                frm3.FromLayerImportTable(mapControl1.Map.Layers[id]);
                frm3.Activate();
            }

        }

        //图层-设置图层属性
        private void menuItemLayerAttr_Click(object sender, EventArgs e)
        {
            int id = mapControl1.Map.SelectedLayer;
            Form2 frm2 = new Form2(mapControl1.Map.Layers[id]);
            if (frm2.ShowDialog(this) == DialogResult.OK)
            {
                clboxLayersUpdata();
                mapControl1.SetNeedRefreshBase();
                mapControl1.Refresh();
            }
            frm2.Dispose();
        }

        //图层-图层上移
        private void menuItemLayerUp_Click(object sender, EventArgs e)
        {
            mapControl1.Map.MoveUpLayer(mapControl1.Map.SelectedLayer);
            clboxLayersUpdata();
            mapControl1.SetNeedRefreshBase();
            mapControl1.Refresh();
        }

        //图层-图层下移
        private void menuItemLayerDown_Click(object sender, EventArgs e)
        {
            mapControl1.Map.MoveDownLayer(mapControl1.Map.SelectedLayer);
            clboxLayersUpdata();
            mapControl1.SetNeedRefreshBase();
            mapControl1.Refresh();
        }

        //选择-鼠标选择几何体
        private void menuItemSelectMouse_Click(object sender, EventArgs e)
        {
            mapControl1.OperationType = OperationType.Select;
        }

        //选择-查询语句选择
        private void menuItemSelectStr_Click(object sender, EventArgs e)
        {
            int id = mapControl1.Map.SelectedLayer;
            Layer nowlayer = mapControl1.Map.Layers[id];
            String SQLstr = Interaction.InputBox("请输入sql语句查询", "查询语句进行选择", "null", -1, -1);
            //Console.WriteLine(SQLstr);
            nowlayer.QuerySQL(SQLstr, mapControl1.SelectedMode);

            //属性表刷新选择的要素
            if (!frm3.IsDisposed) { frm3.RefreshSelectFeature(); }
        }

        // 选择-显示查询结果
        private void MenuItemShowSelected_Click(object sender, EventArgs e)
        {
            if (showFeatureForm == null)
            {
                showFeatureForm = new ShowSelectedFeatureForm(mapControl1.Map);
                showFeatureForm.DoubleSelectedChanged += ShowFeatureForm_DoubleSelectedChanged;
                showFeatureForm.Show(this);
            }
            else if (showFeatureForm.IsDisposed)
            {
                showFeatureForm.Dispose();
                showFeatureForm = new ShowSelectedFeatureForm(mapControl1.Map);
                showFeatureForm.DoubleSelectedChanged += ShowFeatureForm_DoubleSelectedChanged;
                showFeatureForm.Show(this);
            }
            else
            {
                showFeatureForm.RenewSelectedItem();
                showFeatureForm.Activate();
            }
        }

        //选择-选择模式-创建新选择内容
        private void menuItemSelectNew_Click(object sender, EventArgs e)
        {
            mapControl1.SelectedMode = SelectedMode.New;
        }

        //选择-选择模式-与当前选择求并集
        private void menuItemSelectUnion_Click(object sender, EventArgs e)
        {
            mapControl1.SelectedMode = SelectedMode.Add;
        }

        //选择-选择模式-从当前选择中去除
        private void menuItemSelectDel_Click(object sender, EventArgs e)
        {
            mapControl1.SelectedMode = SelectedMode.Delete;
        }

        //选择-选择模式-与当前选择求交集
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

        //全屏显示
        private void tsButtonZoomScale_Click(object sender, EventArgs e)
        {
            mapControl1.Map.FullScreen(mapControl1.ClientSize.Width, mapControl1.ClientSize.Height, mapControl1.Map.Box);
            mapControl1.SetNeedRefreshBase();
            mapControl1.Refresh();
            tslScale.Text = "比例尺:  1:" + mapControl1.Map.MapScale.ToString("G6");
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
            menuItemEditMode.PerformClick();
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

        private void mapControl1_MouseMove(object sender, MouseEventArgs e)
        {
            //屏幕坐标
            Point point = Control.MousePosition;
            point = mapControl1.PointToClient(point);

            //地图坐标
            PointD screenPoint = new PointD(point.X, point.Y);
            screenPoint = mapControl1.Map.ToMapPoint(screenPoint);

            //显示文本
            tslCoordinate.Text = "坐标： X=" + screenPoint.X.ToString("G8")
                + ", Y=" + screenPoint.Y.ToString("G8");
        }

        private void mapControl1_MouseWheel(object sender, MouseEventArgs e)
        {
            tslScale.Text = "比例尺:  1:" + mapControl1.Map.MapScale.ToString("G8");
        }

        // mapControl当前工具发生变化
        private void mapControl1_OperationTypeChanged(object sender)
        {
            tsButtonOperateNone.Checked = false;
            tsButtonPan.Checked = false;
            tsButtonZoomIn.Checked = false;
            tsButtonZoomOut.Checked = false;
            tsButtonSelect.Checked = false;
            tsButtonNewGeo.Checked = false;
            tsButtonEditGeo.Checked = false;
            switch (mapControl1.OperationType)
            {
                case OperationType.None:
                    tsButtonOperateNone.Checked = true;
                    break;
                case OperationType.Pan:
                    tsButtonPan.Checked = true;
                    break;
                case OperationType.ZoomIn:
                    tsButtonZoomIn.Checked = true;
                    break;
                case OperationType.ZoomOut:
                    tsButtonZoomOut.Checked = true;
                    break;
                case OperationType.Select:
                    tsButtonSelect.Checked = true;
                    break;
                case OperationType.Track:
                    tsButtonNewGeo.Checked = true;
                    break;
                case OperationType.Edit:
                    tsButtonEditGeo.Checked = true;
                    break;
            }
        }

        private void mapControl1_SelectedModeChanged(object sender)
        {
            menuItemSelectNew.Checked = false;
            menuItemSelectDel.Checked = false;
            menuItemSelectUnion.Checked = false;
            menuItemSelectIntersect.Checked = false;
            switch (mapControl1.SelectedMode)
            {
                case SelectedMode.New:
                    menuItemSelectNew.Checked = true;
                    break;
                case SelectedMode.Add:
                    menuItemSelectUnion.Checked = true;
                    break;
                case SelectedMode.Delete:
                    menuItemSelectDel.Checked = true;
                    break;
                case SelectedMode.Intersect:
                    menuItemSelectIntersect.Checked = true;
                    break;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mapControl1.SelectedFeatureChanged += RefreshSelectedFromMapControl;
        }
    }
        
}
