namespace simpleGIS
{
    partial class Form3
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
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnSaveEdit = new System.Windows.Forms.ToolStripButton();
            this.btnStartEdit = new System.Windows.Forms.ToolStripButton();
            this.stpSearch = new System.Windows.Forms.ToolStripButton();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxSQL = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.Operation = new System.Windows.Forms.ToolStripMenuItem();
            this.tmiStartEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.tmiStopEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.tmiSaveEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.tmiCancelEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.tmiEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.tmiAddColumn = new System.Windows.Forms.ToolStripMenuItem();
            this.tmiDeleteRow = new System.Windows.Forms.ToolStripMenuItem();
            this.删除列ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.选择ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tmiSelectClear = new System.Windows.Forms.ToolStripMenuItem();
            this.全选ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSearch = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tmiDeleteColumn = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridView1.Location = new System.Drawing.Point(0, 82);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 30;
            this.dataGridView1.Size = new System.Drawing.Size(403, 303);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Column2";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Column3";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSaveEdit,
            this.btnStartEdit,
            this.stpSearch});
            this.toolStrip1.Location = new System.Drawing.Point(0, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(403, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnSaveEdit
            // 
            this.btnSaveEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSaveEdit.Image = global::simpleGIS.Properties.Resources.GenericSave161;
            this.btnSaveEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveEdit.Name = "btnSaveEdit";
            this.btnSaveEdit.Size = new System.Drawing.Size(23, 22);
            this.btnSaveEdit.Text = "toolStripButton1";
            // 
            // btnStartEdit
            // 
            this.btnStartEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnStartEdit.Image = global::simpleGIS.Properties.Resources.GenericPaintBrush16;
            this.btnStartEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStartEdit.Name = "btnStartEdit";
            this.btnStartEdit.Size = new System.Drawing.Size(23, 22);
            this.btnStartEdit.Text = "toolStripButton2";
            // 
            // stpSearch
            // 
            this.stpSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.stpSearch.Image = global::simpleGIS.Properties.Resources.GenericSearch16;
            this.stpSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stpSearch.Name = "stpSearch";
            this.stpSearch.Size = new System.Drawing.Size(23, 22);
            this.stpSearch.Text = "toolStripButton3";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 54);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "查询条件：";
            // 
            // tbxSQL
            // 
            this.tbxSQL.Location = new System.Drawing.Point(70, 51);
            this.tbxSQL.Margin = new System.Windows.Forms.Padding(2);
            this.tbxSQL.Name = "tbxSQL";
            this.tbxSQL.Size = new System.Drawing.Size(274, 21);
            this.tbxSQL.TabIndex = 3;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Operation,
            this.tmiEdit,
            this.选择ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(403, 25);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // Operation
            // 
            this.Operation.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmiStartEdit,
            this.tmiStopEdit,
            this.tmiSaveEdit,
            this.tmiCancelEdit});
            this.Operation.Name = "Operation";
            this.Operation.Size = new System.Drawing.Size(44, 21);
            this.Operation.Text = "操作";
            // 
            // tmiStartEdit
            // 
            this.tmiStartEdit.Name = "tmiStartEdit";
            this.tmiStartEdit.Size = new System.Drawing.Size(124, 22);
            this.tmiStartEdit.Text = "开始编辑";
            this.tmiStartEdit.Click += new System.EventHandler(this.tmiStartEdit_Click);
            // 
            // tmiStopEdit
            // 
            this.tmiStopEdit.Name = "tmiStopEdit";
            this.tmiStopEdit.Size = new System.Drawing.Size(124, 22);
            this.tmiStopEdit.Text = "结束编辑";
            this.tmiStopEdit.Click += new System.EventHandler(this.tmiStopEdit_Click);
            // 
            // tmiSaveEdit
            // 
            this.tmiSaveEdit.Name = "tmiSaveEdit";
            this.tmiSaveEdit.Size = new System.Drawing.Size(124, 22);
            this.tmiSaveEdit.Text = "保存更改";
            this.tmiSaveEdit.Click += new System.EventHandler(this.tmiSaveEdit_Click);
            // 
            // tmiCancelEdit
            // 
            this.tmiCancelEdit.Name = "tmiCancelEdit";
            this.tmiCancelEdit.Size = new System.Drawing.Size(124, 22);
            this.tmiCancelEdit.Text = "取消更改";
            this.tmiCancelEdit.Click += new System.EventHandler(this.tmiCancelEdit_Click);
            // 
            // tmiEdit
            // 
            this.tmiEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmiAddColumn,
            this.tmiDeleteRow,
            this.删除列ToolStripMenuItem});
            this.tmiEdit.Enabled = false;
            this.tmiEdit.Name = "tmiEdit";
            this.tmiEdit.Size = new System.Drawing.Size(44, 21);
            this.tmiEdit.Text = "编辑";
            // 
            // tmiAddColumn
            // 
            this.tmiAddColumn.Name = "tmiAddColumn";
            this.tmiAddColumn.Size = new System.Drawing.Size(136, 22);
            this.tmiAddColumn.Text = "增加字段";
            this.tmiAddColumn.Click += new System.EventHandler(this.tmiAddColumn_Click);
            // 
            // tmiDeleteRow
            // 
            this.tmiDeleteRow.Name = "tmiDeleteRow";
            this.tmiDeleteRow.Size = new System.Drawing.Size(136, 22);
            this.tmiDeleteRow.Text = "删除所选行";
            this.tmiDeleteRow.Click += new System.EventHandler(this.tmiDeleteRow_Click);
            // 
            // 删除列ToolStripMenuItem
            // 
            this.删除列ToolStripMenuItem.Name = "删除列ToolStripMenuItem";
            this.删除列ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.删除列ToolStripMenuItem.Text = "删除列";
            this.删除列ToolStripMenuItem.Click += new System.EventHandler(this.删除列ToolStripMenuItem_Click);
            // 
            // 选择ToolStripMenuItem
            // 
            this.选择ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmiSelectClear,
            this.全选ToolStripMenuItem});
            this.选择ToolStripMenuItem.Name = "选择ToolStripMenuItem";
            this.选择ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.选择ToolStripMenuItem.Text = "选择";
            // 
            // tmiSelectClear
            // 
            this.tmiSelectClear.Name = "tmiSelectClear";
            this.tmiSelectClear.Size = new System.Drawing.Size(124, 22);
            this.tmiSelectClear.Text = "清除选择";
            this.tmiSelectClear.Click += new System.EventHandler(this.清除选择ToolStripMenuItem_Click);
            // 
            // 全选ToolStripMenuItem
            // 
            this.全选ToolStripMenuItem.Name = "全选ToolStripMenuItem";
            this.全选ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.全选ToolStripMenuItem.Text = "全选";
            this.全选ToolStripMenuItem.Click += new System.EventHandler(this.全选ToolStripMenuItem_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(349, 49);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(52, 23);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmiDeleteColumn});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(113, 26);
            // 
            // tmiDeleteColumn
            // 
            this.tmiDeleteColumn.Name = "tmiDeleteColumn";
            this.tmiDeleteColumn.Size = new System.Drawing.Size(112, 22);
            this.tmiDeleteColumn.Text = "删除列";
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 385);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.tbxSQL);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.dataGridView1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "Form3";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "要素属性表";
            this.Load += new System.EventHandler(this.Form3_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnSaveEdit;
        private System.Windows.Forms.ToolStripButton btnStartEdit;
        private System.Windows.Forms.ToolStripButton stpSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxSQL;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem Operation;
        private System.Windows.Forms.ToolStripMenuItem tmiStartEdit;
        private System.Windows.Forms.ToolStripMenuItem tmiStopEdit;
        private System.Windows.Forms.ToolStripMenuItem tmiSaveEdit;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ToolStripMenuItem tmiCancelEdit;
        private System.Windows.Forms.ToolStripMenuItem tmiEdit;
        private System.Windows.Forms.ToolStripMenuItem tmiAddColumn;
        private System.Windows.Forms.ToolStripMenuItem tmiDeleteRow;
        private System.Windows.Forms.ToolStripMenuItem 选择ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tmiSelectClear;
        private System.Windows.Forms.ToolStripMenuItem 全选ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tmiDeleteColumn;
        private System.Windows.Forms.ToolStripMenuItem 删除列ToolStripMenuItem;
    }
}