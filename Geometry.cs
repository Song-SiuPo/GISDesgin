using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/// <summary>
/// 几何相关类+外包矩形类
/// </summary>


namespace simpleGIS
{
    /// <summary>
    /// 矩形类——外包矩形、框选矩形
    /// </summary>
    [Serializable]
    public class RectangleD
    {
        #region 属性
        public double MinX { get; set ; }
        public double MinY { get ; set; }
        public double MaxX { get; set; }
        public double MaxY { get; set; }
        #endregion

        //构造函数
        public RectangleD()
        {
            MinX = MinY = MaxX = MaxY = 0;
        }
        
        public RectangleD(RectangleD rect)
        {
            this.MinX = rect.MinX;
            this.MaxX = rect.MaxX;
            this.MinY = rect.MinY;
            this.MaxY = rect.MaxY;
        }
        public RectangleD(double minx,double miny,double maxx,double maxy)
        {
            this.MinX = minx;
            this.MinY = miny;
            this.MaxX = maxx;
            this.MaxY = maxy;
        }

        /// <summary>
        /// 判断点是否在矩形内
        /// </summary>
        /// <param name="point">地图点</param>
        /// <returns>在矩形内返回true</returns>
        public bool IsPointOn(PointD point)
        {
            return point.X >= MinX && point.Y >= MinY &&
                point.X <= MaxX && point.Y <= MaxY;
        }
    }


    /// <summary>
    /// 几何类——基类
    /// </summary>
    [Serializable]
    public abstract class Geometry
    {
        protected bool needRenewBox = true;
        protected RectangleD box = new RectangleD();

        #region 属性
        public int ID { get; set; } = -1;//唯一值-标明对象id
        public RectangleD Box { get { if (needRenewBox)
                { RenewBox(); needRenewBox = false; }
                return box; }
            set => box = value; }//记录外包矩形
        #endregion

        #region 方法
        /// <summary>
        /// 图形整体平移
        /// </summary>
        /// <param name="deltaX">水平方向移动量</param>
        /// <param name="deltaY">竖直方向移动量</param>
        public abstract void Move(double deltaX, double deltaY);

        /// <summary>
        /// 点选——该point是否能选中该几何体
        /// </summary>
        /// <param name="point">点击点坐标</param>
        /// <returns></returns>
        public abstract bool IsPointOn(PointD point, double BufferDist);

        /// <summary>
        /// 框选——该box是否能选中该几何体
        /// </summary>
        /// <param name="box">矩形</param>
        /// <returns></returns>
        public abstract bool IsWithinBox(RectangleD box);

        /// <summary>
        /// 指示几何体需更新外包矩形
        /// </summary>
        public void NeedRenewBox()
        {
            needRenewBox = true;
        }

        public abstract double GetDistance(PointD MouseLocation);
        #endregion

        #region 私有函数
        /// <summary>
        /// 更新外包矩形
        /// </summary>
        protected abstract void RenewBox();

        public PointD FindMaxXY(List<PointD> data)
        {
            double maxX = data[0].X, maxY = data[0].Y;
            for (int i = 0; i < data.Count(); i++)
            {
                if (data[i].X > maxX)
                    maxX = data[i].X;
                if (data[i].Y > maxY)
                    maxY = data[i].Y;
            }
            PointD outputP = new PointD(maxX,maxY);
            
            return outputP;
        }
        public PointD FindMinXY(List<PointD> data)
        {
            double minX = data[0].X, minY = data[0].Y;
            for (int i = 0; i < data.Count(); i++)
            {
                if (data[i].X < minX)
                    minX = data[i].X;
                if (data[i].Y < minY)
                    minY = data[i].Y;
            }
            PointD outputP = new PointD(minX,minY);
            
            return outputP;
        }
        public bool IfHasPoint(PointD point,PointD startP,PointD endP)
        {
            double cx;
            cx = (startP.X - endP.X) / (startP.Y - endP.Y) * (point.Y - endP.Y) + endP.X;
            double _maxY = Math.Max(startP.Y, endP.Y);
            double _minY = Math.Min(startP.Y, endP.Y);
            if (cx >= point.X && point.Y > _minY && point.Y <= _maxY)
                return true;
            else
                return false;
        }

        #endregion
    }


    /// <summary>
    /// 子类——点
    /// </summary>
    [Serializable]
    public class PointD:Geometry
    {
        #region 属性
        public double X { get ; set; }//点的横坐标
        public double Y { get; set; }
        #endregion
        #region 方法
        //构造函数
        public PointD()
        {
            this.X = this.Y = 0;
        }

