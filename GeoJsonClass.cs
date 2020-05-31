using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsAppTest
{
    public class GeoJsonClass
    {
    }

    #region JsonPoint

    public class JsonPoint
    {
        public string type { get; set; }
        public List<FeaturePoint> features { get; set; }
    }
    public class FeaturePoint
    {
        public string type { get; set; }
        public GeoPoint geometry { get; set; }
        public PropertyPoint properties { get; set; }
    }
    public class GeoPoint
    {
        public string type { get; set; }
        public List<double> coordinates { get; set; }
        
    }
    public class PropertyPoint
    {
        public int ID { get; set; }
    }
    #endregion
    #region JsonPolyline

    public class JsonPolyline
    {
        public string type { get; set; }
        public List<FeaturePolyline> features { get; set; }
    }
    public class FeaturePolyline
    {
        public string type { get; set; }
        public GeoPolyline geometry { get; set; }
        public PropertyPolyline properties { get; set; }
    }
    public class GeoPolyline
    {
        public string type { get; set; }
        public List<List<double>> coordinates { get; set; }
        
    }
    public class PropertyPolyline
    {
        public int FNODE_ { get; set; }
        public int TNODE_ { get; set; }
        public int LPOLY_ { get; set; }
        public int RPOLY_ { get; set; }
        public double LENGHTH { get; set; }
        public  int ROA_4M_ { get; set; }
        public int ROA_4_M_ID { get; set; }
        public int GBCODE { get; set; }
    }
    #endregion

    #region JsonPolygon

    public class JsonPolygon
    {
        public string type { get; set; }
        public List<FeaturePolygon> features { get; set; }
    }
    public class FeaturePolygon
    {
        public string type { get; set; }
        public GeoPolygon geometry { get; set; }
        public PropertyPolygon properties { get; set; }
    }
    public class GeoPolygon
    {
        public string type { get; set; }
        public double[][][] coordinates { get; set; }
        
    }
    public class PropertyPolygon
    {
        public string NAME { get; set; }
        public string POPNAME { get; set; }
        public string CODE { get; set; }
        public int TYPE { get; set; }
        public int DISPLAY { get; set; }
        public int EXTENTION { get; set; }
        public string UPDATE { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public string ID { get; set; }
    }
    #endregion
}
