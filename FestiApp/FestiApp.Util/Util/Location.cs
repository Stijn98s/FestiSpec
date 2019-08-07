using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace FestiApp.Util.Util
{
    public class Location
    {
        public Location(string point = "")
        {
            //POINT(5.17326 51.69016)
            string p = point;
            p = p.Replace("POINT(", "");
            p = p.Replace(")","");
            List<string> l = p.Split(' ').ToList();
            X = Convert.ToDouble(l[0], CultureInfo.InvariantCulture);
            Y = Convert.ToDouble(l[1], CultureInfo.InvariantCulture);
        }
        public string Name { get; set; } = "";
        public double X { get; set; }
        public double Y { get; set; }

    }
}