        public PointD(double x,double y)
        {
            this.X = x;
            this.Y = y;
        }
        public PointD(double x,double y,int id)
        {
            this.X = x;
            this.Y = y;
            this.ID = id;
        }

        //点类——点选——缓冲区4*4
        public override bool IsPointOn(PointD point, double BufferDist)
        {
            //throw new NotImplementedException();
            if (this.GetDistance(point) <= BufferDist)
                return true;
            else
                return false;
        }

        public override bool IsWithinBox(RectangleD _box)
        {
            if (_box.IsPointOn(this))
                return true;
            else
                return false;

        }

        public override void Move(double deltaX, double deltaY)
        {
            X = X + deltaX;
            Y = Y + deltaY;
        }

        protected override void RenewBox()
        {
            this.box.MaxX = this.box.MinX = X;
            this.box.MinY = this.box.MaxY = Y;
        }

        public override double GetDistance(PointD MouseLocation)
        {
            double dis;
            dis = Math.Sqrt((MouseLocation.X - this.X) * (MouseLocation.X - this.X) + (MouseLocation.Y - this.Y) * (MouseLocation.Y - this.Y));
            return dis;
        }
        #endregion
    }


    /// <summary>
    /// 子类——线
    /// </summary>
    [Serializable]
    public class Polyline:Geometry
    {
        #region 属性
        private List<PointD> data = new List<PointD>();
        public List<PointD> Data{ get=>data; set { data.Clear();data.AddRange(value); } }
        #endregion

        #region 方法

        //构造函数
        public Polyline()
        {
            this.data = new List<PointD>();
        }
        public Polyline(int id)
        {
            this.data = new List<PointD>();
            this.ID = id;
        }
        public Polyline(List<PointD> value)
        {
            this.data = new List<PointD>(value);
        }
        public Polyline(List<PointD>value,int id)
        {
            this.ID = id;
            this.data = new List<PointD>(value);
        }
        public override bool IsPointOn(PointD point,double BufferDist)
        {
            if (Box.IsPointOn(point))//先用box判断
            {
                double point_dis = GetDistance(point);
                if (point_dis <= BufferDist)
                    return true;
                else
                    return false;
            }
            else
                return false;
                
        }

        public override bool IsWithinBox(RectangleD _box)
        {
            if (Box.MaxX <= _box.MaxX && box.MaxY <= _box.MaxY && box.MinX > _box.MinX && box.MinY > _box.MinY)
                return true;
            for (int i = 0; i < data.Count; i++)
            {
                if (data[i].IsWithinBox(_box))
                { return true; }
            }
            return false;
        }

        public override void Move(double deltaX, double deltaY)
        {
            for(int i=0;i<Data.Count();i++)
            {
                data[i].X = data[i].X + deltaX;
                data[i].Y = data[i].Y + deltaY;
            }
        }

        protected override void RenewBox()
        {
            PointD MaxXY = FindMaxXY(this.Data);
            PointD MinXY = FindMinXY(this.Data);
            this.box.MaxX = MaxXY.X;
            this.box.MaxY = MaxXY.Y;
            this.box.MinX = MinXY.X;
            this.box.MinY = MinXY.Y;
        }

        /// <summary>
        /// 获取点击点到线的距离
        /// </summary>
        /// <param name="MouseLocation">一个点</param>
        /// <returns></returns>
        public override double GetDistance(PointD MouseLocation)
        {
            //throw new NotImplementedException();
            double MinDistance = Math.Sqrt((Data[0].X - MouseLocation.X) * (Data[0].X - MouseLocation.X) + (Data[0].Y - MouseLocation.Y) * (Data[0].Y - MouseLocation.Y));
            double distance1;
            for(int i=0;i<Data.Count()-1;i++)
            {
                PointD a = new PointD(1,1);
                PointD b = new PointD(1,1);
                a.X = MouseLocation.X - Data[i].X;
                a.Y = MouseLocation.Y - Data[i].Y;

                b.X = Data[i + 1].X - Data[i].X;
                b.Y = Data[i + 1].Y - Data[i].Y;
                double neiji = (a.X * b.X) + (a.Y * b.Y);
                double judge = neiji / (b.X * b.X + b.Y * b.Y);
                if(judge < 0)
                {
                    distance1 = Math.Sqrt(a.X * a.X + a.Y * a.Y);
                }
                else if(judge > 1)
                {
                    distance1 = Math.Sqrt((MouseLocation.X - Data[i + 1].X) * (MouseLocation.X - Data[i + 1].X) + (MouseLocation.Y - Data[i + 1].Y) * (MouseLocation.Y - Data[i + 1].Y));
                }
                else
                {
                    distance1 = Math.Abs(a.X * b.Y - b.X * a.Y) / Math.Sqrt(b.X * b.X + b.Y * b.Y);
                }

                if (distance1 < MinDistance)
                    MinDistance = distance1;
            }
            return MinDistance;
        }
        #endregion
    }


