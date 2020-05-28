using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// 渲染相关类
/// </summary>
namespace simpleGIS
{
   /// <summary>
   /// 渲染器基类
   /// </summary>   
    public class Renderer
    {
        public string Field { get; set; }//绑定的数据字段
    }
   
    
    
    /// <summary>
   /// 子类——简单渲染
   /// </summary>
    public  class SimpleRenderer:Renderer
    {
        #region 属性
        public Symbol Symbol { get; set; }
        #endregion

    }
    
    
    
    /// <summary>
    /// 子类——唯一值渲染
    /// </summary>
    public class UniqueValueRenderer:Renderer
    {
        #region 属性
        Dictionary<string, Symbol> Symbols { get; set; }
        Symbol DefaultSymbol { get; set; }
        #endregion

        #region 方法
        public Symbol FindSymbol(string value)//唯一值渲染。根据唯一值寻找符号
        {
            Symbol output = new Symbol();
            //return output;
            if(Symbols.ContainsKey(value))
            {
                return Symbols[value];
            }
            else
            {
                return output;//定义一个默认符号输出
            }
        }
        #endregion
    }
    
    
    
    /// <summary>
    /// 子类——分级渲染
    /// </summary>
    public class ClassBreaksRenderer:Renderer
    {
        #region 属性
        public List<double> BreakPoints { get; set; }//分级渲染的断裂点
        public List<Symbol> Symbols { get; set; }//分级渲染的符号，比断裂点个数多1个。
        #endregion

        #region 方法
        Symbol FindSymbol(double value)//分级渲染。根据该属性值寻找对应的符号
        {
            Symbol output = new Symbol();//同前赋值一个默认的符号
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
                            output = Symbols[i + 1];
                        }
                    }
                    return output;
                }
            }
            else
            {
                return output;
            }
        }
        #endregion
    }

}
