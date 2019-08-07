using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Web;
using FestiAppViewModels;
using Newtonsoft.Json;

namespace FestiApp.Util.Util
{
    public class GeodanHelperService : IHelperService
    {
        private readonly GeodanClient _client;
        private readonly RouteClient _routeClient;
        private readonly string _key;
        public string Error { get; set; }
        public GeodanHelperService()
        {
            _client = new GeodanClient();
            _routeClient = new RouteClient();
            _key = "";
            try
            {
                _key = ConfigurationManager.ConnectionStrings["geodan_key"].ConnectionString;
            }
            catch
            {
                Error = "Kon key niet o[phalen uit de config";
            }  
            if (string.IsNullOrEmpty(_key))
            {
                Error = "Geen key";
            }
        }

        public async Task<SearchResult> SearchByQueryAsync(string searchterm)
        {
            SearchResult result;

            searchterm = HttpUtility.UrlPathEncode(searchterm);
            try
            {
                result = await _client.FreeAsync(searchterm, null, null, Wt.Json, null, null, 1, _key);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                throw;
            }

            return result;
        }
        public async Task<Doc> SearchByQueryAndKeyAsync(string searchterm)
        {           
            return GetDocValueByResult(await SearchByQueryAsync(searchterm));
        }

        public Doc GetDocValueByResult(SearchResult result)
        {
            int i = result.Response.Docs.Count;
            if(i < 1)
            {
                Error = "Geen resultaten";
                return null;
            }         
            string json = result.Response.Docs[0].ToString();
            try
            {
                Doc d = JsonConvert.DeserializeObject<Doc>(json);
                return d;
            }
            catch
            {
                Error = "Kon het object niet deserialiseren";
            }
            return null; 
        }
        public Properties2 GetProperties2ValueByResult(SearchResult result)
        {
            if (result.Response.Docs.Count < 1)
                return null;
            try
            {
                var json = result.Response.Docs[0].ToString();
                Properties2 d = JsonConvert.DeserializeObject<Properties2>(json);
                return d;
            }
            catch
            {
                return null;
            }
        }
        public bool IsValidSearchTerm(string zoekquery)
        {
            return zoekquery[0] == '*' && zoekquery[zoekquery.Length] == '*';
        }
        public async Task<Doc> GetDocByAdres(string city = null, string street = null, string housenumber = null,
            string postalcode = null)
        {
            string q;
            if (street != null && housenumber != null && postalcode != null)
            {
                q = $"fpostcode:{postalcode}+AND+housenumber:{housenumber}+AND+fstreet:{street}";
            }
            else if (postalcode != null && housenumber != null)
            {
                q = $"fpostcode:{postalcode}+AND+housenumber:{housenumber}";
            }
            else if (street != null && housenumber != null && city != null)
            {
                q = $"fcity:{city}+AND+housenumber:{housenumber}+AND+fstreet:{street}";
            }
            else
            {
                return null;
            }
            var r = await SearchByQueryAsync(q);           
            return GetDocValueByResult(r);
        }
        public Doc GetDocByBagId(string bagid)
        {
            string q = "bagid:"+ bagid;                           
            var r = SearchByQueryAsync(q);
            return GetDocValueByResult(r.Result);
        }
        public Doc GetDocByNaturalId(string naturalid)
        {
            string q = "naturalid:" + naturalid;                           
            var r = SearchByQueryAsync(q);
            return GetDocValueByResult(r.Result);
        }
        public Doc GetDocById(string id)
        {
            string q = "id:" + id;
            var r = SearchByQueryAsync(q);           
            return GetDocValueByResult(r.Result);
        }
        public async Task<DistanceViewModel> GetDistanceBetweenTwoLocatioins(string fromx, string fromy, string tox,
            string toy,
            NetworkType transporttype = NetworkType.Auto, Format f = Format.MinKm)
        {
            try
            {
                RouteObject result = await _routeClient.GetAsync(fromx, fromy, tox, toy, null, transporttype, null, null, f, OutputFormat.Json, null, _key);
                return new DistanceViewModel()
                {
                    Distance = result.features[0].properties.distance,
                    DistanceUnit = result.features[0].properties.distanceUnit,
                    Time = result.features[0].properties.duration,
                    TimeUnit = result.features[0].properties.timeUnit
                };

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        public async Task<DistanceViewModel> GetDistanceBetweenTwoLocatioinsByAdresId(string adresid1, string adresid2,
            NetworkType transporttype = NetworkType.Auto, Format format = Format.MinKm)
        {
            Doc doc1 = GetDocById(adresid1);
            Doc doc2 = GetDocById(adresid2);

            Location l1 = new Location(doc1.geom);
            Location l2 = new Location(doc2.geom);
            return await GetDistanceBetweenTwoLocatioins(l1.X.ToString(), l1.Y.ToString(), l2.X.ToString(), l2.Y.ToString(),transporttype,format);
            
        }
    }
}