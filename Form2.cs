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

        // 三种渲染器都存着
        private SimpleRenderer simpleR = new SimpleRenderer();
        private UniqueValueRenderer uniqueR = new UniqueValueRenderer();
        private ClassBreaksRenderer classBreaksR = new ClassBreaksRenderer();
        // 点线面当前编辑样式的引用
        private PointSymbol pSymbol;
        private LineSymbol lineSymbol;
        private PolygonSymbol polySymbol;

        // 注记样式
        private LabelStyle labelStyle;

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
            // 从源图层读取到简单渲染
            if (layer.Renderer.GetType() == typeof(SimpleRenderer))
            {
                rbSimple.Checked = true;
                SimpleRenderer temp = (SimpleRenderer)layer.Renderer;
                simpleR.Symbol = CloneSymbol(temp.Symbol);
            }
            // 读取唯一值渲染
            else if (layer.Renderer.GetType() == typeof(UniqueValueRenderer))
            {
                rbUniqueValue.Checked = true;
                panelColumns.Visible = true;
                UniqueValueRenderer temp = (UniqueValueRenderer)layer.Renderer;
                // 向comboBox添加字段
                foreach (DataColumn column in layer.Table.Columns)
                {
                    if (column.DataType == typeof(string) ||
                    column.DataType == typeof(int))
                    {
                        cbBoxColumn.Items.Add(column.ColumnName);
                        if (column.ColumnName == temp.Field)
                        { cbBoxColumn.SelectedItem = column.ColumnName; }
                    }
                }
                // 装载Renderer
                uniqueR.Field = temp.Field;
                uniqueR.Symbols = new Dictionary<string, Symbol>();
                foreach (KeyValuePair<string, Symbol> item in temp.Symbols)
                {
                    uniqueR.Symbols.Add(item.Key, CloneSymbol(item.Value));
                }
                uniqueR.DefaultSymbol = CloneSymbol(temp.DefaultSymbol);
                cbBoxGroups.SelectedIndex = cbBoxGroups.Items.Count - 1;
            }
            // 读取分级渲染
            else
            {
                rbClassBreak.Checked = true;
                panelColumns.Visible = panelBreakPoints.Visible = true;
                ClassBreaksRenderer temp = (ClassBreaksRenderer)layer.Renderer;
                // 向combox添加字段
                foreach (DataColumn column in layer.Table.Columns)
                {
                    if (column.DataType == typeof(double) ||
                    column.DataType == typeof(int))
                    {
                        cbBoxColumn.Items.Add(column.ColumnName);
                        if (column.ColumnName == temp.Field)
                        { cbBoxColumn.SelectedItem = column.ColumnName; }
                    }
                }
                numGroupNum.Value = temp.Symbols.Count;
                // 加载Renderer
                classBreaksR.Field = temp.Field;
                classBreaksR.Symbols = new List<Symbol>(temp.Symbols.Count);
                foreach (Symbol item in temp.Symbols)
                {
                    classBreaksR.Symbols.Add(CloneSymbol(item));
                }
                classBreaksR.BreakPoints = new List<double>(temp.BreakPoints);
                cbBoxGroups.SelectedIndex = 0;
            }
            // 检查是否可以使用不同渲染方式
            foreach(DataColumn column in layer.Table.Columns)
            {
                if (column.DataType == typeof(string) ||
                    column.DataType == typeof(int))
                {
                    rbUniqueValue.Enabled = true;
                }
                if (column.DataType == typeof(double) ||
                    column.DataType == typeof(int))
                {
                    rbClassBreak.Enabled = true;
                }
                cbBoxLabelField.Items.Add(column.ColumnName);
            }

            // 注记
            checkBoxLabelVisible.Checked = layer.LabelVisible;
            panelFont.Enabled = layer.LabelVisible;
            labelStyle = new LabelStyle(layer.LabelStyle.Field,
                (Font)layer.LabelStyle.Font.Clone(), layer.LabelStyle.Color);
            cbBoxLabelField.SelectedItem = layer.LabelStyle.Field;
            labelFontName.Text = layer.LabelStyle.Font.ToString();
            pBoxBoundColor.BackColor = layer.LabelStyle.Color;
        }
        
        #region 私有函数

        // 复制符号
        private Symbol CloneSymbol(Symbol symbol)
        {
            Symbol newSymbol;
            if (symbol.GetType() == typeof(PointSymbol))
            {
                PointSymbol temp = (PointSymbol)symbol;
                newSymbol = new PointSymbol(temp.PointType, temp.Color, temp.Size);
            }
            else if (symbol.GetType() == typeof(LineSymbol))
            {
                LineSymbol temp = (LineSymbol)symbol;
                newSymbol = new LineSymbol((Pen)temp.Style.Clone());
            }
            else
            {
                PolygonSymbol temp = (PolygonSymbol)symbol;
                newSymbol = new PolygonSymbol((Pen)temp.OutLine.Clone(),
                    (SolidBrush)temp.Fill.Clone());
            }
            return newSymbol;
        }

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
        private void ChangeStyleFromSymbol()
        {
            if (pSymbol != null)
            {
                cbBoxPointType.SelectedIndex = pSymbol.PointType - 1;
                numPointSize.Value = new decimal(pSymbol.Size);
                pBoxPointColor.BackColor = pSymbol.Color;
            }
            if (lineSymbol != null)
            {
                cbBoxLineDash.SelectedIndex = (int)lineSymbol.Style.DashStyle;
                numLineWidth.Value = new decimal(lineSymbol.Style.Width);
                pBoxLineColor.BackColor = lineSymbol.Style.Color;
            }
            if (polySymbol != null)
            {
                pBoxBoundColor.BackColor = polySymbol.OutLine.Color;
                pBoxFillColor.BackColor = polySymbol.Fill.Color;
            }
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

        // 选择简单渲染
        private void rbSimple_CheckedChanged(object sender, EventArgs e)
        {
            panelBreakPoints.Visible = panelColumns.Visible = false;
            Symbol _symbol;     // 当前编辑的Symbol
            // 原先没有简单渲染，新建
            if (simpleR == null)
            {
                simpleR = new SimpleRenderer();
                if (uniqueR != null)
                {
                    simpleR.Symbol = CloneSymbol(uniqueR.DefaultSymbol);
                    _symbol = simpleR.Symbol;
                }
                else if (classBreaksR != null)
                {
                    simpleR.Symbol = CloneSymbol(classBreaksR.Symbols[0]);
                    _symbol = simpleR.Symbol;
                }
            }
        }

        // 选择唯一值渲染
        private void rbUniqueValue_CheckedChanged(object sender, EventArgs e)
        {

        }

        // 选择分级渲染
        private void rbClassBreak_CheckedChanged(object sender, EventArgs e)
        {

        }

        // 唯一值和分级的字段改变
        private void cbBoxColumn_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        // 分级断裂点数改变
        private void numGroupNum_ValueChanged(object sender, EventArgs e)
        {

        }

        // 唯一值和分级的选择具体项目（符号）
        private void cbBoxGroups_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        // 分级的断裂点数值编辑完毕
        private void txtBoxMaxValue_Leave(object sender, EventArgs e)
        {

        }

        // 请求改变多边形边界色
        private void pBoxBoundColor_Click(object sender, EventArgs e)
        {

        }

        // 请求改变多边形填充色
        private void pBoxFillColor_Click(object sender, EventArgs e)
        {

        }

        // 线形状发生改变
        private void cbBoxLineDash_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        // 请求改变线颜色
        private void pBoxLineColor_Click(object sender, EventArgs e)
        {

        }

        // 线宽度改变
        private void numLineWidth_ValueChanged(object sender, EventArgs e)
        {

        }

        // 点形状改变
        private void cbBoxPointType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        // 点颜色改变
        private void pBoxPointColor_Click(object sender, EventArgs e)
        {

        }

        // 点大小改变
        private void numPointSize_ValueChanged(object sender, EventArgs e)
        {

        }

        // 注记可见性改变
        private void checkBoxLabelVisible_CheckedChanged(object sender, EventArgs e)
        {

        }

        // 注记字段改变
        private void cbBoxLabelField_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        // 注记颜色改变
        private void pBoxFontColor_Click(object sender, EventArgs e)
        {

        }

        // 请求改变注记字体
        private void buttonSetFont_Click(object sender, EventArgs e)
        {

        }

        #endregion
    }
}
