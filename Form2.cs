using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace simpleGIS
{
    public partial class Form2 : Form
    {
        #region 字段

        // 常规
        private string layerName;   // 真正的图层名

        // 可用字段
        private List<string> uniqueColumns = new List<string>();
        private List<string> classBreakColumns = new List<string>();

        // 三种渲染器都存着
        private SimpleRenderer simpleR;
        private UniqueValueRenderer uniqueR;
        private ClassBreaksRenderer classBreaksR;
        // 点线面当前编辑样式的引用
        private PointSymbol pSymbol;
        private LineSymbol lineSymbol;
        private PolygonSymbol polySymbol;
        // 记录分级点max值
        double classBreakP;

        // 注记样式
        private LabelStyle labelStyle;

        // 图层
        Layer _layer;

        #endregion

        #region 属性

        /// <summary>
        /// 获取图层的名字
        /// </summary>
        public string LayerName { get => layerName; }

        /// <summary>
        /// 获取图层是否可见
        /// </summary>
        public bool LayerVisible { get => checkBoxVisible.Checked; }

        /// <summary>
        /// 获取设置后的图层渲染器
        /// </summary>
        public Renderer Renderer { get
            {
                if (rbSimple.Checked) { return simpleR; }
                if (rbUniqueValue.Checked) { return uniqueR; }
                if (rbClassBreak.Checked) { return classBreaksR; }
                return null;
            }
        }

        public bool LabelVisible { get => checkBoxLabelVisible.Checked; }

        /// <summary>
        /// 获取图层注记样式
        /// </summary>
        public LabelStyle LabelStyle { get => labelStyle; }

        #endregion

        /// <summary>
        /// 生成图层属性对话框
        /// </summary>
        /// <param name="layer">图层</param>
        public Form2(Layer layer)
        {
            InitializeComponent();

            _layer = layer;
            simpleR = new SimpleRenderer(layer.FeatureType);
            uniqueR = new UniqueValueRenderer(layer.FeatureType);
            classBreaksR = new ClassBreaksRenderer(layer.FeatureType);
        // 将Layer的属性值移到界面上
        // 常规
            layerName = txtBoxLayerName.Text = layer.Name;
            checkBoxLabelVisible.Checked = layer.Visible;
            checkBoxLabelVisible.Enabled = !layer.IsEdit;

            // 渲染
            // 确定图层类型，选择显示何种几何符号
            if (layer.FeatureType == typeof(PointD)) { groupPoint.Visible = true; }
            else if (layer.FeatureType == typeof(Polyline) ||
                layer.FeatureType == typeof(MultiPolyline))
            { groupPolyline.Visible = true; }
            else { groupPolygon.Visible = true; }
            // 读取可用于特殊渲染的字段
            foreach (DataColumn column in layer.Table.Columns)
            {
                if (column.DataType == typeof(string) ||
                    column.DataType == typeof(int))
                { uniqueColumns.Add(column.ColumnName); }
                if (column.DataType == typeof(double) ||
                    column.DataType == typeof(int))
                { classBreakColumns.Add(column.ColumnName); }
            }
            // 检查是否可以使用不同渲染方式
            if (uniqueColumns.Count != 0) { rbUniqueValue.Enabled = true; }
            if (classBreakColumns.Count != 0) { rbClassBreak.Enabled = true; }
            // 从源图层读取到简单渲染
            if (layer.Renderer.GetType() == typeof(SimpleRenderer))
            {
                SimpleRenderer temp = (SimpleRenderer)layer.Renderer;
                simpleR.Symbol = temp.Symbol.Clone();
                // 生成默认唯一值渲染
                if (uniqueColumns.Count > 0)
                {
                    uniqueR.DefaultSymbol = temp.Symbol.Clone();
                    SetUniqueDict(uniqueColumns[0]);
                }
                // 生成默认分级渲染
                if (classBreakColumns.Count > 0)
                {
                    classBreaksR.Field = classBreakColumns[0];
                    classBreaksR.Symbols = new List<Symbol>(
                        simpleR.Symbol.RandomSymbolFromSelf((int)numGroupNum.Value));
                    SetClassBreak();
                }
                rbSimple.Checked = true;
            }
            // 读取唯一值渲染
            else if (layer.Renderer.GetType() == typeof(UniqueValueRenderer))
            {
                UniqueValueRenderer temp = (UniqueValueRenderer)layer.Renderer;
                // 装载Renderer
                uniqueR.Field = temp.Field;
                uniqueR.Symbols = new Dictionary<string, Symbol>();
                foreach (KeyValuePair<string, Symbol> item in temp.Symbols)
                {
                    uniqueR.Symbols.Add(item.Key, item.Value.Clone());
                }
                uniqueR.DefaultSymbol = temp.DefaultSymbol.Clone();
                simpleR.Symbol = uniqueR.DefaultSymbol.Clone();
                // 生成默认分级渲染
                if (classBreakColumns.Count > 0)
                {
                    if (classBreakColumns.Contains(temp.Field))
                    { classBreaksR.Field = temp.Field; }
                    else { classBreaksR.Field = classBreakColumns[0]; }
                    SetClassBreak();
                    classBreaksR.Symbols = new List<Symbol>(
                        simpleR.Symbol.RandomSymbolFromSelf((int)numGroupNum.Value));
                }
                rbUniqueValue.Checked = true;
            }
            // 读取分级渲染
            else
            {
                ClassBreaksRenderer temp = (ClassBreaksRenderer)layer.Renderer;
                // 加载Renderer
                classBreaksR.Field = temp.Field;
                classBreaksR.Symbols = new List<Symbol>(temp.Symbols.Count);
                foreach (Symbol item in temp.Symbols)
                {
                    classBreaksR.Symbols.Add(item.Clone());
                }
                classBreaksR.BreakPoints = new List<double>(temp.BreakPoints);
                simpleR.Symbol = temp.Symbols[0];
                // 生成默认唯一值渲染
                if (uniqueColumns.Count > 0)
                {
                    uniqueR.DefaultSymbol = temp.Symbols[0];
                    if (uniqueColumns.Contains(temp.Field))
                    { SetUniqueDict(temp.Field); }
                    else { SetUniqueDict(uniqueColumns[0]); }
                }
                rbClassBreak.Checked = true;
            }

            // 注记
            checkBoxLabelVisible.Checked = layer.LabelVisible;
            panelFont.Enabled = layer.LabelVisible;
            labelStyle = new LabelStyle(layer.LabelStyle.Field,
                (Font)layer.LabelStyle.Font.Clone(), layer.LabelStyle.Color);
            foreach (DataColumn column in layer.Table.Columns)
            {
                cbBoxLabelField.Items.Add(column.ColumnName);
                if (column.ColumnName == layer.LabelStyle.Field)
                { cbBoxLabelField.SelectedItem = column.ColumnName; }
            }
            labelFontName.Text = layer.LabelStyle.Font.ToString();
            pBoxBoundColor.BackColor = layer.LabelStyle.Color;
        }
        
        #region 私有函数

        // 设置symbol符号为当前编辑符号
        private void SetCurrentSymbol(Symbol symbol)
        {
            if (symbol.GetType() == typeof(PointSymbol))
            { pSymbol = (PointSymbol)symbol; }
            else if (symbol.GetType() == typeof(LineSymbol))
            { lineSymbol = (LineSymbol)symbol; }
            else { polySymbol = (PolygonSymbol)symbol; }
        }

        // 根据暂存的各个当前symbol字段刷新界面
        private void ShowSymbol()
        {
            if (pSymbol != null && groupPoint.Visible)
            {
                cbBoxPointType.SelectedIndex = pSymbol.PointType - 1;
                numPointSize.Value = new decimal(pSymbol.Size);
                pBoxPointColor.BackColor = pSymbol.Color;
            }
            if (lineSymbol != null && groupPolyline.Visible)
            {
                cbBoxLineDash.SelectedIndex = (int)lineSymbol.Style.DashStyle;
                numLineWidth.Value = new decimal(lineSymbol.Style.Width);
                pBoxLineColor.BackColor = lineSymbol.Style.Color;
            }
            if (polySymbol != null && groupPolygon.Visible)
            {
                pBoxBoundColor.BackColor = polySymbol.OutLine.Color;
                pBoxFillColor.BackColor = polySymbol.Fill.Color;
            }
            pBoxShowStyle.Refresh();
        }

        // 生成唯一值渲染的随机符号
        private void SetUniqueDict(string columnName)
        {
            uniqueR.Field = columnName;
            HashSet<string> set = new HashSet<string>();
            foreach (DataRow row in _layer.Table.Rows)
            {
                set.Add(row[columnName].ToString());
            }
            string[] uniqueValues = set.ToArray();
            Symbol[] symbols = uniqueR.DefaultSymbol.RandomSymbolFromSelf(uniqueValues.Length);
            Dictionary<string, Symbol> dict = new Dictionary<string, Symbol>(symbols.Length);
            for (int i = 0; i < symbols.Length; i++)
            {
                dict.Add(uniqueValues[i], symbols[i]);
            }
            uniqueR.Symbols = dict;
        }

        // 设置默认分级断裂点
        private void SetClassBreak()
        {
            double min, max;
            int breakPNum = (int)numGroupNum.Value - 1;
            classBreaksR.BreakPoints = new List<double>(breakPNum);
            if (_layer.Table.Rows.Count == 0)
            {
                for (int i = 0; i < breakPNum; i++)
                {
                    classBreaksR.BreakPoints.Add(i);
                }
                return;
            }
            min = max = (double)_layer.Table.Rows[0][classBreaksR.Field];
            for (int i = 1; i < _layer.Table.Rows.Count; i++)
            {
                double data = (double)_layer.Table.Rows[i][classBreaksR.Field];
                if (data < min) { min = data; }
                if (data > max) { max = data; }
            }
            for (int i = 0; i < breakPNum; i++)
            {
                classBreaksR.BreakPoints.Add(min + i * (max - min) / (breakPNum + 1));
            }
        }

        // 生成分级断裂组的名字序列
        private string[] GetClassBreakNames()
        {
            string[] names = new string[classBreaksR.Symbols.Count];
            names[0] = "< " + classBreaksR.BreakPoints[0].ToString();
            for (int i = 1; i < classBreaksR.BreakPoints.Count; i++)
            {
                names[i] = classBreaksR.BreakPoints[i - 1].ToString() + " - "
                    + classBreaksR.BreakPoints[i].ToString();
            }
            names[names.Length - 1] = "> " + classBreaksR.BreakPoints[names.Length - 2].ToString();
            return names;
        }

        #endregion

        #region 事件处理

        // 按下OK按钮
        private void buttonOK_Click(object sender, EventArgs e)
        {
            // 各个符号存到可序列化字段中
            if (rbSimple.Checked)
            {
                simpleR.Symbol.SaveToStruct();
            }
            else if (rbUniqueValue.Checked)
            {
                foreach (KeyValuePair<string, Symbol> kvp in uniqueR.Symbols)
                { kvp.Value.SaveToStruct(); }
                uniqueR.DefaultSymbol.SaveToStruct();
            }
            else if (rbClassBreak.Checked)
            {
                foreach (Symbol symbol in classBreaksR.Symbols)
                { symbol.SaveToStruct(); }
            }
            _layer.Name = LayerName;
            _layer.Visible = LayerVisible;
            _layer.Renderer = Renderer;
            _layer.LabelVisible = LabelVisible;
            _layer.LabelStyle = LabelStyle;
            DialogResult = DialogResult.OK;
        }

        // 按下取消按钮
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        // 图层名输入框完成编辑
        private void txtBoxLayerName_Leave(object sender, EventArgs e)
        {
            if (txtBoxLayerName.Text == "" || txtBoxLayerName.Text.
                All((char c) =>  c == ' '))
            {
                MessageBox.Show(this, "输入的图层名不能为空。", "图层名错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtBoxLayerName.Text = layerName;
            }
            else { layerName = txtBoxLayerName.Text; }
        }

        #region 图层渲染

        // 选择简单渲染
        private void rbSimple_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSimple.Checked)
            {
                panelBreakPoints.Visible = panelColumns.Visible = false;
                SetCurrentSymbol(simpleR.Symbol);
                ShowSymbol();
            }
        }

        // 选择唯一值渲染
        private void rbUniqueValue_CheckedChanged(object sender, EventArgs e)
        {
            if (rbUniqueValue.Checked)
            {
                panelColumns.Visible = true;
                panelBreakPoints.Visible = false;
                cbBoxColumn.Items.Clear();
                cbBoxColumn.Items.AddRange(uniqueColumns.ToArray());
                cbBoxColumn.SelectedItem = uniqueR.Field;
            }
        }

        // 选择分级渲染
        private void rbClassBreak_CheckedChanged(object sender, EventArgs e)
        {
            if (rbClassBreak.Checked)
            {
                panelColumns.Visible = panelBreakPoints.Visible = true;
                cbBoxColumn.Items.Clear();
                cbBoxColumn.Items.AddRange(classBreakColumns.ToArray());
                cbBoxColumn.SelectedItem = classBreaksR.Field;
            }
        }

        // 唯一值和分级的字段改变
        private void cbBoxColumn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbBoxColumn.SelectedIndex == -1)
            { return; }
            cbBoxGroups.Items.Clear();
            if (rbUniqueValue.Checked)
            {
                // 更改字段
                if ((string)cbBoxColumn.SelectedItem != uniqueR.Field)
                {
                    SetUniqueDict((string)cbBoxColumn.SelectedItem);
                }
                cbBoxGroups.Items.Add("__默认值__");
                cbBoxGroups.Items.AddRange(uniqueR.Symbols.Keys.ToArray());
            }
            else if (rbClassBreak.Checked)
            {
                // 更改字段
                if ((string)cbBoxColumn.SelectedItem != classBreaksR.Field)
                {
                    classBreaksR.Field = (string)cbBoxColumn.SelectedItem;
                    SetClassBreak();
                }
                cbBoxGroups.Items.AddRange(GetClassBreakNames());
            }
            cbBoxGroups.SelectedIndex = 0;
        }

        // 分级断裂点数改变
        private void numGroupNum_ValueChanged(object sender, EventArgs e)
        {
            int index = cbBoxGroups.SelectedIndex;
            SetClassBreak();
            if (classBreaksR.Symbols.Count < (int)numGroupNum.Value)
            {
                Symbol[] symbols = classBreaksR.Symbols[0].RandomSymbolFromSelf(
                    (int)numGroupNum.Value - classBreaksR.Symbols.Count);
                classBreaksR.Symbols.AddRange(symbols);
            }
            else
            {
                while (classBreaksR.Symbols.Count > (int)numGroupNum.Value)
                {
                    classBreaksR.Symbols.RemoveAt(classBreaksR.Symbols.Count - 1);
                }
            }
            cbBoxGroups.Items.Clear();
            cbBoxGroups.Items.AddRange(GetClassBreakNames());
            cbBoxGroups.SelectedIndex = Math.Min(index, cbBoxGroups.Items.Count - 1);
        }

        // 唯一值和分级的选择具体项目（符号）
        private void cbBoxGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbBoxGroups.SelectedIndex == -1)
            { return; }
            if (rbUniqueValue.Checked)
            {
                if ((string)cbBoxGroups.SelectedItem == "__默认值__")
                {
                    SetCurrentSymbol(uniqueR.DefaultSymbol);
                }
                else
                {
                    SetCurrentSymbol(uniqueR.Symbols[(string)cbBoxGroups.SelectedItem]);
                }
            }
            else if (rbClassBreak.Checked)
            {
                int index = cbBoxGroups.SelectedIndex;
                SetCurrentSymbol(classBreaksR.Symbols[index]);
                if (index != cbBoxGroups.Items.Count - 1)
                {
                    txtBoxMaxValue.Enabled = true;
                    classBreakP = classBreaksR.BreakPoints[index];
                    txtBoxMaxValue.Text = classBreakP.ToString();
                }
                else { txtBoxMaxValue.Enabled = false; }
            }
            ShowSymbol();
        }

        // 分级的断裂点数值编辑完毕
        private void txtBoxMaxValue_Leave(object sender, EventArgs e)
        {
            double temp;
            if (double.TryParse(txtBoxMaxValue.Text, out temp))
            {
                classBreakP = temp;
                classBreaksR.BreakPoints[cbBoxGroups.SelectedIndex] = temp;
                string[] names = GetClassBreakNames();
                for (int i = 0; i < names.Length; i++)
                {
                    cbBoxGroups.Items[i] = names[i];
                }
            }
            else
            {
                MessageBox.Show(this, "错误：分级断裂点必须为数字", "输入数字错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtBoxMaxValue.Text = classBreakP.ToString();
            }
        }

        // 请求改变多边形边界色
        private void pBoxBoundColor_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            dialog.Color = polySymbol.OutLine.Color;
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                polySymbol.OutLine.Color = dialog.Color;
                ShowSymbol();
            }
        }

        // 请求改变多边形填充色
        private void pBoxFillColor_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            dialog.Color = polySymbol.Fill.Color;
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                polySymbol.Fill.Color = dialog.Color;
                ShowSymbol();
            }
        }

        // 线形状发生改变
        private void cbBoxLineDash_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lineSymbol.Style.DashStyle != (DashStyle)cbBoxLineDash.SelectedIndex)
            {
                lineSymbol.Style.DashStyle = (DashStyle)cbBoxLineDash.SelectedIndex;
                ShowSymbol();
            }
        }

        // 请求改变线颜色
        private void pBoxLineColor_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            dialog.Color = lineSymbol.Style.Color;
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                lineSymbol.Style.Color = dialog.Color;
                ShowSymbol();
            }
        }

        // 线宽度改变
        private void numLineWidth_ValueChanged(object sender, EventArgs e)
        {
            if ((float)numLineWidth.Value != lineSymbol.Style.Width)
            {
                lineSymbol.Style.Width = (float)numLineWidth.Value;
                ShowSymbol();
            }
        }

        // 点形状改变
        private void cbBoxPointType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbBoxPointType.SelectedIndex != pSymbol.PointType - 1)
            {
                pSymbol.PointType = cbBoxPointType.SelectedIndex + 1;
                ShowSymbol();
            }
        }

        // 点颜色改变
        private void pBoxPointColor_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            dialog.Color = pSymbol.Color;
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                pSymbol.Color = dialog.Color;
                ShowSymbol();
            }
        }

        // 点大小改变
        private void numPointSize_ValueChanged(object sender, EventArgs e)
        {
            if (pSymbol.Size != (float)numPointSize.Value)
            {
                pSymbol.Size = (float)numPointSize.Value;
                ShowSymbol();
            }
        }

        #endregion

        // 注记可见性改变
        private void checkBoxLabelVisible_CheckedChanged(object sender, EventArgs e)
        {
            panelFont.Enabled = checkBoxLabelVisible.Checked;
        }

        // 注记字段改变
        private void cbBoxLabelField_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbBoxLabelField.SelectedIndex != -1)
                labelStyle.Field = (string)cbBoxLabelField.SelectedItem;
        }

        // 注记颜色改变
        private void pBoxFontColor_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            dialog.Color = labelStyle.Color;
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                labelStyle.Color = dialog.Color;
                pBoxFontColor.BackColor = dialog.Color;
                pBoxShowFontStyle.Refresh();
            }
        }

        // 请求改变注记字体
        private void buttonSetFont_Click(object sender, EventArgs e)
        {
            FontDialog dialog = new FontDialog();
            dialog.Font = labelStyle.Font;
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                labelStyle.Font = dialog.Font;
                labelFontName.Text = dialog.Font.ToString();
                pBoxShowFontStyle.Refresh();
            }
        }

        // 绘制符号样式
        private void pBoxShowStyle_Paint(object sender, PaintEventArgs e)
        {
            if (groupPoint.Visible)
            {
                pSymbol.DrawPoint(e.Graphics, new PointF(Width / 2, Height / 2));
            }
            else if (groupPolyline.Visible)
            {
                lineSymbol.DrawLine(e.Graphics, new PointF[]
                    { new PointF(Width/8, Height/2), new PointF(Width*7/8, Height/2) });
            }
            else
            {
                polySymbol.DrawPolygon(e.Graphics, new PointF[] 
                    { new PointF(Width/4, Height/4),
                        new PointF(Width*3/4, Height/4),
                        new PointF(Width*3/4, Height*3/4),
                        new PointF(Width/4, Height*3/4)
                    });
            }
        }

        // 绘制字体样式
        private void pBoxShowFontStyle_Paint(object sender, PaintEventArgs e)
        {
            const string temp = "SimpleGIS123";
            SizeF size = e.Graphics.MeasureString(temp, labelStyle.Font);
            RectangleF rect = new RectangleF(new PointF((Width - size.Width) / 2,
                (Height - size.Height) / 2), size);
            Brush brush = new SolidBrush(labelStyle.Color);
            e.Graphics.DrawString(temp, labelStyle.Font, brush, rect);
        }

        #endregion
    }
}