    /// <summary>
    /// 子类——多边形
    /// </summary>
    [Serializable]
    public class Polygon:Geometry
    {
        #region 属性
        private List<PointD> data = new List<PointD>();
        public List<PointD> Data { get => data; set { data.Clear();data.AddRange(value); } }
        #endregion

        #region 方法

        //构造函数
        public Polygon()
        {
            this.data = new List<PointD>();
        }
        public Polygon(int id)
        {
            this.data = new List<PointD>();
            this.ID = id;
        }
        public Polygon(List<PointD> value)
        {
            this.data = new List<PointD>(value);
        }
        public Polygon(List<PointD> value, int id)
        {
            this.ID = id;
            this.data = new List<PointD>(value);
        }

        public override bool IsPointOn(PointD point, double BufferDist)
        {
            //throw new NotImplementedException();
            if(Box.IsPointOn(point))//先用box判断
            {
                int NumOfPointIntersection = 0;
                for (int i = 0; i < this.Data.Count() - 1; i++)
                {
                    //Data[i]和data[i+1]组成一个线段
                    if (IfHasPoint(point, this.Data[i], this.Data[i + 1]))
                        NumOfPointIntersection = NumOfPointIntersection + 1;
                }
                // 首尾连接线的交点判断
                if (IfHasPoint(point, this.Data[0], this.Data[Data.Count() - 1]))
                    NumOfPointIntersection = NumOfPointIntersection + 1;
                if (NumOfPointIntersection % 2 == 0)
                    return false;
                else
                    return true;
            }
            else
            {
                return false;
            }
            
        }

        public override bool IsWithinBox(RectangleD _box)
        {
            //throw new NotImplementedException();
            if (Box.MaxX <= _box.MaxX && box.MaxY <= _box.MaxY && box.MinX >= _box.MinX && box.MinY >= _box.MinY)
                return true;
            for (int i = 0; i < data.Count; i++)
            {
                if (data[i].IsWithinBox(_box))
                { return true; }
            }
            PointD[] boxP = new PointD[4] {new PointD(_box.MinX, _box.MinY),
                new PointD(_box.MinX, _box.MaxY), new PointD(_box.MaxX, _box.MinY),
                new PointD(_box.MaxX, _box.MaxY)};
            for (int i = 0; i < 4; i++)
            {
                if (IsPointOn(boxP[i], 0)) { return true; }
            }
            return false;
        }

        public override void Move(double deltaX, double deltaY)
        {
            //throw new NotImplementedException();
            for (int i = 0; i < Data.Count(); i++)
            {
                data[i].X = data[i].X + deltaX;
                data[i].Y = data[i].Y + deltaY;
            }
        }

        protected override void RenewBox()
        {
            //throw new NotImplementedException();
            PointD MaxXY = FindMaxXY(this.Data);
            PointD MinXY = FindMinXY(this.Data);
            this.box.MaxX = MaxXY.X;
            this.box.MaxY = MaxXY.Y;
            this.box.MinX = MinXY.X;
            this.box.MinY = MinXY.Y;
        }

        public override double GetDistance(PointD MouseLocation)
        {
            throw new NotImplementedException();
        }
        #endregion
    }



    /// <summary>
    /// 子类——多线
    /// </summary>
    [Serializable]
    public class MultiPolyline:Geometry
    {
        #region 属性
        private List<Polyline> data = new List<Polyline>();
        public List<Polyline> Data { get=>data; set { data.Clear(); data.AddRange(value); } }

        #endregion

        #region 方法
        //构造函数
        public MultiPolyline()
        {
            this.data = new List<Polyline>();
        }
        public MultiPolyline(int id)
        {
            this.data = new List<Polyline>();
            this.ID = id;
        }
        public MultiPolyline(IList<Polyline> value)
        {
            this.data = new List<Polyline>(value);
        }
        public MultiPolyline(IList<Polyline> value, int id)
        {
            this.ID = id;
            this.data = new List<Polyline>(value);
        }

