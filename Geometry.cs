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
    public class RectangleD
    {
        #region 属性
        public double MinX { get => MinX; set => MinX = value; }
        public double MinY { get => MinY; set => MinY = value; }
        public double MaxX { get => MaxX; set => MaxX = value; }
        public double MaxY { get => MaxY; set => MaxY = value; }
        #endregion
    }


    /// <summary>
    /// 几何类——基类
    /// </summary>
    public abstract class Geometry
    {
        protected bool needRenewBox = false;
        protected RectangleD box;

        #region 属性
        public int ID { get; set; }//唯一值-标明对象id
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
        /// 标记该几何体需更新边界盒
        /// </summary>
        public void NeedRenewBox() { needRenewBox = true; }

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
            PointD outputP = new PointD();
            outputP.X = maxX;
            outputP.Y = maxY;
            return outputP;
        }
        public PointD FindMinXY(List<PointD> data)
        {
            double maxX = data[0].X, maxY = data[0].Y;
            for (int i = 0; i < data.Count(); i++)
            {
                if (data[i].X < maxX)
                    maxX = data[i].X;
                if (data[i].Y < maxY)
                    maxY = data[i].Y;
            }
            PointD outputP = new PointD();
            outputP.X = maxX;
            outputP.Y = maxY;
            return outputP;
        }
        public bool IfHasPoint(PointD point,PointD startP,PointD endP)
        {
            double cx;
            cx = (startP.X - endP.X) / (startP.Y - endP.Y) * (point.Y - endP.Y) + endP.X;
            if (cx >= point.X)
                return true;
            else
                return false;
        }

        #endregion
    }


    /// <summary>
    /// 子类——点
    /// </summary>
    public class PointD:Geometry
    {
        #region 属性
        public double X { get => X; set=>X = value; }//点的横坐标
        public double Y { get => X; set=>X = value; }

       

        #endregion
        #region 方法
        //点类——点选——缓冲区4*4
        public override bool IsPointOn(PointD point, double BufferDist)
        {
            //throw new NotImplementedException();
            if (this.GetDistance(point) <= BufferDist)
                return true;
            else
                return false;
        }

        public override bool IsWithinBox(RectangleD box)
        {
            if (X < box.MaxX && X > box.MinX && Y < box.MaxY && Y > box.MinY)
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
    public class Polyline:Geometry
    {
        #region 属性
        public List<PointD> Data { get=>Data.ToList<PointD>(); set { Data.Clear();Data.AddRange(value); } }

        #endregion

        #region 方法
        public override bool IsPointOn(PointD point,double BufferDist)
        {
            double point_dis = GetDistance(point);
            if (point_dis <= BufferDist)
                return true;
            else
                return false;
        }

        public override bool IsWithinBox(RectangleD box)
        {
            PointD MaxXY = FindMaxXY(this.Data);
            PointD MinXY = FindMinXY(this.Data);
            if (MaxXY.X <= box.MaxX && MaxXY.Y <= box.MaxY && MinXY.X > box.MinX && MinXY.Y > box.MinY)
                return true;
            else
                return false;

        }

        public override void Move(double deltaX, double deltaY)
        {
            for(int i=0;i<Data.Count();i++)
            {
                Data[i].X = Data[i].X + deltaX;
                Data[i].Y = Data[i].Y + deltaY;
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
            for(int i=0;i<Data.Count()-1;i++)
            {
                PointD a = new PointD();
                PointD b = new PointD();
                a.X = MouseLocation.X - Data[i].X;
                a.Y = MouseLocation.Y - Data[i].Y;
                b.X = Data[i + 1].X - Data[i].X;
                b.Y = Data[i + 1].Y - Data[i].Y;
                double dist;
                dist = Math.Abs(a.X * b.Y - b.X * a.Y) / Math.Sqrt(b.X * b.X + b.Y * b.Y);
                if (dist < MinDistance)
                    MinDistance = dist;
            }
            return MinDistance;
        }
        #endregion
    }


    /// <summary>
    /// 子类——多边形
    /// </summary>
    public class Polygon:Geometry
    {
        #region 属性
        public List<PointD> Data { get=>Data.ToList<PointD>(); set { Data.Clear();Data.AddRange(value); } }
        #endregion

        #region 方法
        public override bool IsPointOn(PointD point, double BufferDist)
        {
            //throw new NotImplementedException();
            int NumOfPointIntersection = 0;
            for(int i=0;i<this.Data.Count()-1;i++)
            {
                //Data[i]和data[i+1]组成一个线段
                if (IfHasPoint(point, this.Data[i], this.Data[i + 1]))
                    NumOfPointIntersection = NumOfPointIntersection + 1;

            }
            if (NumOfPointIntersection / 2 == 0)
                return false;
            else
                return true;
        }

        public override bool IsWithinBox(RectangleD box)
        {
            //throw new NotImplementedException();
            PointD MaxXY = FindMaxXY(this.Data);
            PointD MinXY = FindMinXY(this.Data);
            if (MaxXY.X <= box.MaxX && MaxXY.Y <= box.MaxY && MinXY.X >= box.MinX && MinXY.Y >= box.MinY)
                return true;
            else
                return false;
        }

        public override void Move(double deltaX, double deltaY)
        {
            //throw new NotImplementedException();
            for (int i = 0; i < Data.Count(); i++)
            {
                Data[i].X = Data[i].X + deltaX;
                Data[i].Y = Data[i].Y + deltaY;
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
    public class MultiPolyline:Geometry
    {
        #region 属性
        public List<Polyline> Data { get=>Data.ToList<Polyline>(); set { Data.Clear();Data.AddRange(value); } }

        #endregion

        #region 方法
        public override bool IsPointOn(PointD point, double BufferDist)
        {
            //throw new NotImplementedException();
            bool result = false;
            for(int i=0;i<Data.Count();i++)
            {
                double dis;
                dis = this.Data[i].GetDistance(point);
                if (dis <= BufferDist)
                    result = true;
            }
            return result;
        }

        public override bool IsWithinBox(RectangleD box)
        {
            //throw new NotImplementedException();
            double maxX = 0, maxY = 0, minX = 0, minY = 0;
            PointD max, min;
            for(int i=0;i<this.Data.Count();i++)
            {
                max = FindMaxXY(this.Data[i].Data);
                min = FindMinXY(this.Data[i].Data);
                if (max.X > maxX)
                    maxX = max.X;
                if (max.Y > maxY)
                    maxY = max.Y;
                if (min.X < minX)
                    minX = min.X;
                if (min.Y < minY)
                    minY = min.Y;
            }
            if (minX >= box.MinX && minY >= box.MinY && maxX <= box.MaxX && maxY <= box.MaxY)
                return true;
            else
                return false;
        }

        public override void Move(double deltaX, double deltaY)
        {
            for(int i=0;i<this.Data.Count();i++)
            {
                for(int j=0;j<this.Data[i].Data.Count();j++)
                {
                    this.Data[i].Data[j].X = this.Data[i].Data[j].X + deltaX;
                    this.Data[i].Data[j].Y = this.Data[i].Data[j].Y + deltaY;
                }
            }
        }

        protected override void RenewBox()
        {
            //throw new NotImplementedException();
            double maxX = 0, maxY = 0, minX = 0, minY = 0;
            PointD max, min;
            for (int i = 0; i < this.Data.Count(); i++)
            {
                max = FindMaxXY(this.Data[i].Data);
                min = FindMinXY(this.Data[i].Data);
                if (max.X > maxX)
                    maxX = max.X;
                if (max.Y > maxY)
                    maxY = max.Y;
                if (min.X < minX)
                    minX = min.X;
                if (min.Y < minY)
                    minY = min.Y;
            }
            this.Box.MinX = minX;
            this.Box.MinY = minY;
            this.Box.MaxX = maxX;
            this.Box.MaxY = maxY;
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
    public class MultiPolygon:Geometry
    {
        #region 属性
        public List<Polygon> Data { get=>Data.ToList<Polygon>(); set { Data.Clear();Data.AddRange(value); } }
        #endregion

        #region 方法
        public override bool IsPointOn(PointD point, double BufferDist)
        {
            //throw new NotImplementedException();
            int NumOfPointIntersection = 0;
            for (int i = 0; i < this.Data.Count(); i++)
            {
                for(int j=0;j<this.Data[i].Data.Count-1;j++)
                {
                    if (IfHasPoint(point, this.Data[i].Data[j], this.Data[i].Data[j + 1]))
                        NumOfPointIntersection = NumOfPointIntersection + 1;
                }
                //Data[i]和data[i+1]组成一个线段

            }
            if (NumOfPointIntersection / 2 == 0)
                return false;
            else
                return true;
        }

        public override bool IsWithinBox(RectangleD box)
        {
            double maxX = 0, maxY = 0, minX = 0, minY = 0;
            PointD max, min;
            for (int i = 0; i < this.Data.Count(); i++)
            {
                max = FindMaxXY(this.Data[i].Data);
                min = FindMinXY(this.Data[i].Data);
                if (max.X > maxX)
                    maxX = max.X;
                if (max.Y > maxY)
                    maxY = max.Y;
                if (min.X < minX)
                    minX = min.X;
                if (min.Y < minY)
                    minY = min.Y;
            }
            if (minX >= box.MinX && minY >= box.MinY && maxX <= box.MaxX && maxY <= box.MaxY)
                return true;
            else
                return false;
        }

        public override void Move(double deltaX, double deltaY)
        {
            for (int i = 0; i < this.Data.Count(); i++)
            {
                for (int j = 0; j < this.Data[i].Data.Count(); j++)
                {
                    this.Data[i].Data[j].X = this.Data[i].Data[j].X + deltaX;
                    this.Data[i].Data[j].Y = this.Data[i].Data[j].Y + deltaY;
                }
            }
        }

        protected override void RenewBox()
        {
            double maxX = 0, maxY = 0, minX = 0, minY = 0;
            PointD max, min;
            for (int i = 0; i < this.Data.Count(); i++)
            {
                max = FindMaxXY(this.Data[i].Data);
                min = FindMinXY(this.Data[i].Data);
                if (max.X > maxX)
                    maxX = max.X;
                if (max.Y > maxY)
                    maxY = max.Y;
                if (min.X < minX)
                    minX = min.X;
                if (min.Y < minY)
                    minY = min.Y;
            }
            this.Box.MinX = minX;
            this.Box.MinY = minY;
            this.Box.MaxX = maxX;
            this.Box.MaxY = maxY;
        }

        public override double GetDistance(PointD MouseLocation)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
