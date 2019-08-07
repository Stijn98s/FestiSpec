using System.Collections.Generic;

namespace FestiApp.Util.Util
{
    public class Doc
    {
        public string addresstype { get; set; }
        public string buildingid { get; set; }
        public string street { get; set; }
        public string mainaddress { get; set; }
        public string municipality { get; set; }
        public string bagid { get; set; }
        public string purpose { get; set; }
        public string type { get; set; }
        public string city { get; set; }
        public string id { get; set; }
        public string naturalid { get; set; }
        public int area { get; set; }
        public string province { get; set; }
        public string building { get; set; }
        public string status { get; set; }
        public List<string> suggest { get; set; }
        public int housenumber { get; set; }
        public string postcode { get; set; }
        public List<string> centroid { get; set; }
        public string geom
        {
            get
            {
                return this._geom;
            }
            set
            {
                _geom = value;
                location = new Location(_geom);
            }
        }
        private string _geom { get; set; }
        public string geom_rd { get; set; }      
        public List<string> centroid_rd { get; set; }
        public List<string> category { get; set; }
        public int constructionyear { get; set; }
        public string source { get; set; }
        public string displayname { get; set; }
        public object _version_ { get; set; }
        public double score { get; set; }
        public Location location { get; set; }      
    }
}