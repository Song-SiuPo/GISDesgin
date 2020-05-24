using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace simpleGIS
{
    /// <summary>
    /// 地图控件类
    /// </summary>
    public partial class MapControl : UserControl
    {
        #region 字段
        private Map map = new Map();            // 地图对象
        private OperationType mapOperation;     // 当前地图操作类型
        private SelectedMode selectedmode;      // 地图选择对象的模式
        private bool needRefresh = false;       // 是否需要更新cache
        private bool needSave = false;          // 是否需要保存数据
        private Bitmap cache;   // 当前地图显示范围的缓冲图片，不包括跟踪层和已选对象层

        private PointF mouseLoc = new PointF();             // 记录鼠标当前位置
        private PointF mouseDownLoc = new PointF();         // 鼠标左键按下时的位置
        #endregion

        public MapControl()
        {
            cache = new Bitmap(Width, Height);
            selectedmode = SelectedMode.New;
            mapOperation = OperationType.None;
            MouseWheel += MapControl_MouseWheel;
        }

        #region 属性

        /// <summary>
        /// 获取控件的地图对象
        /// </summary>
        public Map Map { get => map; }

        /// <summary>
        /// 获取或设置控件的当前工具（操作类型）
        /// </summary>
        public OperationType OperationType { get => mapOperation; set => mapOperation = value; }

        /// <summary>
        /// 获取或设置对象的选择模式
        /// </summary>
        public SelectedMode SelectedMode { get => selectedmode; set => selectedmode = value; }

        /// <summary>
        /// 获取或设置是否需要保存标识，用于关闭前弹窗提示
        /// </summary>
        public bool NeedSave { get => needSave; set => needSave = value; }

        #endregion

        #region 方法

        public void NewMap()
        {
            map = new Map();
            needRefresh = true;
            needSave = false;
        }

        /// <summary>
        /// 加载文件，并生成新的Map对象
        /// </summary>
        /// <param name="path">文件路径</param>
        public void OpenFile(string path)
        {

        }

        /// <summary>
        /// 将map对象保存至指定路径
        /// </summary>
        /// <param name="path">文件保存路径</param>
        public void SaveFile(string path)
        {

        }

        /// <summary>
        /// 将当前地图画面输出成栅格图像
        /// </summary>
        /// <param name="path">图像保存路径</param>
        public void SaveBitmap(string path)
        {
            cache.Save(path);
        }

        #endregion

        #region 私有函数

        /// <summary>
        /// 更新Cache对象，即缓冲图片层（地图的基础层）
        /// </summary>
        private void RedrawMap(Graphics g)
        {
            map.Render(g);
            needRefresh = false;
        }

        /// <summary>
        /// 重新绘制跟踪层
        /// </summary>
        /// <param name="g">GDI绘图对象</param>
        private void RedrawTrackingLayer(Graphics g)
        {

        }

        #endregion

        #region 响应事件处理

        // 重新绘制控件
        private void MapControl_Paint(object sender, PaintEventArgs e)
        {
            if (needRefresh)
            {
                RedrawMap(e.Graphics);
            }
            e.Graphics.DrawImage(cache, 0, 0);
            RedrawTrackingLayer(e.Graphics);
        }

        // 控件大小改变
        private void MapControl_SizeChanged(object sender, EventArgs e)
        {
            if (Width != cache.Width || Height != cache.Height)
            {
                cache.Dispose();
                cache = new Bitmap(Width, Height);
                needRefresh = true;
            }
        }

        // 鼠标按下
        private void MapControl_MouseDown(object sender, MouseEventArgs e)
        {

        }
        #endregion

        // 鼠标移动
        private void MapControl_MouseMove(object sender, MouseEventArgs e)
        {

        }

        // 鼠标松开
        private void MapControl_MouseUp(object sender, MouseEventArgs e)
        {

        }

        // 鼠标双击
        private void MapControl_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        // 滑轮滚动
        private void MapControl_MouseWheel(object sender, MouseEventArgs e)
        {

        }
    }

    /// <summary>
    /// 地图选择对象的模式
    /// </summary>
    public enum SelectedMode
    {
        /// <summary>
        /// 选取新的对象
        /// </summary>
        New,
        /// <summary>
        /// 在原选择基础上添加新选择的对象
        /// </summary>
        Add,
        /// <summary>
        /// 在原选择基础上删除新选择的对象
        /// </summary>
        Delete,
        /// <summary>
        /// 取原选择对象和新选择对象的交集
        /// </summary>
        Intersect
    }

    /// <summary>
    /// 地图的编辑状态的枚举，表示当前使用的工具
    /// </summary>
    public enum OperationType
    {
        /// <summary>
        /// 无状态
        /// </summary>
        None,

        /// <summary>
        /// 地图放大
        /// </summary>
        ZoomIn,

        /// <summary>
        /// 地图缩小
        /// </summary>
        ZoomOut,

        /// <summary>
        /// 地图漫游
        /// </summary>
        Pan,

        /// <summary>
        /// 绘制新的对象
        /// </summary>
        Track,

        /// <summary>
        /// 编辑对象的几何属性（节点）
        /// </summary>
        Edit,

        /// <summary>
        /// 选择对象
        /// </summary>
        Select
    }
}