        public override bool IsPointOn(PointD point, double BufferDist)
        {
            //throw new NotImplementedException();
            bool result = false;
            if (!Box.IsPointOn(point))
            { return false; }
            for(int i=0;i<data.Count();i++)
            {
                double dis;
                dis = this.data[i].GetDistance(point);
                if (dis <= BufferDist)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public override bool IsWithinBox(RectangleD _box)
        {
            //throw new NotImplementedException();
            if (Box.MaxX <= _box.MaxX && box.MaxY <= _box.MaxY && box.MinX >= _box.MinX && box.MinY >= _box.MinY)
                return true;
            for (int i = 0; i < data.Count; i++)
            {
                if (data[i].IsWithinBox(_box))
                {
                    return true;
                }
            }
            return false;
        }

        public override void Move(double deltaX, double deltaY)
        {
            for(int i=0;i<this.Data.Count();i++)
            {
                for(int j=0;j<this.Data[i].Data.Count();j++)
                {
                    this.data[i].Data[j].X = this.data[i].Data[j].X + deltaX;
                    this.data[i].Data[j].Y = this.data[i].Data[j].Y + deltaY;
                }
            }
        }

        protected override void RenewBox()
        {
            //throw new NotImplementedException();
            if (data.Count == 0)
            {
                box = new RectangleD();
                return;
            }
            double maxX = data[0].Box.MaxX, maxY = data[0].Box.MaxY,
                minX = data[0].Box.MinX, minY = data[0].Box.MinY;
            for (int i = 1; i < this.Data.Count(); i++)
            {
                if (data[i].Box.MaxX > maxX)
                    maxX = data[i].Box.MaxX;
                if (data[i].Box.MaxY > maxY)
                    maxY = data[i].Box.MaxY;
                if (data[i].Box.MinX < minX)
                    minX = data[i].Box.MinX;
                if (data[i].Box.MinY < minY)
                    minY = data[i].Box.MinY;
            }
            this.box.MinX = minX;
            this.box.MinY = minY;
            this.box.MaxX = maxX;
            this.box.MaxY = maxY;
        }

        public override double GetDistance(PointD MouseLocation)
        {
            throw new NotImplementedException();
        }
        #endregion
    }



    /// <summary>
    /// 子类——多多边形
    /// </summary>
    [Serializable]
    public class MultiPolygon:Geometry
    {
        #region 属性
        private List<Polygon> data = new List<Polygon>();
        public List<Polygon> Data { get=>data; set { data.Clear();data.AddRange(value); } }
        #endregion

        #region 方法
        //构造函数
        public MultiPolygon()
        {
            this.data = new List<Polygon>();
        }
        public MultiPolygon(int id)
        {
            this.data = new List<Polygon>();
            this.ID = id;
        }
        public MultiPolygon(IList<Polygon> value)
        {
            this.data = new List<Polygon>(value);
        }
        public MultiPolygon(IList<Polygon> value, int id)
        {
            this.ID = id;
            this.data = new List<Polygon>(value);
        }

        public override bool IsPointOn(PointD point, double BufferDist)
        {
            //throw new NotImplementedException();
            bool result = false;
            if (!Box.IsPointOn(point))
            { return false; }
            for (int i = 0; i < data.Count(); i++)
            {
                if (data[i].IsPointOn(point, BufferDist))
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public override bool IsWithinBox(RectangleD _box)
        {
            //throw new NotImplementedException();
            if (Box.MaxX <= _box.MaxX && box.MaxY <= _box.MaxY && box.MinX >= _box.MinX && box.MinY >= _box.MinY)
                return true;
            for (int i = 0; i < data.Count; i++)
            {
                if (data[i].IsWithinBox(_box))
                {
                    return true;
                }
            }
            return false;
        }

        public override void Move(double deltaX, double deltaY)
        {
            for (int i = 0; i < this.Data.Count(); i++)
            {
                for (int j = 0; j < this.Data[i].Data.Count(); j++)
                {
                    this.data[i].Data[j].X = this.data[i].Data[j].X + deltaX;
                    this.data[i].Data[j].Y = this.data[i].Data[j].Y + deltaY;
                }
            }
        }

        protected override void RenewBox()
        {
            if (data.Count == 0)
            {
                box = new RectangleD();
                return;
            }
            double maxX = data[0].Box.MaxX, maxY = data[0].Box.MaxY,
                minX = data[0].Box.MinX, minY = data[0].Box.MinY;
            for (int i = 0; i < this.Data.Count(); i++)
            {
                if (data[i].Box.MaxX > maxX)
                    maxX = data[i].Box.MaxX;
                if (data[i].Box.MaxY > maxY)
                    maxY = data[i].Box.MaxY;
                if (data[i].Box.MinX < minX)
                    minX = data[i].Box.MinX;
                if (data[i].Box.MinY < minY)
                    minY = data[i].Box.MinY;
            }
            this.box.MinX = minX;
            this.box.MinY = minY;
            this.box.MaxX = maxX;
            this.box.MaxY = maxY;
        }

        public override double GetDistance(PointD MouseLocation)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
