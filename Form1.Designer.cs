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
            this.MenuItemShowSelected = new System.Windows.Forms.ToolStripMenuItem();
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
            this.tsButtonZoomScale = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsButtonNewLayer = new System.Windows.Forms.ToolStripButton();
            this.tsButtonSelect = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsButtonEdit = new System.Windows.Forms.ToolStripButton();
            this.tsButtonNewGeo = new System.Windows.Forms.ToolStripButton();
            this.tsButtonEditGeo = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.clboxLayers = new System.Windows.Forms.CheckedListBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tslScale = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslCoordinate = new System.Windows.Forms.ToolStripStatusLabel();
            this.mapControl1 = new simpleGIS.MapControl();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
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
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(2, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(943, 24);
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
            this.menuItemFile.Text = "文件(&F)";
            // 
            // menuItemNewMap
            // 
            this.menuItemNewMap.Image = global::simpleGIS.Properties.Resources.GenericDocument161;
            this.menuItemNewMap.Name = "menuItemNewMap";
            this.menuItemNewMap.ShortcutKeyDisplayString = "Ctrl+N";
            this.menuItemNewMap.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.menuItemNewMap.Size = new System.Drawing.Size(183, 22);
            this.menuItemNewMap.Text = "新建(&N)";
            this.menuItemNewMap.ToolTipText = "新建(Ctrl+N)\r\n创建新的地图文档";
            this.menuItemNewMap.Click += new System.EventHandler(this.menuItemNewMap_Click);
            // 
            // menuItemOpen
            // 
            this.menuItemOpen.Image = global::simpleGIS.Properties.Resources.Folder16;
            this.menuItemOpen.Name = "menuItemOpen";
            this.menuItemOpen.ShortcutKeyDisplayString = "Ctrl+O";
            this.menuItemOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.menuItemOpen.Size = new System.Drawing.Size(183, 22);
            this.menuItemOpen.Text = "打开(&O)";
            this.menuItemOpen.ToolTipText = "打开(Ctrl+O)\r\n打开现有地图文档";
            this.menuItemOpen.Click += new System.EventHandler(this.menuItemOpen_Click);
            // 
            // menuItemSave
            // 
            this.menuItemSave.Image = global::simpleGIS.Properties.Resources.GenericSave16;
            this.menuItemSave.Name = "menuItemSave";
            this.menuItemSave.ShortcutKeyDisplayString = "Ctrl+S";
            this.menuItemSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.menuItemSave.Size = new System.Drawing.Size(183, 22);
            this.menuItemSave.Text = "保存(&S)";
            this.menuItemSave.ToolTipText = "保存(Ctrl+S)\r\n保存当前地图文档";
            this.menuItemSave.Click += new System.EventHandler(this.menuItemSave_Click);
            // 
            // menuItemSavePic
            // 
            this.menuItemSavePic.Image = global::simpleGIS.Properties.Resources.GenericSaveAs16;
            this.menuItemSavePic.Name = "menuItemSavePic";
            this.menuItemSavePic.ShortcutKeyDisplayString = "Ctrl+P";
            this.menuItemSavePic.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.menuItemSavePic.Size = new System.Drawing.Size(183, 22);
            this.menuItemSavePic.Text = "导出地图(&P)";
            this.menuItemSavePic.ToolTipText = "导出地图(Ctrl+P)\r\n将此地图导出为BitMap文件";
            this.menuItemSavePic.Click += new System.EventHandler(this.menuItemSavePic_Click);
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
            this.menuItemEdit.Text = "编辑(&E)";
            // 
            // menuItemEditMode
            // 
            this.menuItemEditMode.CheckOnClick = true;
            this.menuItemEditMode.Enabled = false;
            this.menuItemEditMode.Name = "menuItemEditMode";
            this.menuItemEditMode.Size = new System.Drawing.Size(166, 22);
            this.menuItemEditMode.Text = "编辑模式(&S)";
            this.menuItemEditMode.Click += new System.EventHandler(this.menuItemEditMode_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(163, 6);
            // 
            // menuItemNewGeo
            // 
            this.menuItemNewGeo.Enabled = false;
            this.menuItemNewGeo.Image = global::simpleGIS.Properties.Resources.GenericBlackAdd16;
            this.menuItemNewGeo.Name = "menuItemNewGeo";
            this.menuItemNewGeo.Size = new System.Drawing.Size(166, 22);
            this.menuItemNewGeo.Text = "绘制新几何体(&N)";
            this.menuItemNewGeo.Click += new System.EventHandler(this.menuItemNewGeo_Click);
            // 
            // menuItemEditGeo
            // 
            this.menuItemEditGeo.Enabled = false;
            this.menuItemEditGeo.Image = global::simpleGIS.Properties.Resources.EditingVertex16;
            this.menuItemEditGeo.Name = "menuItemEditGeo";
            this.menuItemEditGeo.Size = new System.Drawing.Size(166, 22);
            this.menuItemEditGeo.Text = "编辑几何体(&E)";
            this.menuItemEditGeo.Click += new System.EventHandler(this.menuItemEditGeo_Click);
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
            this.menuItemLayer.Text = "图层(&L)";
            // 
            // menuItemNewLayer
            // 
            this.menuItemNewLayer.Image = global::simpleGIS.Properties.Resources.DataAdd16;
            this.menuItemNewLayer.Name = "menuItemNewLayer";
            this.menuItemNewLayer.Size = new System.Drawing.Size(184, 26);
            this.menuItemNewLayer.Text = "创建新图层(&N)";
            this.menuItemNewLayer.Click += new System.EventHandler(this.menuItemNewLayer_Click);
            // 
            // menuItemDelLayer
            // 
            this.menuItemDelLayer.Enabled = false;
            this.menuItemDelLayer.Name = "menuItemDelLayer";
            this.menuItemDelLayer.Size = new System.Drawing.Size(184, 26);
            this.menuItemDelLayer.Text = "删除当前图层(&D)";
            this.menuItemDelLayer.Click += new System.EventHandler(this.menuItemDelLayer_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(181, 6);
            // 
            // menuItemLayerTable
            // 
            this.menuItemLayerTable.Enabled = false;
            this.menuItemLayerTable.Name = "menuItemLayerTable";
            this.menuItemLayerTable.Size = new System.Drawing.Size(184, 26);
            this.menuItemLayerTable.Text = "打开属性表(&T)";
            this.menuItemLayerTable.Click += new System.EventHandler(this.menuItemLayerTable_Click);
            // 
            // menuItemLayerAttr
            // 
            this.menuItemLayerAttr.Enabled = false;
            this.menuItemLayerAttr.Name = "menuItemLayerAttr";
            this.menuItemLayerAttr.Size = new System.Drawing.Size(184, 26);
            this.menuItemLayerAttr.Text = "设置图层属性(&I)";
            this.menuItemLayerAttr.Click += new System.EventHandler(this.menuItemLayerAttr_Click);
            // 
            // menuItemLayerUp
            // 
            this.menuItemLayerUp.Enabled = false;
            this.menuItemLayerUp.Name = "menuItemLayerUp";
            this.menuItemLayerUp.Size = new System.Drawing.Size(184, 26);
            this.menuItemLayerUp.Text = "图层上移(&F)";
            this.menuItemLayerUp.Click += new System.EventHandler(this.menuItemLayerUp_Click);
            // 
            // menuItemLayerDown
            // 
            this.menuItemLayerDown.Enabled = false;
            this.menuItemLayerDown.Name = "menuItemLayerDown";
            this.menuItemLayerDown.Size = new System.Drawing.Size(184, 26);
            this.menuItemLayerDown.Text = "图层下移(&B)";
            this.menuItemLayerDown.Click += new System.EventHandler(this.menuItemLayerDown_Click);
            // 
            // menuItemSelect
            // 
            this.menuItemSelect.AutoSize = false;
            this.menuItemSelect.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemSelectMouse,
            this.menuItemSelectStr,
            this.MenuItemShowSelected,
            this.menuItemSelectMode});
            this.menuItemSelect.Name = "menuItemSelect";
            this.menuItemSelect.Size = new System.Drawing.Size(80, 21);
            this.menuItemSelect.Text = "选择(&S)";
            // 
            // menuItemSelectMouse
            // 
            this.menuItemSelectMouse.Enabled = false;
            this.menuItemSelectMouse.Image = global::simpleGIS.Properties.Resources.SelectionSelectTool16;
            this.menuItemSelectMouse.Name = "menuItemSelectMouse";
            this.menuItemSelectMouse.Size = new System.Drawing.Size(187, 22);
            this.menuItemSelectMouse.Text = "鼠标选择几何体(&M)";
            this.menuItemSelectMouse.Click += new System.EventHandler(this.menuItemSelectMouse_Click);
            // 
            // menuItemSelectStr
            // 
            this.menuItemSelectStr.Enabled = false;
            this.menuItemSelectStr.Name = "menuItemSelectStr";
            this.menuItemSelectStr.Size = new System.Drawing.Size(187, 22);
            this.menuItemSelectStr.Text = "查询语句选择(&A)";
            this.menuItemSelectStr.Click += new System.EventHandler(this.menuItemSelectStr_Click);
            // 
            // MenuItemShowSelected
            // 
            this.MenuItemShowSelected.Name = "MenuItemShowSelected";
            this.MenuItemShowSelected.Size = new System.Drawing.Size(187, 22);
            this.MenuItemShowSelected.Text = "显示识别要素窗口(&S)";
            this.MenuItemShowSelected.Click += new System.EventHandler(this.MenuItemShowSelected_Click);
            // 
            // menuItemSelectMode
            // 
            this.menuItemSelectMode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemSelectNew,
            this.menuItemSelectUnion,
            this.menuItemSelectDel,
            this.menuItemSelectIntersect});
            this.menuItemSelectMode.Name = "menuItemSelectMode";
            this.menuItemSelectMode.Size = new System.Drawing.Size(187, 22);
            this.menuItemSelectMode.Text = "选择模式(&I)";
            // 
            // menuItemSelectNew
            // 
            this.menuItemSelectNew.Checked = true;
            this.menuItemSelectNew.CheckState = System.Windows.Forms.CheckState.Checked;
            this.menuItemSelectNew.Name = "menuItemSelectNew";
            this.menuItemSelectNew.Size = new System.Drawing.Size(188, 22);
            this.menuItemSelectNew.Text = "创建新选择内容(&C)";
            this.menuItemSelectNew.Click += new System.EventHandler(this.menuItemSelectNew_Click);
            // 
            // menuItemSelectUnion
            // 
            this.menuItemSelectUnion.Name = "menuItemSelectUnion";
            this.menuItemSelectUnion.Size = new System.Drawing.Size(188, 22);
            this.menuItemSelectUnion.Text = "与当前选择求并集(&A)";
            this.menuItemSelectUnion.Click += new System.EventHandler(this.menuItemSelectUnion_Click);
            // 
            // menuItemSelectDel
            // 
            this.menuItemSelectDel.Name = "menuItemSelectDel";
            this.menuItemSelectDel.Size = new System.Drawing.Size(188, 22);
            this.menuItemSelectDel.Text = "从当前选择中去除(&R)";
            this.menuItemSelectDel.Click += new System.EventHandler(this.menuItemSelectDel_Click);
            // 
            // menuItemSelectIntersect
            // 
            this.menuItemSelectIntersect.Name = "menuItemSelectIntersect";
            this.menuItemSelectIntersect.Size = new System.Drawing.Size(188, 22);
            this.menuItemSelectIntersect.Text = "与当前选择求交集(&S)";
            this.menuItemSelectIntersect.Click += new System.EventHandler(this.menuItemSelectIntersect_Click);
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.AutoSize = false;
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(80, 21);
            this.帮助ToolStripMenuItem.Text = "帮助(&H)";
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
            this.tsButtonZoomScale,
            this.toolStripSeparator4,
            this.tsButtonNewLayer,
            this.tsButtonSelect,
            this.toolStripSeparator5,
            this.tsButtonEdit,
            this.tsButtonNewGeo,
            this.tsButtonEditGeo});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStrip1.Size = new System.Drawing.Size(943, 27);
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
            this.tsButtonNew.ToolTipText = "新建(Ctrl+N)\r\n创建新的地图文档";
            this.tsButtonNew.Click += new System.EventHandler(this.tsButtonNew_Click);
            // 
            // tsButtonOpen
            // 
            this.tsButtonOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsButtonOpen.Image = global::simpleGIS.Properties.Resources.Folder16;
            this.tsButtonOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsButtonOpen.Name = "tsButtonOpen";
            this.tsButtonOpen.Size = new System.Drawing.Size(24, 24);
            this.tsButtonOpen.Text = "toolStripButton2";
            this.tsButtonOpen.ToolTipText = "打开(Ctrl+O)\r\n打开现有地图文档";
            this.tsButtonOpen.Click += new System.EventHandler(this.tsButtonOpen_Click);
            // 
            // tsButtonSave
            // 
            this.tsButtonSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsButtonSave.Image = global::simpleGIS.Properties.Resources.GenericSave16;
            this.tsButtonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsButtonSave.Name = "tsButtonSave";
            this.tsButtonSave.Size = new System.Drawing.Size(24, 24);
            this.tsButtonSave.Text = "toolStripButton3";
            this.tsButtonSave.ToolTipText = "保存(Ctrl+S)\r\n保存当前地图文档";
            this.tsButtonSave.Click += new System.EventHandler(this.tsButtonSave_Click);
            // 
            // tsButtonSavePic
            // 
            this.tsButtonSavePic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsButtonSavePic.Image = global::simpleGIS.Properties.Resources.GenericSaveAs16;
            this.tsButtonSavePic.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsButtonSavePic.Name = "tsButtonSavePic";
            this.tsButtonSavePic.Size = new System.Drawing.Size(24, 24);
            this.tsButtonSavePic.Text = "toolStripButton4";
            this.tsButtonSavePic.ToolTipText = "导出地图(Ctrl+P)\r\n将此地图导出为BitMap文件";
            this.tsButtonSavePic.Click += new System.EventHandler(this.tsButtonSavePic_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 27);
            // 
            // tsButtonOperateNone
            // 
            this.tsButtonOperateNone.Checked = true;
            this.tsButtonOperateNone.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsButtonOperateNone.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsButtonOperateNone.Image = global::simpleGIS.Properties.Resources.ElementSelectTool16;
            this.tsButtonOperateNone.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsButtonOperateNone.Name = "tsButtonOperateNone";
            this.tsButtonOperateNone.Size = new System.Drawing.Size(24, 24);
            this.tsButtonOperateNone.Text = "tsButtonOperateNone";
            this.tsButtonOperateNone.ToolTipText = "选择元素\r\n选择、调整和移动放置到地图上的文本、图形或其他元素";
            this.tsButtonOperateNone.Click += new System.EventHandler(this.tsButtonOperateNone_Click);
            // 
            // tsButtonPan
            // 
            this.tsButtonPan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsButtonPan.Image = global::simpleGIS.Properties.Resources.PanTool_B_16;
            this.tsButtonPan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsButtonPan.Name = "tsButtonPan";
            this.tsButtonPan.Size = new System.Drawing.Size(24, 24);
            this.tsButtonPan.Text = "tsButtonOperatePan";
            this.tsButtonPan.ToolTipText = "平移\r\n通过拖动来平移地图";
            this.tsButtonPan.Click += new System.EventHandler(this.tsButtonPan_Click);
            // 
            // tsButtonZoomIn
            // 
            this.tsButtonZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsButtonZoomIn.Image = global::simpleGIS.Properties.Resources.ZoomInTool_B_16;
            this.tsButtonZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsButtonZoomIn.Name = "tsButtonZoomIn";
            this.tsButtonZoomIn.Size = new System.Drawing.Size(24, 24);
            this.tsButtonZoomIn.Text = "tsButtonOperateZoomIn";
            this.tsButtonZoomIn.ToolTipText = "放大\r\n切换到放大工具\r\n";
            this.tsButtonZoomIn.Click += new System.EventHandler(this.tsButtonZoomIn_Click);
            // 
            // tsButtonZoomOut
            // 
            this.tsButtonZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsButtonZoomOut.Image = global::simpleGIS.Properties.Resources.ZoomOutTool_B_16;
            this.tsButtonZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsButtonZoomOut.Name = "tsButtonZoomOut";
            this.tsButtonZoomOut.Size = new System.Drawing.Size(24, 24);
            this.tsButtonZoomOut.Text = "tsButtonOperateZoomOut";
            this.tsButtonZoomOut.ToolTipText = "缩小\r\n切换到缩小工具";
            this.tsButtonZoomOut.Click += new System.EventHandler(this.tsButtonZoomOut_Click);
            // 
            // tsButtonZoomScale
            // 
            this.tsButtonZoomScale.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsButtonZoomScale.Image = global::simpleGIS.Properties.Resources.ZoomFixedZoomOut16;
            this.tsButtonZoomScale.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsButtonZoomScale.Name = "tsButtonZoomScale";
            this.tsButtonZoomScale.Size = new System.Drawing.Size(24, 24);
            this.tsButtonZoomScale.Text = "toolStripButton9";
            this.tsButtonZoomScale.ToolTipText = "缩放至地图范围";
            this.tsButtonZoomScale.Click += new System.EventHandler(this.tsButtonZoomScale_Click);
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
            this.tsButtonNewLayer.Click += new System.EventHandler(this.tsButtonNewLayer_Click);
            // 
            // tsButtonSelect
            // 
            this.tsButtonSelect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsButtonSelect.Image = global::simpleGIS.Properties.Resources.SelectionSelectTool16;
            this.tsButtonSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsButtonSelect.Name = "tsButtonSelect";
            this.tsButtonSelect.Size = new System.Drawing.Size(24, 24);
            this.tsButtonSelect.Text = "toolStripButton7";
            this.tsButtonSelect.ToolTipText = "选择要素\r\n通过在上方单击或拖拽方框的方式从可选图层选择要素";
            this.tsButtonSelect.Click += new System.EventHandler(this.tsButtonSelect_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 27);
            // 
            // tsButtonEdit
            // 
            this.tsButtonEdit.CheckOnClick = true;
            this.tsButtonEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsButtonEdit.Enabled = false;
            this.tsButtonEdit.Image = global::simpleGIS.Properties.Resources.GenericPaintBrush16;
            this.tsButtonEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsButtonEdit.Name = "tsButtonEdit";
            this.tsButtonEdit.Size = new System.Drawing.Size(24, 24);
            this.tsButtonEdit.Text = "toolStripButton2";
            this.tsButtonEdit.ToolTipText = "编辑模式";
            this.tsButtonEdit.Click += new System.EventHandler(this.tsButtonEdit_Click);
            // 
            // tsButtonNewGeo
            // 
            this.tsButtonNewGeo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsButtonNewGeo.Enabled = false;
            this.tsButtonNewGeo.Image = global::simpleGIS.Properties.Resources.GenericBlackAdd16;
            this.tsButtonNewGeo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsButtonNewGeo.Name = "tsButtonNewGeo";
            this.tsButtonNewGeo.Size = new System.Drawing.Size(24, 24);
            this.tsButtonNewGeo.Text = "toolStripButton3";
            this.tsButtonNewGeo.ToolTipText = "绘制新要素";
            this.tsButtonNewGeo.Click += new System.EventHandler(this.tsButtonNewGeo_Click);
            // 
            // tsButtonEditGeo
            // 
            this.tsButtonEditGeo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsButtonEditGeo.Enabled = false;
            this.tsButtonEditGeo.Image = global::simpleGIS.Properties.Resources.EditingVertex16;
            this.tsButtonEditGeo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsButtonEditGeo.Name = "tsButtonEditGeo";
            this.tsButtonEditGeo.Size = new System.Drawing.Size(24, 24);
            this.tsButtonEditGeo.Text = "toolStripButton4";
            this.tsButtonEditGeo.ToolTipText = "编辑要素几何";
            this.tsButtonEditGeo.Click += new System.EventHandler(this.tsButtonEditGeo_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 51);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.clboxLayers);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.statusStrip1);
            this.splitContainer1.Panel2.Controls.Add(this.mapControl1);
            this.splitContainer1.Size = new System.Drawing.Size(943, 452);
            this.splitContainer1.SplitterDistance = 169;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 2;
            // 
            // clboxLayers
            // 
            this.clboxLayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clboxLayers.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.clboxLayers.FormattingEnabled = true;
            this.clboxLayers.Location = new System.Drawing.Point(0, 0);
            this.clboxLayers.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.clboxLayers.Name = "clboxLayers";
            this.clboxLayers.Size = new System.Drawing.Size(169, 452);
            this.clboxLayers.TabIndex = 0;
            this.clboxLayers.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clboxLayers_ItemCheck);
            this.clboxLayers.SelectedIndexChanged += new System.EventHandler(this.clboxLayers_SelectedIndexChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslScale,
            this.tslCoordinate});
            this.statusStrip1.Location = new System.Drawing.Point(0, 422);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.statusStrip1.Size = new System.Drawing.Size(771, 30);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tslScale
            // 
            this.tslScale.AutoSize = false;
            this.tslScale.Name = "tslScale";
            this.tslScale.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tslScale.Size = new System.Drawing.Size(300, 25);
            this.tslScale.Text = "比例尺：";
            this.tslScale.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tslCoordinate
            // 
            this.tslCoordinate.AutoSize = false;
            this.tslCoordinate.BorderStyle = System.Windows.Forms.Border3DStyle.Bump;
            this.tslCoordinate.Name = "tslCoordinate";
            this.tslCoordinate.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tslCoordinate.Size = new System.Drawing.Size(300, 25);
            this.tslCoordinate.Text = "坐标：";
            this.tslCoordinate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mapControl1
            // 
            this.mapControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mapControl1.BackColor = System.Drawing.Color.White;
            this.mapControl1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.mapControl1.DoubleSelectedItem = -1;
            this.mapControl1.Location = new System.Drawing.Point(0, 0);
            this.mapControl1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.mapControl1.Name = "mapControl1";
            this.mapControl1.NeedSave = false;
            this.mapControl1.OperationType = simpleGIS.OperationType.None;
            this.mapControl1.SelectedMode = simpleGIS.SelectedMode.New;
            this.mapControl1.Size = new System.Drawing.Size(772, 431);
            this.mapControl1.TabIndex = 0;
            this.mapControl1.OperationTypeChanged += new simpleGIS.MapControl.SimpleHandler(this.mapControl1_OperationTypeChanged);
            this.mapControl1.SelectedModeChanged += new simpleGIS.MapControl.SimpleHandler(this.mapControl1_SelectedModeChanged);
            this.mapControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mapControl1_MouseMove);
            this.mapControl1.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.mapControl1_MouseWheel);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(943, 503);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SimpleGIS";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
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
        private System.Windows.Forms.ToolStripButton tsButtonZoomScale;
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
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tslScale;
        private System.Windows.Forms.ToolStripStatusLabel tslCoordinate;
        private System.Windows.Forms.ToolStripMenuItem MenuItemShowSelected;
    }
}

