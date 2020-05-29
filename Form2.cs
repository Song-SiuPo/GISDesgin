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
    public partial class Form2 : Form
    {
        #region 字段

        // 三种渲染器都存着
        SimpleRenderer simpleR = new SimpleRenderer();
        UniqueValueRenderer uniqueR = new UniqueValueRenderer();
        ClassBreaksRenderer classBreaksR = new ClassBreaksRenderer();

        // 注记字体，颜色放在pBoxFontColor.BackColor
        Font font;

        #endregion

        #region 属性

        /// <summary>
        /// 获取图层的名字
        /// </summary>
        public string LayerName { get => txtBoxLayerName.Text; }

        /// <summary>
        /// 获取图层是否可见
        /// </summary>
        public bool LayerVisible { get => checkBoxVisible.Checked; }

        /// <summary>
        /// 获取图层注记字体
        /// </summary>
        public Font LabelFont { get => font; }

        /// <summary>
        /// 获取图层注记的颜色
        /// </summary>
        public Color LabelColor { get => pBoxFontColor.BackColor; }

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
            txtBoxLayerName.Text = layer.Name;
            checkBoxLabelVisible.Checked = layer.Visible;

            // 渲染
            if (layer.Renderer.GetType() == typeof(SimpleRenderer))
            {
                rbSimple.Checked = true;
                SimpleRenderer temp = (SimpleRenderer)layer.Renderer;
                simpleR.Symbol = CloneSymbol(temp.Symbol);
            }
            else if (layer.Renderer.GetType() == typeof(UniqueValueRenderer))
            {
                rbUniqueValue.Checked = true;
                panelColumns.Visible = true;
                UniqueValueRenderer temp = (UniqueValueRenderer)layer.Renderer;
                uniqueR.Field = temp.Field;
                uniqueR.Symbols = new Dictionary<string, Symbol>();
                foreach (KeyValuePair<string, Symbol> item in temp.Symbols)
                {
                    uniqueR.Symbols.Add(item.Key, CloneSymbol(item.Value));
                }
                uniqueR.DefaultSymbol = CloneSymbol(temp.DefaultSymbol);
            }
            else
            {
                rbClassBreak.Checked = true;
                panelColumns.Visible = panelBreakPoints.Visible = true;
                ClassBreaksRenderer temp = (ClassBreaksRenderer)layer.Renderer;
                classBreaksR.Field = temp.Field;
                classBreaksR.Symbols = new List<Symbol>(temp.Symbols.Count);
                foreach (Symbol item in temp.Symbols)
                {
                    classBreaksR.Symbols.Add(CloneSymbol(item));
                }
                classBreaksR.BreakPoints = new List<double>(temp.BreakPoints);
            }
            foreach(DataColumn column in layer.Table.Columns)
            {
                if (column.ColumnMapping)
            }

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

        #endregion
    }
}
