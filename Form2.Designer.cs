namespace simpleGIS
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageNorm = new System.Windows.Forms.TabPage();
            this.checkBoxVisible = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPageRender = new System.Windows.Forms.TabPage();
            this.groupPoint = new System.Windows.Forms.GroupBox();
            this.cbBoxPointType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbClassBreak = new System.Windows.Forms.RadioButton();
            this.rbUniqueValue = new System.Windows.Forms.RadioButton();
            this.rbSimple = new System.Windows.Forms.RadioButton();
            this.tabPageLabel = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.pBoxPointColor = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.numPointSize = new System.Windows.Forms.NumericUpDown();
            this.groupPolyline = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbBoxLineDash = new System.Windows.Forms.ComboBox();
            this.pBoxLineColor = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.numLineWidth = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.groupPolygon = new System.Windows.Forms.GroupBox();
            this.pBoxBoundColor = new System.Windows.Forms.PictureBox();
            this.label8 = new System.Windows.Forms.Label();
            this.pBoxFillColor = new System.Windows.Forms.PictureBox();
            this.label9 = new System.Windows.Forms.Label();
            this.labelColumn = new System.Windows.Forms.Label();
            this.cbBoxColumn = new System.Windows.Forms.ComboBox();
            this.labelItem = new System.Windows.Forms.Label();
            this.cbBoxGroups = new System.Windows.Forms.ComboBox();
            this.panelColumns = new System.Windows.Forms.Panel();
            this.panelBreakPoints = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.numGroupNum = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.txtBoxMaxValue = new System.Windows.Forms.TextBox();
            this.pBoxShowStyle = new System.Windows.Forms.PictureBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.checkBoxLabelVisible = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.labelFontName = new System.Windows.Forms.Label();
            this.buttonSetFont = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.pBoxFontColor = new System.Windows.Forms.PictureBox();
            this.pBoxShowFontStyle = new System.Windows.Forms.PictureBox();
            this.tabControl.SuspendLayout();
            this.tabPageNorm.SuspendLayout();
            this.tabPageRender.SuspendLayout();
            this.groupPoint.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPageLabel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBoxPointColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPointSize)).BeginInit();
            this.groupPolyline.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBoxLineColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLineWidth)).BeginInit();
            this.groupPolygon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBoxBoundColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBoxFillColor)).BeginInit();
            this.panelColumns.SuspendLayout();
            this.panelBreakPoints.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGroupNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBoxShowStyle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBoxFontColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBoxShowFontStyle)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageNorm);
            this.tabControl.Controls.Add(this.tabPageRender);
            this.tabControl.Controls.Add(this.tabPageLabel);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(599, 531);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageNorm
            // 
            this.tabPageNorm.Controls.Add(this.checkBoxVisible);
            this.tabPageNorm.Controls.Add(this.textBox1);
            this.tabPageNorm.Controls.Add(this.label1);
            this.tabPageNorm.Location = new System.Drawing.Point(4, 33);
            this.tabPageNorm.Name = "tabPageNorm";
            this.tabPageNorm.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageNorm.Size = new System.Drawing.Size(591, 494);
            this.tabPageNorm.TabIndex = 0;
            this.tabPageNorm.Text = "常规";
            this.tabPageNorm.UseVisualStyleBackColor = true;
            // 
            // checkBoxVisible
            // 
            this.checkBoxVisible.AutoSize = true;
            this.checkBoxVisible.Location = new System.Drawing.Point(28, 71);
            this.checkBoxVisible.Name = "checkBoxVisible";
            this.checkBoxVisible.Size = new System.Drawing.Size(110, 29);
            this.checkBoxVisible.TabIndex = 2;
            this.checkBoxVisible.Text = "显示图层";
            this.checkBoxVisible.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(151, 20);
            this.textBox1.Margin = new System.Windows.Forms.Padding(20);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(200, 31);
            this.textBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "图层名：";
            // 
            // tabPageRender
            // 
            this.tabPageRender.Controls.Add(this.pBoxShowStyle);
            this.tabPageRender.Controls.Add(this.panelBreakPoints);
            this.tabPageRender.Controls.Add(this.panelColumns);
            this.tabPageRender.Controls.Add(this.groupPolygon);
            this.tabPageRender.Controls.Add(this.groupPolyline);
            this.tabPageRender.Controls.Add(this.groupPoint);
            this.tabPageRender.Controls.Add(this.groupBox1);
            this.tabPageRender.Location = new System.Drawing.Point(4, 33);
            this.tabPageRender.Name = "tabPageRender";
            this.tabPageRender.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRender.Size = new System.Drawing.Size(591, 494);
            this.tabPageRender.TabIndex = 1;
            this.tabPageRender.Text = "图层渲染";
            this.tabPageRender.UseVisualStyleBackColor = true;
            // 
            // groupPoint
            // 
            this.groupPoint.Controls.Add(this.numPointSize);
            this.groupPoint.Controls.Add(this.label4);
            this.groupPoint.Controls.Add(this.pBoxPointColor);
            this.groupPoint.Controls.Add(this.label3);
            this.groupPoint.Controls.Add(this.cbBoxPointType);
            this.groupPoint.Controls.Add(this.label2);
            this.groupPoint.Location = new System.Drawing.Point(3, 251);
            this.groupPoint.Name = "groupPoint";
            this.groupPoint.Size = new System.Drawing.Size(579, 160);
            this.groupPoint.TabIndex = 1;
            this.groupPoint.TabStop = false;
            this.groupPoint.Visible = false;
            // 
            // cbBoxPointType
            // 
            this.cbBoxPointType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBoxPointType.FormattingEnabled = true;
            this.cbBoxPointType.Items.AddRange(new object[] {
            "空心圆",
            "实心圆",
            "空心方形",
            "实心方形",
            "空心三角",
            "实心三角",
            "圈点",
            "双圈"});
            this.cbBoxPointType.Location = new System.Drawing.Point(159, 35);
            this.cbBoxPointType.Name = "cbBoxPointType";
            this.cbBoxPointType.Size = new System.Drawing.Size(132, 32);
            this.cbBoxPointType.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 38);
            this.label2.Margin = new System.Windows.Forms.Padding(20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 25);
            this.label2.TabIndex = 0;
            this.label2.Text = "点的形状：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbClassBreak);
            this.groupBox1.Controls.Add(this.rbUniqueValue);
            this.groupBox1.Controls.Add(this.rbSimple);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(579, 84);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "渲染方式";
            // 
            // rbClassBreak
            // 
            this.rbClassBreak.AutoSize = true;
            this.rbClassBreak.Location = new System.Drawing.Point(392, 37);
            this.rbClassBreak.Margin = new System.Windows.Forms.Padding(20, 15, 20, 15);
            this.rbClassBreak.Name = "rbClassBreak";
            this.rbClassBreak.Size = new System.Drawing.Size(109, 29);
            this.rbClassBreak.TabIndex = 2;
            this.rbClassBreak.TabStop = true;
            this.rbClassBreak.Text = "分级渲染";
            this.rbClassBreak.UseVisualStyleBackColor = true;
            // 
            // rbUniqueValue
            // 
            this.rbUniqueValue.AutoSize = true;
            this.rbUniqueValue.Location = new System.Drawing.Point(211, 37);
            this.rbUniqueValue.Margin = new System.Windows.Forms.Padding(20, 15, 20, 15);
            this.rbUniqueValue.Name = "rbUniqueValue";
            this.rbUniqueValue.Size = new System.Drawing.Size(128, 29);
            this.rbUniqueValue.TabIndex = 1;
            this.rbUniqueValue.TabStop = true;
            this.rbUniqueValue.Text = "唯一值渲染";
            this.rbUniqueValue.UseVisualStyleBackColor = true;
            // 
            // rbSimple
            // 
            this.rbSimple.AutoSize = true;
            this.rbSimple.Location = new System.Drawing.Point(45, 37);
            this.rbSimple.Margin = new System.Windows.Forms.Padding(20, 15, 20, 15);
            this.rbSimple.Name = "rbSimple";
            this.rbSimple.Size = new System.Drawing.Size(109, 29);
            this.rbSimple.TabIndex = 0;
            this.rbSimple.TabStop = true;
            this.rbSimple.Text = "简单渲染";
            this.rbSimple.UseVisualStyleBackColor = true;
            // 
            // tabPageLabel
            // 
            this.tabPageLabel.Controls.Add(this.pBoxShowFontStyle);
            this.tabPageLabel.Controls.Add(this.pBoxFontColor);
            this.tabPageLabel.Controls.Add(this.label13);
            this.tabPageLabel.Controls.Add(this.buttonSetFont);
            this.tabPageLabel.Controls.Add(this.labelFontName);
            this.tabPageLabel.Controls.Add(this.label12);
            this.tabPageLabel.Controls.Add(this.checkBoxLabelVisible);
            this.tabPageLabel.Location = new System.Drawing.Point(4, 33);
            this.tabPageLabel.Name = "tabPageLabel";
            this.tabPageLabel.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLabel.Size = new System.Drawing.Size(591, 494);
            this.tabPageLabel.TabIndex = 2;
            this.tabPageLabel.Text = "注记";
            this.tabPageLabel.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(399, 38);
            this.label3.Margin = new System.Windows.Forms.Padding(20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "颜色：";
            // 
            // pBoxPointColor
            // 
            this.pBoxPointColor.BackColor = System.Drawing.Color.White;
            this.pBoxPointColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pBoxPointColor.Location = new System.Drawing.Point(470, 35);
            this.pBoxPointColor.Name = "pBoxPointColor";
            this.pBoxPointColor.Size = new System.Drawing.Size(32, 32);
            this.pBoxPointColor.TabIndex = 3;
            this.pBoxPointColor.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(67, 103);
            this.label4.Margin = new System.Windows.Forms.Padding(20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 25);
            this.label4.TabIndex = 4;
            this.label4.Text = "大小：";
            // 
            // numPointSize
            // 
            this.numPointSize.DecimalPlaces = 1;
            this.numPointSize.Location = new System.Drawing.Point(159, 101);
            this.numPointSize.Name = "numPointSize";
            this.numPointSize.Size = new System.Drawing.Size(94, 31);
            this.numPointSize.TabIndex = 5;
            this.numPointSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // groupPolyline
            // 
            this.groupPolyline.Controls.Add(this.numLineWidth);
            this.groupPolyline.Controls.Add(this.label7);
            this.groupPolyline.Controls.Add(this.pBoxLineColor);
            this.groupPolyline.Controls.Add(this.label6);
            this.groupPolyline.Controls.Add(this.cbBoxLineDash);
            this.groupPolyline.Controls.Add(this.label5);
            this.groupPolyline.Location = new System.Drawing.Point(6, 251);
            this.groupPolyline.Name = "groupPolyline";
            this.groupPolyline.Size = new System.Drawing.Size(579, 159);
            this.groupPolyline.TabIndex = 2;
            this.groupPolyline.TabStop = false;
            this.groupPolyline.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(58, 41);
            this.label5.Margin = new System.Windows.Forms.Padding(20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 25);
            this.label5.TabIndex = 1;
            this.label5.Text = "线的形状：";
            // 
            // cbBoxLineDash
            // 
            this.cbBoxLineDash.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBoxLineDash.FormattingEnabled = true;
            this.cbBoxLineDash.Items.AddRange(new object[] {
            "实线",
            "短划线",
            "点",
            "点-短划线",
            "点-点-短划线"});
            this.cbBoxLineDash.Location = new System.Drawing.Point(167, 38);
            this.cbBoxLineDash.Name = "cbBoxLineDash";
            this.cbBoxLineDash.Size = new System.Drawing.Size(175, 32);
            this.cbBoxLineDash.TabIndex = 2;
            // 
            // pBoxLineColor
            // 
            this.pBoxLineColor.BackColor = System.Drawing.Color.White;
            this.pBoxLineColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pBoxLineColor.Location = new System.Drawing.Point(478, 38);
            this.pBoxLineColor.Name = "pBoxLineColor";
            this.pBoxLineColor.Size = new System.Drawing.Size(32, 32);
            this.pBoxLineColor.TabIndex = 5;
            this.pBoxLineColor.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(407, 41);
            this.label6.Margin = new System.Windows.Forms.Padding(20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 25);
            this.label6.TabIndex = 4;
            this.label6.Text = "颜色：";
            // 
            // numLineWidth
            // 
            this.numLineWidth.DecimalPlaces = 1;
            this.numLineWidth.Location = new System.Drawing.Point(167, 104);
            this.numLineWidth.Name = "numLineWidth";
            this.numLineWidth.Size = new System.Drawing.Size(94, 31);
            this.numLineWidth.TabIndex = 7;
            this.numLineWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(75, 106);
            this.label7.Margin = new System.Windows.Forms.Padding(20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 25);
            this.label7.TabIndex = 6;
            this.label7.Text = "宽度：";
            // 
            // groupPolygon
            // 
            this.groupPolygon.Controls.Add(this.pBoxFillColor);
            this.groupPolygon.Controls.Add(this.label9);
            this.groupPolygon.Controls.Add(this.pBoxBoundColor);
            this.groupPolygon.Controls.Add(this.label8);
            this.groupPolygon.Location = new System.Drawing.Point(6, 247);
            this.groupPolygon.Name = "groupPolygon";
            this.groupPolygon.Size = new System.Drawing.Size(579, 114);
            this.groupPolygon.TabIndex = 3;
            this.groupPolygon.TabStop = false;
            this.groupPolygon.Visible = false;
            // 
            // pBoxBoundColor
            // 
            this.pBoxBoundColor.BackColor = System.Drawing.Color.White;
            this.pBoxBoundColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pBoxBoundColor.Location = new System.Drawing.Point(192, 40);
            this.pBoxBoundColor.Name = "pBoxBoundColor";
            this.pBoxBoundColor.Size = new System.Drawing.Size(32, 32);
            this.pBoxBoundColor.TabIndex = 7;
            this.pBoxBoundColor.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(74, 47);
            this.label8.Margin = new System.Windows.Forms.Padding(20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(107, 25);
            this.label8.TabIndex = 6;
            this.label8.Text = "边界颜色：";
            // 
            // pBoxFillColor
            // 
            this.pBoxFillColor.BackColor = System.Drawing.Color.White;
            this.pBoxFillColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pBoxFillColor.Location = new System.Drawing.Point(440, 40);
            this.pBoxFillColor.Name = "pBoxFillColor";
            this.pBoxFillColor.Size = new System.Drawing.Size(32, 32);
            this.pBoxFillColor.TabIndex = 9;
            this.pBoxFillColor.TabStop = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(322, 47);
            this.label9.Margin = new System.Windows.Forms.Padding(20);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(107, 25);
            this.label9.TabIndex = 8;
            this.label9.Text = "填充颜色：";
            // 
            // labelColumn
            // 
            this.labelColumn.AutoSize = true;
            this.labelColumn.Location = new System.Drawing.Point(40, 20);
            this.labelColumn.Margin = new System.Windows.Forms.Padding(20);
            this.labelColumn.Name = "labelColumn";
            this.labelColumn.Size = new System.Drawing.Size(69, 25);
            this.labelColumn.TabIndex = 4;
            this.labelColumn.Text = "字段：";
            // 
            // cbBoxColumn
            // 
            this.cbBoxColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBoxColumn.FormattingEnabled = true;
            this.cbBoxColumn.Location = new System.Drawing.Point(132, 17);
            this.cbBoxColumn.Name = "cbBoxColumn";
            this.cbBoxColumn.Size = new System.Drawing.Size(121, 32);
            this.cbBoxColumn.TabIndex = 5;
            // 
            // labelItem
            // 
            this.labelItem.AutoSize = true;
            this.labelItem.Location = new System.Drawing.Point(338, 20);
            this.labelItem.Margin = new System.Windows.Forms.Padding(20);
            this.labelItem.Name = "labelItem";
            this.labelItem.Size = new System.Drawing.Size(69, 25);
            this.labelItem.TabIndex = 6;
            this.labelItem.Text = "分组：";
            // 
            // cbBoxGroups
            // 
            this.cbBoxGroups.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBoxGroups.FormattingEnabled = true;
            this.cbBoxGroups.Location = new System.Drawing.Point(416, 17);
            this.cbBoxGroups.Name = "cbBoxGroups";
            this.cbBoxGroups.Size = new System.Drawing.Size(121, 32);
            this.cbBoxGroups.TabIndex = 7;
            // 
            // panelColumns
            // 
            this.panelColumns.Controls.Add(this.labelColumn);
            this.panelColumns.Controls.Add(this.cbBoxGroups);
            this.panelColumns.Controls.Add(this.cbBoxColumn);
            this.panelColumns.Controls.Add(this.labelItem);
            this.panelColumns.Location = new System.Drawing.Point(6, 96);
            this.panelColumns.Name = "panelColumns";
            this.panelColumns.Size = new System.Drawing.Size(579, 65);
            this.panelColumns.TabIndex = 8;
            // 
            // panelBreakPoints
            // 
            this.panelBreakPoints.Controls.Add(this.txtBoxMaxValue);
            this.panelBreakPoints.Controls.Add(this.label11);
            this.panelBreakPoints.Controls.Add(this.numGroupNum);
            this.panelBreakPoints.Controls.Add(this.label10);
            this.panelBreakPoints.Location = new System.Drawing.Point(6, 167);
            this.panelBreakPoints.Name = "panelBreakPoints";
            this.panelBreakPoints.Size = new System.Drawing.Size(576, 74);
            this.panelBreakPoints.TabIndex = 9;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(24, 20);
            this.label10.Margin = new System.Windows.Forms.Padding(20);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(107, 25);
            this.label10.TabIndex = 5;
            this.label10.Text = "分组数目：";
            // 
            // numGroupNum
            // 
            this.numGroupNum.Location = new System.Drawing.Point(130, 18);
            this.numGroupNum.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numGroupNum.Name = "numGroupNum";
            this.numGroupNum.Size = new System.Drawing.Size(69, 31);
            this.numGroupNum.TabIndex = 8;
            this.numGroupNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(266, 20);
            this.label11.Margin = new System.Windows.Forms.Padding(20);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(126, 25);
            this.label11.TabIndex = 9;
            this.label11.Text = "本组最大值：";
            // 
            // txtBoxMaxValue
            // 
            this.txtBoxMaxValue.Location = new System.Drawing.Point(392, 18);
            this.txtBoxMaxValue.Name = "txtBoxMaxValue";
            this.txtBoxMaxValue.Size = new System.Drawing.Size(161, 31);
            this.txtBoxMaxValue.TabIndex = 10;
            // 
            // pBoxShowStyle
            // 
            this.pBoxShowStyle.BackColor = System.Drawing.Color.White;
            this.pBoxShowStyle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pBoxShowStyle.Location = new System.Drawing.Point(256, 417);
            this.pBoxShowStyle.Name = "pBoxShowStyle";
            this.pBoxShowStyle.Size = new System.Drawing.Size(64, 64);
            this.pBoxShowStyle.TabIndex = 10;
            this.pBoxShowStyle.TabStop = false;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(379, 549);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(108, 40);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "确定";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(493, 549);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(108, 40);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "取消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // checkBoxLabelVisible
            // 
            this.checkBoxLabelVisible.AutoSize = true;
            this.checkBoxLabelVisible.Location = new System.Drawing.Point(46, 28);
            this.checkBoxLabelVisible.Name = "checkBoxLabelVisible";
            this.checkBoxLabelVisible.Size = new System.Drawing.Size(110, 29);
            this.checkBoxLabelVisible.TabIndex = 3;
            this.checkBoxLabelVisible.Text = "显示注记";
            this.checkBoxLabelVisible.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(41, 111);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(69, 25);
            this.label12.TabIndex = 4;
            this.label12.Text = "字体：";
            // 
            // labelFontName
            // 
            this.labelFontName.Location = new System.Drawing.Point(116, 111);
            this.labelFontName.Name = "labelFontName";
            this.labelFontName.Size = new System.Drawing.Size(280, 25);
            this.labelFontName.TabIndex = 5;
            // 
            // buttonSetFont
            // 
            this.buttonSetFont.Location = new System.Drawing.Point(402, 104);
            this.buttonSetFont.Name = "buttonSetFont";
            this.buttonSetFont.Size = new System.Drawing.Size(106, 39);
            this.buttonSetFont.TabIndex = 6;
            this.buttonSetFont.Text = "设置字体";
            this.buttonSetFont.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(41, 177);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(107, 25);
            this.label13.TabIndex = 7;
            this.label13.Text = "文字颜色：";
            // 
            // pBoxFontColor
            // 
            this.pBoxFontColor.BackColor = System.Drawing.Color.White;
            this.pBoxFontColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pBoxFontColor.Location = new System.Drawing.Point(154, 177);
            this.pBoxFontColor.Name = "pBoxFontColor";
            this.pBoxFontColor.Size = new System.Drawing.Size(32, 32);
            this.pBoxFontColor.TabIndex = 8;
            this.pBoxFontColor.TabStop = false;
            // 
            // pBoxShowFontStyle
            // 
            this.pBoxShowFontStyle.BackColor = System.Drawing.Color.White;
            this.pBoxShowFontStyle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pBoxShowFontStyle.Location = new System.Drawing.Point(186, 250);
            this.pBoxShowFontStyle.Name = "pBoxShowFontStyle";
            this.pBoxShowFontStyle.Size = new System.Drawing.Size(210, 81);
            this.pBoxShowFontStyle.TabIndex = 11;
            this.pBoxShowFontStyle.TabStop = false;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(623, 603);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.tabControl);
            this.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form2";
            this.Text = "图层属性";
            this.tabControl.ResumeLayout(false);
            this.tabPageNorm.ResumeLayout(false);
            this.tabPageNorm.PerformLayout();
            this.tabPageRender.ResumeLayout(false);
            this.groupPoint.ResumeLayout(false);
            this.groupPoint.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPageLabel.ResumeLayout(false);
            this.tabPageLabel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBoxPointColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPointSize)).EndInit();
            this.groupPolyline.ResumeLayout(false);
            this.groupPolyline.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBoxLineColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLineWidth)).EndInit();
            this.groupPolygon.ResumeLayout(false);
            this.groupPolygon.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBoxBoundColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBoxFillColor)).EndInit();
            this.panelColumns.ResumeLayout(false);
            this.panelColumns.PerformLayout();
            this.panelBreakPoints.ResumeLayout(false);
            this.panelBreakPoints.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGroupNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBoxShowStyle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBoxFontColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBoxShowFontStyle)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageNorm;
        private System.Windows.Forms.CheckBox checkBoxVisible;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPageRender;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbSimple;
        private System.Windows.Forms.TabPage tabPageLabel;
        private System.Windows.Forms.RadioButton rbClassBreak;
        private System.Windows.Forms.RadioButton rbUniqueValue;
        private System.Windows.Forms.GroupBox groupPoint;
        private System.Windows.Forms.ComboBox cbBoxPointType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pBoxPointColor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupPolyline;
        private System.Windows.Forms.ComboBox cbBoxLineDash;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numPointSize;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panelColumns;
        private System.Windows.Forms.Label labelColumn;
        private System.Windows.Forms.ComboBox cbBoxGroups;
        private System.Windows.Forms.ComboBox cbBoxColumn;
        private System.Windows.Forms.Label labelItem;
        private System.Windows.Forms.GroupBox groupPolygon;
        private System.Windows.Forms.PictureBox pBoxFillColor;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.PictureBox pBoxBoundColor;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numLineWidth;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox pBoxLineColor;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panelBreakPoints;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown numGroupNum;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtBoxMaxValue;
        private System.Windows.Forms.PictureBox pBoxShowStyle;
        private System.Windows.Forms.PictureBox pBoxShowFontStyle;
        private System.Windows.Forms.PictureBox pBoxFontColor;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button buttonSetFont;
        private System.Windows.Forms.Label labelFontName;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox checkBoxLabelVisible;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
    }
}