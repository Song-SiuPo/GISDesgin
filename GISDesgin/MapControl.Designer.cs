namespace simpleGIS
{
    partial class MapControl
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.editContextStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addVertexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.delVertexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.delGeoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addPartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.delPartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trackContexStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.delLastVertexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.finishTrackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearTrackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trackNewPartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editContextStrip.SuspendLayout();
            this.trackContexStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // editContextStrip
            // 
            this.editContextStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.editContextStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addVertexToolStripMenuItem,
            this.delVertexToolStripMenuItem,
            this.delGeoToolStripMenuItem,
            this.addPartToolStripMenuItem,
            this.delPartToolStripMenuItem});
            this.editContextStrip.Name = "editContextStrip";
            this.editContextStrip.ShowImageMargin = false;
            this.editContextStrip.Size = new System.Drawing.Size(231, 154);
            // 
            // addVertexToolStripMenuItem
            // 
            this.addVertexToolStripMenuItem.Name = "addVertexToolStripMenuItem";
            this.addVertexToolStripMenuItem.Size = new System.Drawing.Size(230, 30);
            this.addVertexToolStripMenuItem.Text = "添加顶点";
            this.addVertexToolStripMenuItem.Click += new System.EventHandler(this.addVertexToolStripMenuItem_Click);
            // 
            // delVertexToolStripMenuItem
            // 
            this.delVertexToolStripMenuItem.Name = "delVertexToolStripMenuItem";
            this.delVertexToolStripMenuItem.Size = new System.Drawing.Size(230, 30);
            this.delVertexToolStripMenuItem.Text = "删除选定顶点";
            this.delVertexToolStripMenuItem.Click += new System.EventHandler(this.delVertexToolStripMenuItem_Click);
            // 
            // delGeoToolStripMenuItem
            // 
            this.delGeoToolStripMenuItem.Name = "delGeoToolStripMenuItem";
            this.delGeoToolStripMenuItem.Size = new System.Drawing.Size(230, 30);
            this.delGeoToolStripMenuItem.Text = "删除几何体";
            this.delGeoToolStripMenuItem.Click += new System.EventHandler(this.delGeoToolStripMenuItem_Click);
            // 
            // addPartToolStripMenuItem
            // 
            this.addPartToolStripMenuItem.Name = "addPartToolStripMenuItem";
            this.addPartToolStripMenuItem.Size = new System.Drawing.Size(230, 30);
            this.addPartToolStripMenuItem.Text = "复合几何添加新部件";
            this.addPartToolStripMenuItem.Click += new System.EventHandler(this.addPartToolStripMenuItem_Click);
            // 
            // delPartToolStripMenuItem
            // 
            this.delPartToolStripMenuItem.Name = "delPartToolStripMenuItem";
            this.delPartToolStripMenuItem.Size = new System.Drawing.Size(230, 30);
            this.delPartToolStripMenuItem.Text = "复合几何删除该部件";
            this.delPartToolStripMenuItem.Click += new System.EventHandler(this.delPartToolStripMenuItem_Click);
            // 
            // trackContexStrip
            // 
            this.trackContexStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.trackContexStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.delLastVertexToolStripMenuItem,
            this.finishTrackToolStripMenuItem,
            this.clearTrackToolStripMenuItem,
            this.trackNewPartToolStripMenuItem});
            this.trackContexStrip.Name = "trackContexStrip";
            this.trackContexStrip.ShowImageMargin = false;
            this.trackContexStrip.Size = new System.Drawing.Size(231, 152);
            // 
            // delLastVertexToolStripMenuItem
            // 
            this.delLastVertexToolStripMenuItem.Name = "delLastVertexToolStripMenuItem";
            this.delLastVertexToolStripMenuItem.Size = new System.Drawing.Size(230, 30);
            this.delLastVertexToolStripMenuItem.Text = "删除上一个顶点";
            this.delLastVertexToolStripMenuItem.Click += new System.EventHandler(this.delLastVertexToolStripMenuItem_Click);
            // 
            // finishTrackToolStripMenuItem
            // 
            this.finishTrackToolStripMenuItem.Name = "finishTrackToolStripMenuItem";
            this.finishTrackToolStripMenuItem.Size = new System.Drawing.Size(230, 30);
            this.finishTrackToolStripMenuItem.Text = "完成绘制";
            this.finishTrackToolStripMenuItem.Click += new System.EventHandler(this.finishTrackToolStripMenuItem_Click);
            // 
            // clearTrackToolStripMenuItem
            // 
            this.clearTrackToolStripMenuItem.Name = "clearTrackToolStripMenuItem";
            this.clearTrackToolStripMenuItem.Size = new System.Drawing.Size(230, 30);
            this.clearTrackToolStripMenuItem.Text = "清空当前绘制";
            this.clearTrackToolStripMenuItem.Click += new System.EventHandler(this.clearTrackToolStripMenuItem_Click);
            // 
            // trackNewPartToolStripMenuItem
            // 
            this.trackNewPartToolStripMenuItem.Name = "trackNewPartToolStripMenuItem";
            this.trackNewPartToolStripMenuItem.Size = new System.Drawing.Size(230, 30);
            this.trackNewPartToolStripMenuItem.Text = "复合几何绘制新部件";
            this.trackNewPartToolStripMenuItem.Click += new System.EventHandler(this.trackNewPartToolStripMenuItem_Click);
            // 
            // MapControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Name = "MapControl";
            this.Size = new System.Drawing.Size(146, 146);
            this.SizeChanged += new System.EventHandler(this.MapControl_SizeChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MapControl_Paint);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.MapControl_MouseDoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MapControl_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MapControl_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MapControl_MouseUp);
            this.editContextStrip.ResumeLayout(false);
            this.trackContexStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip editContextStrip;
        private System.Windows.Forms.ToolStripMenuItem addVertexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem delVertexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem delGeoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addPartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem delPartToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip trackContexStrip;
        private System.Windows.Forms.ToolStripMenuItem delLastVertexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem finishTrackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearTrackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem trackNewPartToolStripMenuItem;
    }
}
