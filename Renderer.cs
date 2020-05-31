using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

/// <summary>
/// 渲染相关类
/// </summary>
namespace simpleGIS
{
    /// <summary>
    /// 渲染器基类
    /// </summary>  
    [Serializable]
    public abstract class Renderer
    {
        public string Field { get; set; }//绑定的数据字段
    }



    /// <summary>
    /// 子类——简单渲染
    /// </summary>
    [Serializable]
    public  class SimpleRenderer:Renderer
    {
        #region 属性
        public Symbol Symbol { get; set; }
        #endregion

        /// <summary>
        /// 构造一个简单渲染器
        /// </summary>
        /// <param name="type">几何体类型</param>
        public SimpleRenderer(Type type)
        {
            if (type == typeof(PointD))
            { Symbol = new PointSymbol(1, Color.Black, 10).RandomSymbolFromSelf(1)[0]; }
            else if (type == typeof(Polyline) ||
                type == typeof(MultiPolyline))
            { Symbol = new LineSymbol(Pens.Black).RandomSymbolFromSelf(1)[0]; }
            else { Symbol = new PolygonSymbol(Pens.Black, new SolidBrush(Color.LightYellow)).
                    RandomSymbolFromSelf(1)[0]; }
        }
    }



    /// <summary>
    /// 子类——唯一值渲染
    /// </summary>
    [Serializable]
    public class UniqueValueRenderer:Renderer
    {
        #region 属性
        public Dictionary<string, Symbol> Symbols { get; set; }
        public Symbol DefaultSymbol { get; set; }
        #endregion

        /// <summary>
        /// 构造唯一值渲染器
        /// </summary>
        /// <param name="type">几何体类型</param>
        public UniqueValueRenderer(Type type)
        {
            if (type == typeof(PointD))
            { DefaultSymbol = new PointSymbol(1, Color.Black, 10).RandomSymbolFromSelf(1)[0]; }
            else if (type == typeof(Polyline) ||
                type == typeof(MultiPolyline))
            { DefaultSymbol = new LineSymbol(Pens.Black).RandomSymbolFromSelf(1)[0]; }
            else
            {
                DefaultSymbol = new PolygonSymbol(Pens.Black, new SolidBrush(Color.LightYellow)).
                 RandomSymbolFromSelf(1)[0];
            }
            Field = "";
            Symbols = new Dictionary<string, Symbol>();
        }

        #region 方法
        public Symbol FindSymbol(string value)//唯一值渲染。根据唯一值寻找符号
        {
            //return output;
            if(Symbols.ContainsKey(value))
            {
                return Symbols[value];
            }
            else
            {
                return DefaultSymbol;   //默认符号输出
            }
        }
        #endregion
    }



    /// <summary>
    /// 子类——分级渲染
    /// </summary>
    [Serializable]
    public class ClassBreaksRenderer:Renderer
    {
        #region 属性
        public List<double> BreakPoints { get; set; }//分级渲染的断裂点
        public List<Symbol> Symbols { get; set; }//分级渲染的符号，比断裂点个数多1个。
        #endregion

        public ClassBreaksRenderer(Type type)
        {
            Symbols = new List<Symbol>(3);
            BreakPoints = new List<double>(new double[]{ 0, 100 });
            if (type == typeof(PointD))
            { Symbols.Add(new PointSymbol(1, Color.Black, 10)); }
            else if (type == typeof(Polyline) ||
                type == typeof(MultiPolyline))
            { Symbols.Add(new LineSymbol(Pens.Black)); }
            else
            {
                Symbols.Add(new PolygonSymbol(Pens.Black, new SolidBrush(Color.LightYellow)));
            }
            Field = "";
            Symbols.AddRange(Symbols[0].RandomSymbolFromSelf(2));
        }

        #region 方法
        public Symbol FindSymbol(double value)//分级渲染。根据该属性值寻找对应的符号
        {
            Symbol output = new PointSymbol(1, System.Drawing.Color.Red, 3f);//同前赋值一个默认的符号
            if (Symbols.Count()-BreakPoints.Count() == 1)//确保不会溢出
            {
                
                if (value <= BreakPoints[0])//第一个
                {
                    return Symbols[0];
                }
                else if (value >= BreakPoints.Last())//最后一个
                {
                    return Symbols.Last();
                }
                else//中间的
                {
                    for (int i = 1; i < BreakPoints.Count(); i++)
                    {
                        if (value <= BreakPoints[i] && value > BreakPoints[i - 1])
                        {
                            output = Symbols[i];
                        }
                    }
                    return output;
                }
            }
            else
            {
                throw new IndexOutOfRangeException("分级渲染数据错误：断裂点 != 样式数目 - 1");
            }
        }
        #endregion
    }

}
