using System.Collections.Generic;

namespace FestiApp.Util.Util
{
    public class Properties
    {
        public string name { get; set; }
    }

    public class Crs
    {
        public string type { get; set; }
        public Properties properties { get; set; }
    }

    public class Properties2
    {
        public double distance { get; set; }
        public string distanceUnit { get; set; }
        public double duration { get; set; }
        public string timeUnit { get; set; }
        public int pointcount { get; set; }
    }

    public class Geometry
    {
        public string type { get; set; }
        public List<List<double>> coordinates { get; set; }
    }

    public class Feature
    {
        public string type { get; set; }
        public Crs crs { get; set; }
        public List<double> bbox { get; set; }
        public Properties2 properties { get; set; }
        public Geometry geometry { get; set; }
    }

    public class RouteObject
    {
        public string type { get; set; }
        public List<Feature> features { get; set; }
    }
}