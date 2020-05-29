namespace simpleGIS
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemNewMap = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSavePic = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemEditMode = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemNewGeo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemEditGeo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemLayer = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemNewLayer = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDelLayer = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemLayerTable = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemLayerAttr = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemLayerUp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemLayerDown = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSelectMouse = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSelectStr = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSelectMode = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSelectNew = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSelectUnion = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSelectDel = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSelectIntersect = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsButtonNew = new System.Windows.Forms.ToolStripButton();
            this.tsButtonOpen = new System.Windows.Forms.ToolStripButton();
            this.tsButtonSave = new System.Windows.Forms.ToolStripButton();
            this.tsButtonSavePic = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsButtonOperateNone = new System.Windows.Forms.ToolStripButton();
            this.tsButtonPan = new System.Windows.Forms.ToolStripButton();
            this.tsButtonZoomIn = new System.Windows.Forms.ToolStripButton();
            this.tsButtonZoomOut = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton9 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsButtonNewLayer = new System.Windows.Forms.ToolStripButton();
            this.tsButtonSelect = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsButtonEdit = new System.Windows.Forms.ToolStripButton();
            this.tsButtonNewGeo = new System.Windows.Forms.ToolStripButton();
            this.tsButtonEditGeo = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.clboxLayers = new System.Windows.Forms.CheckedListBox();
            this.mapControl1 = new simpleGIS.MapControl();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemFile,
            this.menuItemEdit,
            this.menuItemLayer,
            this.menuItemSelect,
            this.帮助ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(1257, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuItemFile
            // 
            this.menuItemFile.AutoSize = false;
            this.menuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemNewMap,
            this.menuItemOpen,
            this.menuItemSave,
            this.menuItemSavePic});
            this.menuItemFile.Name = "menuItemFile";
            this.menuItemFile.Size = new System.Drawing.Size(80, 21);
            this.menuItemFile.Text = "文件";
            // 
            // menuItemNewMap
            // 
            this.menuItemNewMap.Name = "menuItemNewMap";
            this.menuItemNewMap.Size = new System.Drawing.Size(204, 30);
            this.menuItemNewMap.Text = "新建空白地图";
            // 
            // menuItemOpen
            // 
            this.menuItemOpen.Name = "menuItemOpen";
            this.menuItemOpen.Size = new System.Drawing.Size(204, 30);
            this.menuItemOpen.Text = "打开";
            // 
            // menuItemSave
            // 
            this.menuItemSave.Name = "menuItemSave";
            this.menuItemSave.Size = new System.Drawing.Size(204, 30);
            this.menuItemSave.Text = "保存";
            // 
            // menuItemSavePic
            // 
            this.menuItemSavePic.Name = "menuItemSavePic";
            this.menuItemSavePic.Size = new System.Drawing.Size(204, 30);
            this.menuItemSavePic.Text = "输出为图片";
            // 
            // menuItemEdit
            // 
            this.menuItemEdit.AutoSize = false;
            this.menuItemEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemEditMode,
            this.toolStripSeparator1,
            this.menuItemNewGeo,
            this.menuItemEditGeo});
            this.menuItemEdit.Name = "menuItemEdit";
            this.menuItemEdit.Size = new System.Drawing.Size(80, 21);
            this.menuItemEdit.Text = "编辑";
            // 
            // menuItemEditMode
            // 
            this.menuItemEditMode.Name = "menuItemEditMode";
            this.menuItemEditMode.Size = new System.Drawing.Size(204, 30);
            this.menuItemEditMode.Text = "编辑模式";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(201, 6);
            // 
            // menuItemNewGeo
            // 
            this.menuItemNewGeo.Name = "menuItemNewGeo";
            this.menuItemNewGeo.Size = new System.Drawing.Size(204, 30);
            this.menuItemNewGeo.Text = "绘制新几何体";
            // 
            // menuItemEditGeo
            // 
            this.menuItemEditGeo.Name = "menuItemEditGeo";
            this.menuItemEditGeo.Size = new System.Drawing.Size(204, 30);
            this.menuItemEditGeo.Text = "编辑几何体";
            // 
            // menuItemLayer
            // 
            this.menuItemLayer.AutoSize = false;
            this.menuItemLayer.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemNewLayer,
            this.menuItemDelLayer,
            this.toolStripSeparator2,
            this.menuItemLayerTable,
            this.menuItemLayerAttr,
            this.menuItemLayerUp,
            this.menuItemLayerDown});
            this.menuItemLayer.Name = "menuItemLayer";
            this.menuItemLayer.Size = new System.Drawing.Size(80, 21);
            this.menuItemLayer.Text = "图层";
            // 
            // menuItemNewLayer
            // 
            this.menuItemNewLayer.Name = "menuItemNewLayer";
            this.menuItemNewLayer.Size = new System.Drawing.Size(204, 30);
            this.menuItemNewLayer.Text = "创建新图层";
            // 
            // menuItemDelLayer
            // 
            this.menuItemDelLayer.Name = "menuItemDelLayer";
            this.menuItemDelLayer.Size = new System.Drawing.Size(204, 30);
            this.menuItemDelLayer.Text = "删除当前图层";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(201, 6);
            // 
            // menuItemLayerTable
            // 
            this.menuItemLayerTable.Name = "menuItemLayerTable";
            this.menuItemLayerTable.Size = new System.Drawing.Size(204, 30);
            this.menuItemLayerTable.Text = "打开属性表";
            // 
            // menuItemLayerAttr
            // 
            this.menuItemLayerAttr.Name = "menuItemLayerAttr";
            this.menuItemLayerAttr.Size = new System.Drawing.Size(204, 30);
            this.menuItemLayerAttr.Text = "设置图层属性";
            // 
            // menuItemLayerUp
            // 
            this.menuItemLayerUp.Name = "menuItemLayerUp";
            this.menuItemLayerUp.Size = new System.Drawing.Size(204, 30);
            this.menuItemLayerUp.Text = "图层上移";
            // 
            // menuItemLayerDown
            // 
            this.menuItemLayerDown.Name = "menuItemLayerDown";
            this.menuItemLayerDown.Size = new System.Drawing.Size(204, 30);
            this.menuItemLayerDown.Text = "图层下移";
            // 
            // menuItemSelect
            // 
            this.menuItemSelect.AutoSize = false;
            this.menuItemSelect.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemSelectMouse,
            this.menuItemSelectStr,
            this.menuItemSelectMode});
            this.menuItemSelect.Name = "menuItemSelect";
            this.menuItemSelect.Size = new System.Drawing.Size(80, 21);
            this.menuItemSelect.Text = "选择";
            // 
            // menuItemSelectMouse
            // 
            this.menuItemSelectMouse.Name = "menuItemSelectMouse";
            this.menuItemSelectMouse.Size = new System.Drawing.Size(223, 30);
            this.menuItemSelectMouse.Text = "鼠标选择几何体";
            // 
            // menuItemSelectStr
            // 
            this.menuItemSelectStr.Name = "menuItemSelectStr";
            this.menuItemSelectStr.Size = new System.Drawing.Size(223, 30);
            this.menuItemSelectStr.Text = "查询语句选择";
            // 
            // menuItemSelectMode
            // 
            this.menuItemSelectMode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemSelectNew,
            this.menuItemSelectUnion,
            this.menuItemSelectDel,
            this.menuItemSelectIntersect});
            this.menuItemSelectMode.Name = "menuItemSelectMode";
            this.menuItemSelectMode.Size = new System.Drawing.Size(223, 30);
            this.menuItemSelectMode.Text = "选择模式";
            // 
            // menuItemSelectNew
            // 
            this.menuItemSelectNew.Name = "menuItemSelectNew";
            this.menuItemSelectNew.Size = new System.Drawing.Size(242, 30);
            this.menuItemSelectNew.Text = "创建新选择内容";
            // 
            // menuItemSelectUnion
            // 
            this.menuItemSelectUnion.Name = "menuItemSelectUnion";
            this.menuItemSelectUnion.Size = new System.Drawing.Size(242, 30);
            this.menuItemSelectUnion.Text = "与当前选择求并集";
            // 
            // menuItemSelectDel
            // 
            this.menuItemSelectDel.Name = "menuItemSelectDel";
            this.menuItemSelectDel.Size = new System.Drawing.Size(242, 30);
            this.menuItemSelectDel.Text = "从当前选择中去除";
            // 
            // menuItemSelectIntersect
            // 
            this.menuItemSelectIntersect.Name = "menuItemSelectIntersect";
            this.menuItemSelectIntersect.Size = new System.Drawing.Size(242, 30);
            this.menuItemSelectIntersect.Text = "与当前选择求交集";
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.AutoSize = false;
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(80, 21);
            this.帮助ToolStripMenuItem.Text = "帮助";
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsButtonNew,
            this.tsButtonOpen,
            this.tsButtonSave,
            this.tsButtonSavePic,
            this.toolStripSeparator3,
            this.tsButtonOperateNone,
            this.tsButtonPan,
            this.tsButtonZoomIn,
            this.tsButtonZoomOut,
            this.toolStripButton9,
            this.toolStripSeparator4,
            this.tsButtonNewLayer,
            this.tsButtonSelect,
            this.toolStripSeparator5,
            this.tsButtonEdit,
            this.tsButtonNewGeo,
            this.tsButtonEditGeo});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1257, 27);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsButtonNew
            // 
            this.tsButtonNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsButtonNew.Image = global::simpleGIS.Properties.Resources.GenericDocument16;
            this.tsButtonNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsButtonNew.Name = "tsButtonNew";
            this.tsButtonNew.Size = new System.Drawing.Size(24, 24);
            this.tsButtonNew.Text = "toolStripButton1";
            this.tsButtonNew.ToolTipText = "新建空白地图";
            // 
            // tsButtonOpen
            // 
            this.tsButtonOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsButtonOpen.Image = global::simpleGIS.Properties.Resources.Folder16;
            this.tsButtonOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsButtonOpen.Name = "tsButtonOpen";
            this.tsButtonOpen.Size = new System.Drawing.Size(24, 24);
            this.tsButtonOpen.Text = "toolStripButton2";
            this.tsButtonOpen.ToolTipText = "打开";
            // 
            // tsButtonSave
            // 
            this.tsButtonSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsButtonSave.Image = global::simpleGIS.Properties.Resources.GenericSave16;
            this.tsButtonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsButtonSave.Name = "tsButtonSave";
            this.tsButtonSave.Size = new System.Drawing.Size(24, 24);
            this.tsButtonSave.Text = "toolStripButton3";
            this.tsButtonSave.ToolTipText = "保存地图";
            // 
            // tsButtonSavePic
            // 
            this.tsButtonSavePic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsButtonSavePic.Image = global::simpleGIS.Properties.Resources.GenericSaveAs16;
            this.tsButtonSavePic.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsButtonSavePic.Name = "tsButtonSavePic";
            this.tsButtonSavePic.Size = new System.Drawing.Size(24, 24);
            this.tsButtonSavePic.Text = "toolStripButton4";
            this.tsButtonSavePic.ToolTipText = "保存为图片";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 27);
            // 
            // tsButtonOperateNone
            // 
            this.tsButtonOperateNone.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsButtonOperateNone.Image = global::simpleGIS.Properties.Resources.ElementSelectTool16;
            this.tsButtonOperateNone.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsButtonOperateNone.Name = "tsButtonOperateNone";
            this.tsButtonOperateNone.Size = new System.Drawing.Size(24, 24);
            this.tsButtonOperateNone.Text = "tsButtonOperateNone";
            this.tsButtonOperateNone.ToolTipText = "鼠标指针";
            // 
            // tsButtonPan
            // 
            this.tsButtonPan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsButtonPan.Image = global::simpleGIS.Properties.Resources.PanTool_B_16;
            this.tsButtonPan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsButtonPan.Name = "tsButtonPan";
            this.tsButtonPan.Size = new System.Drawing.Size(24, 24);
            this.tsButtonPan.Text = "tsButtonOperatePan";
            this.tsButtonPan.ToolTipText = "漫游";
            // 
            // tsButtonZoomIn
            // 
            this.tsButtonZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsButtonZoomIn.Image = global::simpleGIS.Properties.Resources.ZoomInTool_B_16;
            this.tsButtonZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsButtonZoomIn.Name = "tsButtonZoomIn";
            this.tsButtonZoomIn.Size = new System.Drawing.Size(24, 24);
            this.tsButtonZoomIn.Text = "tsButtonOperateZoomIn";
            this.tsButtonZoomIn.ToolTipText = "放大";
            // 
            // tsButtonZoomOut
            // 
            this.tsButtonZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsButtonZoomOut.Image = global::simpleGIS.Properties.Resources.ZoomOutTool_B_16;
            this.tsButtonZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsButtonZoomOut.Name = "tsButtonZoomOut";
            this.tsButtonZoomOut.Size = new System.Drawing.Size(24, 24);
            this.tsButtonZoomOut.Text = "tsButtonOperateZoomOut";
            this.tsButtonZoomOut.ToolTipText = "缩小";
            // 
            // toolStripButton9
            // 
            this.toolStripButton9.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton9.Image = global::simpleGIS.Properties.Resources.ZoomFixedZoomOut16;
            this.toolStripButton9.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton9.Name = "toolStripButton9";
            this.toolStripButton9.Size = new System.Drawing.Size(24, 24);
            this.toolStripButton9.Text = "toolStripButton9";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 27);
            // 
            // tsButtonNewLayer
            // 
            this.tsButtonNewLayer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsButtonNewLayer.Image = global::simpleGIS.Properties.Resources.DataAdd16;
            this.tsButtonNewLayer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsButtonNewLayer.Name = "tsButtonNewLayer";
            this.tsButtonNewLayer.Size = new System.Drawing.Size(24, 24);
            this.tsButtonNewLayer.Text = "tsButtonAddLayer";
            this.tsButtonNewLayer.ToolTipText = "创建新图层";
            // 
            // tsButtonSelect
            // 
            this.tsButtonSelect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsButtonSelect.Image = global::simpleGIS.Properties.Resources.SelectionSelectTool16;
            this.tsButtonSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsButtonSelect.Name = "tsButtonSelect";
            this.tsButtonSelect.Size = new System.Drawing.Size(24, 24);
            this.tsButtonSelect.Text = "toolStripButton7";
            this.tsButtonSelect.ToolTipText = "选择要素";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 27);
            // 
            // tsButtonEdit
            // 
            this.tsButtonEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsButtonEdit.Image = global::simpleGIS.Properties.Resources.GenericPaintBrush16;
            this.tsButtonEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsButtonEdit.Name = "tsButtonEdit";
            this.tsButtonEdit.Size = new System.Drawing.Size(24, 24);
            this.tsButtonEdit.Text = "toolStripButton2";
            this.tsButtonEdit.ToolTipText = "编辑模式";
            // 
            // tsButtonNewGeo
            // 
            this.tsButtonNewGeo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsButtonNewGeo.Image = global::simpleGIS.Properties.Resources.GenericBlackAdd16;
            this.tsButtonNewGeo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsButtonNewGeo.Name = "tsButtonNewGeo";
            this.tsButtonNewGeo.Size = new System.Drawing.Size(24, 24);
            this.tsButtonNewGeo.Text = "toolStripButton3";
            this.tsButtonNewGeo.ToolTipText = "绘制新要素";
            // 
            // tsButtonEditGeo
            // 
            this.tsButtonEditGeo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsButtonEditGeo.Image = global::simpleGIS.Properties.Resources.EditingVertex16;
            this.tsButtonEditGeo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsButtonEditGeo.Name = "tsButtonEditGeo";
            this.tsButtonEditGeo.Size = new System.Drawing.Size(24, 24);
            this.tsButtonEditGeo.Text = "toolStripButton4";
            this.tsButtonEditGeo.ToolTipText = "编辑要素几何";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 51);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.clboxLayers);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.mapControl1);
            this.splitContainer1.Size = new System.Drawing.Size(1257, 653);
            this.splitContainer1.SplitterDistance = 233;
            this.splitContainer1.TabIndex = 2;
            // 
            // clboxLayers
            // 
            this.clboxLayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clboxLayers.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.clboxLayers.FormattingEnabled = true;
            this.clboxLayers.Location = new System.Drawing.Point(0, 0);
            this.clboxLayers.Name = "clboxLayers";
            this.clboxLayers.Size = new System.Drawing.Size(233, 653);
            this.clboxLayers.TabIndex = 0;
            // 
            // mapControl1
            // 
            this.mapControl1.BackColor = System.Drawing.Color.White;
            this.mapControl1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.mapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapControl1.Location = new System.Drawing.Point(0, 0);
            this.mapControl1.Name = "mapControl1";
            this.mapControl1.NeedSave = false;
            this.mapControl1.OperationType = simpleGIS.OperationType.None;
            this.mapControl1.SelectedMode = simpleGIS.SelectedMode.New;
            this.mapControl1.Size = new System.Drawing.Size(1020, 653);
            this.mapControl1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1257, 704);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "SimpleGIS";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuItemFile;
        private System.Windows.Forms.ToolStripMenuItem menuItemOpen;
        private System.Windows.Forms.ToolStripMenuItem menuItemEdit;
        private System.Windows.Forms.ToolStripMenuItem menuItemLayer;
        private System.Windows.Forms.ToolStripMenuItem menuItemSelect;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsButtonNew;
        private System.Windows.Forms.ToolStripButton tsButtonOpen;
        private System.Windows.Forms.ToolStripButton tsButtonSave;
        private System.Windows.Forms.ToolStripButton tsButtonSavePic;
        private System.Windows.Forms.ToolStripButton tsButtonZoomIn;
        private System.Windows.Forms.ToolStripButton tsButtonZoomOut;
        private System.Windows.Forms.ToolStripButton tsButtonNewLayer;
        private System.Windows.Forms.ToolStripButton tsButtonOperateNone;
        private System.Windows.Forms.ToolStripButton toolStripButton9;
        private System.Windows.Forms.ToolStripMenuItem menuItemSave;
        private System.Windows.Forms.ToolStripMenuItem menuItemSavePic;
        private System.Windows.Forms.ToolStripMenuItem menuItemEditMode;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuItemNewGeo;
        private System.Windows.Forms.ToolStripMenuItem menuItemEditGeo;
        private System.Windows.Forms.ToolStripMenuItem menuItemNewLayer;
        private System.Windows.Forms.ToolStripMenuItem menuItemDelLayer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem menuItemLayerTable;
        private System.Windows.Forms.ToolStripMenuItem menuItemLayerAttr;
        private System.Windows.Forms.ToolStripMenuItem menuItemLayerUp;
        private System.Windows.Forms.ToolStripMenuItem menuItemLayerDown;
        private System.Windows.Forms.ToolStripMenuItem menuItemSelectMouse;
        private System.Windows.Forms.ToolStripMenuItem menuItemSelectStr;
        private System.Windows.Forms.ToolStripMenuItem menuItemSelectMode;
        private System.Windows.Forms.ToolStripMenuItem menuItemSelectNew;
        private System.Windows.Forms.ToolStripMenuItem menuItemSelectUnion;
        private System.Windows.Forms.ToolStripMenuItem menuItemSelectDel;
        private System.Windows.Forms.ToolStripMenuItem menuItemSelectIntersect;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.CheckedListBox clboxLayers;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsButtonPan;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tsButtonEdit;
        private System.Windows.Forms.ToolStripButton tsButtonNewGeo;
        private System.Windows.Forms.ToolStripButton tsButtonSelect;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tsButtonEditGeo;
        private System.Windows.Forms.ToolStripMenuItem menuItemNewMap;
        private MapControl mapControl1;
    }
}

