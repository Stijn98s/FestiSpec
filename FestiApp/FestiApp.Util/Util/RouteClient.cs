using System.Diagnostics;

namespace FestiApp.Util.Util
{

#pragma warning disable // Disable all warnings

    [System.CodeDom.Compiler.GeneratedCode("NSwag", "11.20.1.0 (NJsonSchema v9.11.0.0 (Newtonsoft.Json v9.0.0.0))")]
    public partial class RouteClient
    {
        private string _baseUrl = "https://services.geodan.nl/routing";
        private System.Lazy<Newtonsoft.Json.JsonSerializerSettings> _settings;

        public RouteClient()
        {
            _settings = new System.Lazy<Newtonsoft.Json.JsonSerializerSettings>(() =>
            {
                var settings = new Newtonsoft.Json.JsonSerializerSettings();
                UpdateJsonSerializerSettings(settings);
                return settings;
            });
        }

        public string BaseUrl
        {
            get { return _baseUrl; }
            set { _baseUrl = value; }
        }

        protected Newtonsoft.Json.JsonSerializerSettings JsonSerializerSettings { get { return _settings.Value; } }

        partial void UpdateJsonSerializerSettings(Newtonsoft.Json.JsonSerializerSettings settings);
        partial void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, string url);
        partial void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, System.Text.StringBuilder urlBuilder);
        partial void ProcessResponse(System.Net.Http.HttpClient client, System.Net.Http.HttpResponseMessage response);

        /// <summary>Calculates a route from A to B</summary>
        /// <param name="fromCoordX">The X coordinate or longitude of the starting point.<br>For example a number, representing meters for RD of Mercator projections, and decimal degrees for latitude and longitude (WGS84, <code>srs=EPSG:4326</code>).<br>The coordinate system to be used is defined in the <code>srs</code> parameter.</param>
        /// <param name="fromCoordY">The Y coordinate or latitude of the starting point</param>
        /// <param name="toCoordX">The X coordinate or longitude of the ending point</param>
        /// <param name="toCoordY">The Y coordinate or latitude of the ending point</param>
        /// <param name="srs">The coordinate system (projection) of the coordinates, defined by an EPSG code.<br>All common EPSG codes are supported.<br>Please include <code>EPSG:</code> when entering the code.</param>
        /// <param name="networkType">Defines the network on which the route calculation takes place</param>
        /// <param name="calcMode">Defines how the route is calculated.<br>Supported values:<ul><li><code>time</code>: fastest route</li><li><code>distance</code>: shortest route</li><li><code>cost</code>: cheapest route, based on a price per hour and a price per kilometer</ul></param>
        /// <param name="returnType">Defines which information of the calculated route is returned.<br>Supported values are:<ul><li><code>coords</code>: returns all the coordinates of the route and the total travel time and total distance</li><li><code>timedistance</code>: returns only the total travel time and total distance, along with the route made of only start and end points</li></ul>The units of the output are defined by the parameter <code>format</code>.</param>
        /// <param name="format">Defines the units in which the calculated travel time and total distance are printed.<br>Supported values:<ul><li><code>sec-m</code>: seconds, meters</li><li><code>sec-km</code>: seconds, kilometers</li><li><code>min-m</code>: minutes, meters</li><li><code>min-km</code>: minutes, kilometers</li></ul></param>
        /// <param name="outputFormat">Defines the type of format of the results.<br>Supported values:<ul><li><code>xml</code>: XML format.<br><code>Content-type</code>: <code>application/xml</code></li><li><code>geojson</code>: GeoJSON format.<br><code>Content-type</code>: <code>application/json</code> or in combination with the callback parameter: <code>application/javascript</code> (JSON-P)</li><li><code>json</code>: JSON format, identical to <code>geojson</code>.</li></ul></param>
        /// <param name="callback">The output JSON is embedded in a parameter of JavaScript function call with the name of the parameter <code>callback</code>.<br>The <code>Content-type</code> will be: <code>application/javascript</code> (JSON-P) (only when <code>format</code> is <code>geojson</code> or <code>json</code>).</param>
        /// <param name="servicekey">Authorization key for the service. The value can be requested via <a href="mailto:helpdesk@geodan.nl">helpdesk@geodan.nl</a>.</param>
        /// <returns>OK</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<RouteObject> GetAsync(string fromCoordX, string fromCoordY, string toCoordX, string toCoordY, string srs, NetworkType? networkType, CalcMode? calcMode, ReturnType? returnType, Format? format, OutputFormat? outputFormat, string callback, string servicekey)
        {
            return GetAsync(fromCoordX, fromCoordY, toCoordX, toCoordY, srs, networkType, calcMode, returnType, format, outputFormat, callback, servicekey, System.Threading.CancellationToken.None);
        }

        /// <summary>Calculates a route from A to B</summary>
        /// <param name="fromCoordX">The X coordinate or longitude of the starting point.<br>For example a number, representing meters for RD of Mercator projections, and decimal degrees for latitude and longitude (WGS84, <code>srs=EPSG:4326</code>).<br>The coordinate system to be used is defined in the <code>srs</code> parameter.</param>
        /// <param name="fromCoordY">The Y coordinate or latitude of the starting point</param>
        /// <param name="toCoordX">The X coordinate or longitude of the ending point</param>
        /// <param name="toCoordY">The Y coordinate or latitude of the ending point</param>
        /// <param name="srs">The coordinate system (projection) of the coordinates, defined by an EPSG code.<br>All common EPSG codes are supported.<br>Please include <code>EPSG:</code> when entering the code.</param>
        /// <param name="networkType">Defines the network on which the route calculation takes place</param>
        /// <param name="calcMode">Defines how the route is calculated.<br>Supported values:<ul><li><code>time</code>: fastest route</li><li><code>distance</code>: shortest route</li><li><code>cost</code>: cheapest route, based on a price per hour and a price per kilometer</ul></param>
        /// <param name="returnType">Defines which information of the calculated route is returned.<br>Supported values are:<ul><li><code>coords</code>: returns all the coordinates of the route and the total travel time and total distance</li><li><code>timedistance</code>: returns only the total travel time and total distance, along with the route made of only start and end points</li></ul>The units of the output are defined by the parameter <code>format</code>.</param>
        /// <param name="format">Defines the units in which the calculated travel time and total distance are printed.<br>Supported values:<ul><li><code>sec-m</code>: seconds, meters</li><li><code>sec-km</code>: seconds, kilometers</li><li><code>min-m</code>: minutes, meters</li><li><code>min-km</code>: minutes, kilometers</li></ul></param>
        /// <param name="outputFormat">Defines the type of format of the results.<br>Supported values:<ul><li><code>xml</code>: XML format.<br><code>Content-type</code>: <code>application/xml</code></li><li><code>geojson</code>: GeoJSON format.<br><code>Content-type</code>: <code>application/json</code> or in combination with the callback parameter: <code>application/javascript</code> (JSON-P)</li><li><code>json</code>: JSON format, identical to <code>geojson</code>.</li></ul></param>
        /// <param name="callback">The output JSON is embedded in a parameter of JavaScript function call with the name of the parameter <code>callback</code>.<br>The <code>Content-type</code> will be: <code>application/javascript</code> (JSON-P) (only when <code>format</code> is <code>geojson</code> or <code>json</code>).</param>
        /// <param name="servicekey">Authorization key for the service. The value can be requested via <a href="mailto:helpdesk@geodan.nl">helpdesk@geodan.nl</a>.</param>
        /// <returns>OK</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public async System.Threading.Tasks.Task<RouteObject> GetAsync(string fromCoordX, string fromCoordY, string toCoordX, string toCoordY, string srs, NetworkType? networkType, CalcMode? calcMode, ReturnType? returnType, Format? format, OutputFormat? outputFormat, string callback, string servicekey, System.Threading.CancellationToken cancellationToken)
        {
            if (fromCoordX == null)
                throw new System.ArgumentNullException("fromCoordX");

            if (fromCoordY == null)
                throw new System.ArgumentNullException("fromCoordY");

            if (toCoordX == null)
                throw new System.ArgumentNullException("toCoordX");

            if (toCoordY == null)
                throw new System.ArgumentNullException("toCoordY");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/route?");
            urlBuilder_.Append("fromCoordX=").Append(System.Uri.EscapeDataString(ConvertToString(fromCoordX, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append("fromCoordY=").Append(System.Uri.EscapeDataString(ConvertToString(fromCoordY, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append("toCoordX=").Append(System.Uri.EscapeDataString(ConvertToString(toCoordX, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append("toCoordY=").Append(System.Uri.EscapeDataString(ConvertToString(toCoordY, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            if (srs != null)
            {
                urlBuilder_.Append("srs=").Append(System.Uri.EscapeDataString(ConvertToString(srs, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            }
            if (networkType != null)
            {
                urlBuilder_.Append("networkType=").Append(System.Uri.EscapeDataString(ConvertToString(networkType, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            }
            if (calcMode != null)
            {
                urlBuilder_.Append("calcMode=").Append(System.Uri.EscapeDataString(ConvertToString(calcMode, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            }
            if (returnType != null)
            {
                urlBuilder_.Append("returnType=").Append(System.Uri.EscapeDataString(ConvertToString(returnType, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            }
            if (format != null)
            {
                urlBuilder_.Append("format=").Append(System.Uri.EscapeDataString(ConvertToString(format, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            }
            if (outputFormat != null)
            {
                urlBuilder_.Append("outputFormat=").Append(System.Uri.EscapeDataString(ConvertToString(outputFormat, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            }
            if (callback != null)
            {
                urlBuilder_.Append("callback=").Append(System.Uri.EscapeDataString(ConvertToString(callback, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            }
            if (servicekey != null)
            {
                urlBuilder_.Append("servicekey=").Append(System.Uri.EscapeDataString(ConvertToString(servicekey, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            }
            urlBuilder_.Length--;
            Debug.WriteLine(urlBuilder_.ToString());
            var client_ = new System.Net.Http.HttpClient();
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    request_.Method = new System.Net.Http.HttpMethod("GET");
                    request_.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            var result_ = default(RouteObject);
                            try
                            {
                                result_ = Newtonsoft.Json.JsonConvert.DeserializeObject<RouteObject>(responseData_, _settings.Value);
                                return result_;
                            }
                            catch (System.Exception exception_)
                            {
                                throw new SwaggerException("Could not deserialize the response body.", (int)response_.StatusCode, responseData_, headers_, exception_);
                            }
                        }
                        else
                        if (status_ == "403")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new SwaggerException("No credits on given account (<code>servicekey</code> parameter) or not logged in.", (int)response_.StatusCode, responseData_, headers_, null);
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new SwaggerException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }

                        return default(RouteObject);
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (client_ != null)
                    client_.Dispose();
            }
        }

        private string ConvertToString(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value is System.Enum)
            {
                string name = System.Enum.GetName(value.GetType(), value);
                if (name != null)
                {
                    var field = System.Reflection.IntrospectionExtensions.GetTypeInfo(value.GetType()).GetDeclaredField(name);
                    if (field != null)
                    {
                        var attribute = System.Reflection.CustomAttributeExtensions.GetCustomAttribute(field, typeof(System.Runtime.Serialization.EnumMemberAttribute))
                            as System.Runtime.Serialization.EnumMemberAttribute;
                        if (attribute != null)
                        {
                            return attribute.Value;
                        }
                    }
                }
            }
            else if (value is byte[])
            {
                return System.Convert.ToBase64String((byte[])value);
            }
            else if (value != null && value.GetType().IsArray)
            {
                var array = System.Linq.Enumerable.OfType<object>((System.Array)value);
                return string.Join(",", System.Linq.Enumerable.Select(array, o => ConvertToString(o, cultureInfo)));
            }

            return System.Convert.ToString(value, cultureInfo);
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("NSwag", "11.20.1.0 (NJsonSchema v9.11.0.0 (Newtonsoft.Json v9.0.0.0))")]
    public partial class Client
    {
        private string _baseUrl = "/routing";
        private System.Lazy<Newtonsoft.Json.JsonSerializerSettings> _settings;

        public Client()
        {
            _settings = new System.Lazy<Newtonsoft.Json.JsonSerializerSettings>(() =>
            {
                var settings = new Newtonsoft.Json.JsonSerializerSettings();
                UpdateJsonSerializerSettings(settings);
                return settings;
            });
        }

        public string BaseUrl
        {
            get { return _baseUrl; }
            set { _baseUrl = value; }
        }

        protected Newtonsoft.Json.JsonSerializerSettings JsonSerializerSettings { get { return _settings.Value; } }

        partial void UpdateJsonSerializerSettings(Newtonsoft.Json.JsonSerializerSettings settings);
        partial void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, string url);
        partial void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, System.Text.StringBuilder urlBuilder);
        partial void ProcessResponse(System.Net.Http.HttpClient client, System.Net.Http.HttpResponseMessage response);

        /// <summary>Calculates a route of A-to-B based on addresses</summary>
        /// <param name="from">Free form address which will get geocoded and routed with</param>
        /// <param name="to">Free form address which will get geocoded and routed with</param>
        /// <param name="networkType">Defines the network on which the route calculation takes place</param>
        /// <param name="servicekey">Authorization key for the service. The value can be requested via <a href="mailto:helpdesk@geodan.nl">helpdesk@geodan.nl</a>.</param>
        /// <returns>OK</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<object> AddressrouteAsync(string from, string to, NetworkType? networkType, string servicekey)
        {
            return AddressrouteAsync(from, to, networkType, servicekey, System.Threading.CancellationToken.None);
        }

        /// <summary>Calculates a route of A-to-B based on addresses</summary>
        /// <param name="from">Free form address which will get geocoded and routed with</param>
        /// <param name="to">Free form address which will get geocoded and routed with</param>
        /// <param name="networkType">Defines the network on which the route calculation takes place</param>
        /// <param name="servicekey">Authorization key for the service. The value can be requested via <a href="mailto:helpdesk@geodan.nl">helpdesk@geodan.nl</a>.</param>
        /// <returns>OK</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public async System.Threading.Tasks.Task<object> AddressrouteAsync(string from, string to, NetworkType? networkType, string servicekey, System.Threading.CancellationToken cancellationToken)
        {
            if (from == null)
                throw new System.ArgumentNullException("from");

            if (to == null)
                throw new System.ArgumentNullException("to");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/addressroute?");
            urlBuilder_.Append("from=").Append(System.Uri.EscapeDataString(ConvertToString(from, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append("to=").Append(System.Uri.EscapeDataString(ConvertToString(to, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            if (networkType != null)
            {
                urlBuilder_.Append("networkType=").Append(System.Uri.EscapeDataString(ConvertToString(networkType, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            }
            if (servicekey != null)
            {
                urlBuilder_.Append("servicekey=").Append(System.Uri.EscapeDataString(ConvertToString(servicekey, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            }
            urlBuilder_.Length--;

            var client_ = new System.Net.Http.HttpClient();
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    request_.Method = new System.Net.Http.HttpMethod("GET");
                    request_.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            var result_ = default(object);
                            try
                            {
                                result_ = Newtonsoft.Json.JsonConvert.DeserializeObject<object>(responseData_, _settings.Value);
                                return result_;
                            }
                            catch (System.Exception exception_)
                            {
                                throw new SwaggerException("Could not deserialize the response body.", (int)response_.StatusCode, responseData_, headers_, exception_);
                            }
                        }
                        else
                        if (status_ == "404")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            var result_ = default(Response);
                            try
                            {
                                result_ = Newtonsoft.Json.JsonConvert.DeserializeObject<Response>(responseData_, _settings.Value);
                            }
                            catch (System.Exception exception_)
                            {
                                throw new SwaggerException("Could not deserialize the response body.", (int)response_.StatusCode, responseData_, headers_, exception_);
                            }
                            throw new SwaggerException<Response>("One or more of the requested addresses could not be geocoded", (int)response_.StatusCode, responseData_, headers_, result_, null);
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new SwaggerException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }

                        return default(object);
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (client_ != null)
                    client_.Dispose();
            }
        }

        private string ConvertToString(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value is System.Enum)
            {
                string name = System.Enum.GetName(value.GetType(), value);
                if (name != null)
                {
                    var field = System.Reflection.IntrospectionExtensions.GetTypeInfo(value.GetType()).GetDeclaredField(name);
                    if (field != null)
                    {
                        var attribute = System.Reflection.CustomAttributeExtensions.GetCustomAttribute(field, typeof(System.Runtime.Serialization.EnumMemberAttribute))
                            as System.Runtime.Serialization.EnumMemberAttribute;
                        if (attribute != null)
                        {
                            return attribute.Value;
                        }
                    }
                }
            }
            else if (value is byte[])
            {
                return System.Convert.ToBase64String((byte[])value);
            }
            else if (value != null && value.GetType().IsArray)
            {
                var array = System.Linq.Enumerable.OfType<object>((System.Array)value);
                return string.Join(",", System.Linq.Enumerable.Select(array, o => ConvertToString(o, cultureInfo)));
            }

            return System.Convert.ToString(value, cultureInfo);
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("NSwag", "11.20.1.0 (NJsonSchema v9.11.0.0 (Newtonsoft.Json v9.0.0.0))")]
    public partial class BatchRouteClient
    {
        private string _baseUrl = "/routing";
        private System.Lazy<Newtonsoft.Json.JsonSerializerSettings> _settings;

        public BatchRouteClient()
        {
            _settings = new System.Lazy<Newtonsoft.Json.JsonSerializerSettings>(() =>
            {
                var settings = new Newtonsoft.Json.JsonSerializerSettings();
                UpdateJsonSerializerSettings(settings);
                return settings;
            });
        }

        public string BaseUrl
        {
            get { return _baseUrl; }
            set { _baseUrl = value; }
        }

        protected Newtonsoft.Json.JsonSerializerSettings JsonSerializerSettings { get { return _settings.Value; } }

        partial void UpdateJsonSerializerSettings(Newtonsoft.Json.JsonSerializerSettings settings);
        partial void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, string url);
        partial void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, System.Text.StringBuilder urlBuilder);
        partial void ProcessResponse(System.Net.Http.HttpClient client, System.Net.Http.HttpResponseMessage response);

        /// <summary>Calculates a batch of A-to-B routes in a single request</summary>
        /// <param name="locations">List of locations for which the route has to be calculated, separated by a comma (<code>,</code>)</param>
        /// <param name="srs">The coordinate system (projection) of the coordinates, defined by an EPSG code.<br>All common EPSG codes are supported.<br>Please include <code>EPSG:</code> when entering the code.</param>
        /// <param name="networkType">Defines the network on which the route calculation takes place</param>
        /// <param name="calcMode">Defines how the route is calculated.<br>Supported values:<ul><li><code>time</code>: fastest route</li><li><code>distance</code>: shortest route</li><li><code>cost</code>: cheapest route, based on a price per hour and a price per kilometer</ul></param>
        /// <param name="returnType">Defines which information of the calculated route is returned.<br>Supported values are:<ul><li><code>coords</code>: returns all the coordinates of the route and the total travel time and total distance</li><li><code>timedistance</code>: returns only the total travel time and total distance, along with a route with coordinates of start and end point</li></ul></param>
        /// <param name="format">Defines the units in which the calculated travel time and total distance are printed.<br>Supported values:<ul><li><code>sec-m</code>: seconds, meters</li><li><code>sec-km</code>: seconds, kilometers</li><li><code>min-m</code>: minutes, meters</li><li><code>min-km</code>: minutes, kilometers</li></ul></param>
        /// <param name="outputFormat">Defines the type of format of the results.<br>Supported values:<ul><li><code>xml</code>: XML format, compatible with the old service on geoserver.nl/routeerservice.<br><code>Content-type</code>: <code>application/xml</code></li><li><code>geojson</code>: GeoJSON format.<br><code>Content-type</code>: <code>application/json</code> or in combination with the callback parameter: <code>application/javascript</code> (JSON-P)</li><li><code>json</code>: JSON format, identical to <code>geojson</code>.</li></ul></param>
        /// <param name="callback">The output JSON is embedded in a parameter of JavaScript function call with the name of the parameter <code>callback</code>.<br>The <code>Content-type</code> will be: <code>application/javascript</code> (JSON-P) (only when <code>format</code> is <code>geojson</code> or <code>json</code>).</param>
        /// <param name="shortRoutes">Performance enhancing parameter.<br>If the routes to be calculated are short, set this parameter to <code>true</code>; this will reduce the calculation time.<br>On longer routes, this will make the route calculation longer.<br>On the <code>auto</code> and <code>vrachtwagen</code> networks, the maximum distance of a calculated route should be 50 km.<br>On the <code>gsps_nl</code> network (pedestrian network), the distance is a lot shorter because this network has a higher density.</param>
        /// <param name="servicekey">Authorization key for the service. The value can be requested via <a href="mailto:helpdesk@geodan.nl">helpdesk@geodan.nl</a>.</param>
        /// <returns>OK</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<object> PostAsync(System.Collections.Generic.IEnumerable<RouteCoordinates> locations, string srs, NetworkType2? networkType, CalcMode2? calcMode, ReturnType2? returnType, Format2? format, OutputFormat2? outputFormat, string callback, bool? shortRoutes, string servicekey)
        {
            return PostAsync(locations, srs, networkType, calcMode, returnType, format, outputFormat, callback, shortRoutes, servicekey, System.Threading.CancellationToken.None);
        }

        /// <summary>Calculates a batch of A-to-B routes in a single request</summary>
        /// <param name="locations">List of locations for which the route has to be calculated, separated by a comma (<code>,</code>)</param>
        /// <param name="srs">The coordinate system (projection) of the coordinates, defined by an EPSG code.<br>All common EPSG codes are supported.<br>Please include <code>EPSG:</code> when entering the code.</param>
        /// <param name="networkType">Defines the network on which the route calculation takes place</param>
        /// <param name="calcMode">Defines how the route is calculated.<br>Supported values:<ul><li><code>time</code>: fastest route</li><li><code>distance</code>: shortest route</li><li><code>cost</code>: cheapest route, based on a price per hour and a price per kilometer</ul></param>
        /// <param name="returnType">Defines which information of the calculated route is returned.<br>Supported values are:<ul><li><code>coords</code>: returns all the coordinates of the route and the total travel time and total distance</li><li><code>timedistance</code>: returns only the total travel time and total distance, along with a route with coordinates of start and end point</li></ul></param>
        /// <param name="format">Defines the units in which the calculated travel time and total distance are printed.<br>Supported values:<ul><li><code>sec-m</code>: seconds, meters</li><li><code>sec-km</code>: seconds, kilometers</li><li><code>min-m</code>: minutes, meters</li><li><code>min-km</code>: minutes, kilometers</li></ul></param>
        /// <param name="outputFormat">Defines the type of format of the results.<br>Supported values:<ul><li><code>xml</code>: XML format, compatible with the old service on geoserver.nl/routeerservice.<br><code>Content-type</code>: <code>application/xml</code></li><li><code>geojson</code>: GeoJSON format.<br><code>Content-type</code>: <code>application/json</code> or in combination with the callback parameter: <code>application/javascript</code> (JSON-P)</li><li><code>json</code>: JSON format, identical to <code>geojson</code>.</li></ul></param>
        /// <param name="callback">The output JSON is embedded in a parameter of JavaScript function call with the name of the parameter <code>callback</code>.<br>The <code>Content-type</code> will be: <code>application/javascript</code> (JSON-P) (only when <code>format</code> is <code>geojson</code> or <code>json</code>).</param>
        /// <param name="shortRoutes">Performance enhancing parameter.<br>If the routes to be calculated are short, set this parameter to <code>true</code>; this will reduce the calculation time.<br>On longer routes, this will make the route calculation longer.<br>On the <code>auto</code> and <code>vrachtwagen</code> networks, the maximum distance of a calculated route should be 50 km.<br>On the <code>gsps_nl</code> network (pedestrian network), the distance is a lot shorter because this network has a higher density.</param>
        /// <param name="servicekey">Authorization key for the service. The value can be requested via <a href="mailto:helpdesk@geodan.nl">helpdesk@geodan.nl</a>.</param>
        /// <returns>OK</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public async System.Threading.Tasks.Task<object> PostAsync(System.Collections.Generic.IEnumerable<RouteCoordinates> locations, string srs, NetworkType2? networkType, CalcMode2? calcMode, ReturnType2? returnType, Format2? format, OutputFormat2? outputFormat, string callback, bool? shortRoutes, string servicekey, System.Threading.CancellationToken cancellationToken)
        {
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/batchroute?");
            if (srs != null)
            {
                urlBuilder_.Append("srs=").Append(System.Uri.EscapeDataString(ConvertToString(srs, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            }
            if (networkType != null)
            {
                urlBuilder_.Append("networkType=").Append(System.Uri.EscapeDataString(ConvertToString(networkType, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            }
            if (calcMode != null)
            {
                urlBuilder_.Append("calcMode=").Append(System.Uri.EscapeDataString(ConvertToString(calcMode, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            }
            if (returnType != null)
            {
                urlBuilder_.Append("returnType=").Append(System.Uri.EscapeDataString(ConvertToString(returnType, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            }
            if (format != null)
            {
                urlBuilder_.Append("format=").Append(System.Uri.EscapeDataString(ConvertToString(format, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            }
            if (outputFormat != null)
            {
                urlBuilder_.Append("outputFormat=").Append(System.Uri.EscapeDataString(ConvertToString(outputFormat, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            }
            if (callback != null)
            {
                urlBuilder_.Append("callback=").Append(System.Uri.EscapeDataString(ConvertToString(callback, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            }
            if (shortRoutes != null)
            {
                urlBuilder_.Append("shortRoutes=").Append(System.Uri.EscapeDataString(ConvertToString(shortRoutes, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            }
            if (servicekey != null)
            {
                urlBuilder_.Append("servicekey=").Append(System.Uri.EscapeDataString(ConvertToString(servicekey, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            }
            urlBuilder_.Length--;

            var client_ = new System.Net.Http.HttpClient();
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    var content_ = new System.Net.Http.StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(locations, _settings.Value));
                    content_.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");
                    request_.Content = content_;
                    request_.Method = new System.Net.Http.HttpMethod("POST");
                    request_.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            var result_ = default(object);
                            try
                            {
                                result_ = Newtonsoft.Json.JsonConvert.DeserializeObject<object>(responseData_, _settings.Value);
                                return result_;
                            }
                            catch (System.Exception exception_)
                            {
                                throw new SwaggerException("Could not deserialize the response body.", (int)response_.StatusCode, responseData_, headers_, exception_);
                            }
                        }
                        else
                        if (status_ == "403")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new SwaggerException("No credits on given account (<code>servicekey</code> parameter) or not logged in.", (int)response_.StatusCode, responseData_, headers_, null);
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new SwaggerException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }

                        return default(object);
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (client_ != null)
                    client_.Dispose();
            }
        }

        private string ConvertToString(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value is System.Enum)
            {
                string name = System.Enum.GetName(value.GetType(), value);
                if (name != null)
                {
                    var field = System.Reflection.IntrospectionExtensions.GetTypeInfo(value.GetType()).GetDeclaredField(name);
                    if (field != null)
                    {
                        var attribute = System.Reflection.CustomAttributeExtensions.GetCustomAttribute(field, typeof(System.Runtime.Serialization.EnumMemberAttribute))
                            as System.Runtime.Serialization.EnumMemberAttribute;
                        if (attribute != null)
                        {
                            return attribute.Value;
                        }
                    }
                }
            }
            else if (value is byte[])
            {
                return System.Convert.ToBase64String((byte[])value);
            }
            else if (value != null && value.GetType().IsArray)
            {
                var array = System.Linq.Enumerable.OfType<object>((System.Array)value);
                return string.Join(",", System.Linq.Enumerable.Select(array, o => ConvertToString(o, cultureInfo)));
            }

            return System.Convert.ToString(value, cultureInfo);
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("NSwag", "11.20.1.0 (NJsonSchema v9.11.0.0 (Newtonsoft.Json v9.0.0.0))")]
    public partial class TspClient
    {
        private string _baseUrl = "/routing";
        private System.Lazy<Newtonsoft.Json.JsonSerializerSettings> _settings;

        public TspClient()
        {
            _settings = new System.Lazy<Newtonsoft.Json.JsonSerializerSettings>(() =>
            {
                var settings = new Newtonsoft.Json.JsonSerializerSettings();
                UpdateJsonSerializerSettings(settings);
                return settings;
            });
        }

        public string BaseUrl
        {
            get { return _baseUrl; }
            set { _baseUrl = value; }
        }

        protected Newtonsoft.Json.JsonSerializerSettings JsonSerializerSettings { get { return _settings.Value; } }

        partial void UpdateJsonSerializerSettings(Newtonsoft.Json.JsonSerializerSettings settings);
        partial void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, string url);
        partial void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, System.Text.StringBuilder urlBuilder);
        partial void ProcessResponse(System.Net.Http.HttpClient client, System.Net.Http.HttpResponseMessage response);

        /// <summary>Calculates a route from A to B solving the Travelling Salesman Problem (TSP)</summary>
        /// <param name="locations">List of locations to be visited, separated by a comma (<code>,</code>)</param>
        /// <param name="tspMode">Supported values:<ul><li><code>Open</code>: can start and end on any given location</li><li><code>OpenEnd</code>: first given location is the starting point and the route can end on any other location</li><li><code>OpenStart</code>: last given location is ending point, the route can start on any other location</li><li><code>StartEnd</code>: first given location is the starting point and last given location is ending point</li><li><code>Round</code>: first given location is the starting point and the ending point</li></ul></param>
        /// <param name="identifier">Identifier chosen by the user.<br>Can be both text or integer.</param>
        /// <param name="srs">The coordinate system (projection) of the coordinates, defined by an EPSG code.<br>All common EPSG codes are supported.<br>Please include <code>EPSG:</code> when entering the code.</param>
        /// <param name="networkType">Defines the network on which the route calculation takes place</param>
        /// <param name="calcMode">Defines how the route is calculated.<br>Supported values:<ul><li><code>time</code>: fastest route</li><li><code>distance</code>: shortest route</li><li><code>cost</code>: cheapest route, based on a price per hour and a price per kilometer</ul></param>
        /// <param name="symmetric">Defines whether the calculation makes a distinction between outbound and inbound routes. When ```true``` the route from A to B is always identical to the route from B to A and the calculation will be performed much faster.</param>
        /// <param name="servicekey">Authorization key for the service. The value can be requested via <a href="mailto:helpdesk@geodan.nl">helpdesk@geodan.nl</a>.</param>
        /// <returns>OK</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<object> PostAsync(System.Collections.Generic.IEnumerable<RouteStop> locations, TspMode tspMode, string identifier, string srs, NetworkType3? networkType, CalcMode3? calcMode, bool? symmetric, string servicekey)
        {
            return PostAsync(locations, tspMode, identifier, srs, networkType, calcMode, symmetric, servicekey, System.Threading.CancellationToken.None);
        }

        /// <summary>Calculates a route from A to B solving the Travelling Salesman Problem (TSP)</summary>
        /// <param name="locations">List of locations to be visited, separated by a comma (<code>,</code>)</param>
        /// <param name="tspMode">Supported values:<ul><li><code>Open</code>: can start and end on any given location</li><li><code>OpenEnd</code>: first given location is the starting point and the route can end on any other location</li><li><code>OpenStart</code>: last given location is ending point, the route can start on any other location</li><li><code>StartEnd</code>: first given location is the starting point and last given location is ending point</li><li><code>Round</code>: first given location is the starting point and the ending point</li></ul></param>
        /// <param name="identifier">Identifier chosen by the user.<br>Can be both text or integer.</param>
        /// <param name="srs">The coordinate system (projection) of the coordinates, defined by an EPSG code.<br>All common EPSG codes are supported.<br>Please include <code>EPSG:</code> when entering the code.</param>
        /// <param name="networkType">Defines the network on which the route calculation takes place</param>
        /// <param name="calcMode">Defines how the route is calculated.<br>Supported values:<ul><li><code>time</code>: fastest route</li><li><code>distance</code>: shortest route</li><li><code>cost</code>: cheapest route, based on a price per hour and a price per kilometer</ul></param>
        /// <param name="symmetric">Defines whether the calculation makes a distinction between outbound and inbound routes. When ```true``` the route from A to B is always identical to the route from B to A and the calculation will be performed much faster.</param>
        /// <param name="servicekey">Authorization key for the service. The value can be requested via <a href="mailto:helpdesk@geodan.nl">helpdesk@geodan.nl</a>.</param>
        /// <returns>OK</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public async System.Threading.Tasks.Task<object> PostAsync(System.Collections.Generic.IEnumerable<RouteStop> locations, TspMode tspMode, string identifier, string srs, NetworkType3? networkType, CalcMode3? calcMode, bool? symmetric, string servicekey, System.Threading.CancellationToken cancellationToken)
        {
            if (tspMode == null)
                throw new System.ArgumentNullException("tspMode");

            if (identifier == null)
                throw new System.ArgumentNullException("identifier");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/tsp?");
            urlBuilder_.Append("tspMode=").Append(System.Uri.EscapeDataString(ConvertToString(tspMode, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append("identifier=").Append(System.Uri.EscapeDataString(ConvertToString(identifier, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            if (srs != null)
            {
                urlBuilder_.Append("srs=").Append(System.Uri.EscapeDataString(ConvertToString(srs, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            }
            if (networkType != null)
            {
                urlBuilder_.Append("networkType=").Append(System.Uri.EscapeDataString(ConvertToString(networkType, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            }
            if (calcMode != null)
            {
                urlBuilder_.Append("calcMode=").Append(System.Uri.EscapeDataString(ConvertToString(calcMode, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            }
            if (symmetric != null)
            {
                urlBuilder_.Append("symmetric=").Append(System.Uri.EscapeDataString(ConvertToString(symmetric, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            }
            if (servicekey != null)
            {
                urlBuilder_.Append("servicekey=").Append(System.Uri.EscapeDataString(ConvertToString(servicekey, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            }
            urlBuilder_.Length--;

            var client_ = new System.Net.Http.HttpClient();
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    var content_ = new System.Net.Http.StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(locations, _settings.Value));
                    content_.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");
                    request_.Content = content_;
                    request_.Method = new System.Net.Http.HttpMethod("POST");
                    request_.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            var result_ = default(object);
                            try
                            {
                                result_ = Newtonsoft.Json.JsonConvert.DeserializeObject<object>(responseData_, _settings.Value);
                                return result_;
                            }
                            catch (System.Exception exception_)
                            {
                                throw new SwaggerException("Could not deserialize the response body.", (int)response_.StatusCode, responseData_, headers_, exception_);
                            }
                        }
                        else
                        if (status_ == "403")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new SwaggerException("No credits on given account (<code>servicekey</code> parameter) or not logged in.", (int)response_.StatusCode, responseData_, headers_, null);
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new SwaggerException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }

                        return default(object);
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (client_ != null)
                    client_.Dispose();
            }
        }

        private string ConvertToString(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value is System.Enum)
            {
                string name = System.Enum.GetName(value.GetType(), value);
                if (name != null)
                {
                    var field = System.Reflection.IntrospectionExtensions.GetTypeInfo(value.GetType()).GetDeclaredField(name);
                    if (field != null)
                    {
                        var attribute = System.Reflection.CustomAttributeExtensions.GetCustomAttribute(field, typeof(System.Runtime.Serialization.EnumMemberAttribute))
                            as System.Runtime.Serialization.EnumMemberAttribute;
                        if (attribute != null)
                        {
                            return attribute.Value;
                        }
                    }
                }
            }
            else if (value is byte[])
            {
                return System.Convert.ToBase64String((byte[])value);
            }
            else if (value != null && value.GetType().IsArray)
            {
                var array = System.Linq.Enumerable.OfType<object>((System.Array)value);
                return string.Join(",", System.Linq.Enumerable.Select(array, o => ConvertToString(o, cultureInfo)));
            }

            return System.Convert.ToString(value, cultureInfo);
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("NSwag", "11.20.1.0 (NJsonSchema v9.11.0.0 (Newtonsoft.Json v9.0.0.0))")]
    public partial class IsochroneClient
    {
        private string _baseUrl = "/routing";
        private System.Lazy<Newtonsoft.Json.JsonSerializerSettings> _settings;

        public IsochroneClient()
        {
            _settings = new System.Lazy<Newtonsoft.Json.JsonSerializerSettings>(() =>
            {
                var settings = new Newtonsoft.Json.JsonSerializerSettings();
                UpdateJsonSerializerSettings(settings);
                return settings;
            });
        }

        public string BaseUrl
        {
            get { return _baseUrl; }
            set { _baseUrl = value; }
        }

        protected Newtonsoft.Json.JsonSerializerSettings JsonSerializerSettings { get { return _settings.Value; } }

        partial void UpdateJsonSerializerSettings(Newtonsoft.Json.JsonSerializerSettings settings);
        partial void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, string url);
        partial void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, System.Text.StringBuilder urlBuilder);
        partial void ProcessResponse(System.Net.Http.HttpClient client, System.Net.Http.HttpResponseMessage response);

        /// <summary>Returns an attainability area based on an amount of time or distance</summary>
        /// <param name="fromCoordX">The X coordinate or longitude of the starting point.<br>For example a number, representing meters for RD of Mercator projections, and decimal degrees for latitude and longitude (WGS84, <code>srs=EPSG:4326</code>).<br>The coordinate system to be used is defined in the <code>srs</code> parameter.<br>Required if no end point coordinates are supplied.</param>
        /// <param name="fromCoordY">The Y coordinate or latitude of the starting point.<br>Required if no end point coordinates are supplied.</param>
        /// <param name="toCoordX">The X coordinate or longitude of the ending point.<br>Required if no start point coordinates are supplied.</param>
        /// <param name="toCoordY">The Y coordinate or latitude of the ending point.<br>Required if no start point coordinates are supplied.</param>
        /// <param name="srs">The coordinate system (projection) of the coordinates, defined by an EPSG code.<br>All common EPSG codes are supported.<br>Please include <code>EPSG:</code> when entering the code.</param>
        /// <param name="networkType">Defines the network on which the route calculation takes place</param>
        /// <param name="calcMode">Defines how the route is calculated.<br>Supported values:<ul><li><code>time</code>: fastest route</li><li><code>distance</code>: shortest route</li><li><code>cost</code>: cheapest route, based on a price per hour and a price per kilometer</ul></param>
        /// <param name="calcSize">Defines the size of the isochrone.<br>The corresponding unit depends on the chosen <code>calcMode</code>:<ul><li>minutes when <code>calcMode=time</code></li><li>kilometers when <code>calcMode=distance</code></li><li>euros when <code>calcMode=cost</code></li></ul></param>
        /// <param name="outputFormat">Defines the type of format of the results.<br>Supported values:<ul><li><code>xml</code>: XML format, compatible with the old service on geoserver.nl/routeerservice.<br><code>Content-type</code>: <code>application/xml</code></li><li><code>geojson</code>: GeoJSON format.<br><code>Content-type</code>: <code>application/json</code> or in combination with the callback parameter: <code>application/javascript</code> (JSON-P)</li><li><code>json</code>: JSON format, identical to <code>geojson</code>.</li></ul></param>
        /// <param name="callback">The output JSON is embedded in a parameter of JavaScript function call with the name of the parameter <code>callback</code>.<br>The <code>Content-type</code> will be: <code>application/javascript</code> (JSON-P) (only when <code>format</code> is <code>geojson</code> or <code>json</code>).</param>
        /// <param name="angle">Defines how close the polygon fits the network.<br>The larger the angle the smoother the polygon, except for the value <code>0</code> which results in the Convex Hull.<br>Values between <code>0</code> and <code>45</code> are supported, default value is <code>3</code>.</param>
        /// <param name="steps">Defines how many isochrones are calculated.<br>The value of <code>calcSize</code> is used as interval.<br>When using <code>steps</code>, only the first value specified in <code>calcSize</code> will be used.</param>
        /// <param name="servicekey">Authorization key for the service. The value can be requested via <a href="mailto:helpdesk@geodan.nl">helpdesk@geodan.nl</a>.</param>
        /// <returns>OK</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<object> GetAsync(string fromCoordX, string fromCoordY, string toCoordX, string toCoordY, string srs, NetworkType4? networkType, CalcMode4? calcMode, string calcSize, OutputFormat3? outputFormat, string callback, double? angle, string steps, string servicekey)
        {
            return GetAsync(fromCoordX, fromCoordY, toCoordX, toCoordY, srs, networkType, calcMode, calcSize, outputFormat, callback, angle, steps, servicekey, System.Threading.CancellationToken.None);
        }

        /// <summary>Returns an attainability area based on an amount of time or distance</summary>
        /// <param name="fromCoordX">The X coordinate or longitude of the starting point.<br>For example a number, representing meters for RD of Mercator projections, and decimal degrees for latitude and longitude (WGS84, <code>srs=EPSG:4326</code>).<br>The coordinate system to be used is defined in the <code>srs</code> parameter.<br>Required if no end point coordinates are supplied.</param>
        /// <param name="fromCoordY">The Y coordinate or latitude of the starting point.<br>Required if no end point coordinates are supplied.</param>
        /// <param name="toCoordX">The X coordinate or longitude of the ending point.<br>Required if no start point coordinates are supplied.</param>
        /// <param name="toCoordY">The Y coordinate or latitude of the ending point.<br>Required if no start point coordinates are supplied.</param>
        /// <param name="srs">The coordinate system (projection) of the coordinates, defined by an EPSG code.<br>All common EPSG codes are supported.<br>Please include <code>EPSG:</code> when entering the code.</param>
        /// <param name="networkType">Defines the network on which the route calculation takes place</param>
        /// <param name="calcMode">Defines how the route is calculated.<br>Supported values:<ul><li><code>time</code>: fastest route</li><li><code>distance</code>: shortest route</li><li><code>cost</code>: cheapest route, based on a price per hour and a price per kilometer</ul></param>
        /// <param name="calcSize">Defines the size of the isochrone.<br>The corresponding unit depends on the chosen <code>calcMode</code>:<ul><li>minutes when <code>calcMode=time</code></li><li>kilometers when <code>calcMode=distance</code></li><li>euros when <code>calcMode=cost</code></li></ul></param>
        /// <param name="outputFormat">Defines the type of format of the results.<br>Supported values:<ul><li><code>xml</code>: XML format, compatible with the old service on geoserver.nl/routeerservice.<br><code>Content-type</code>: <code>application/xml</code></li><li><code>geojson</code>: GeoJSON format.<br><code>Content-type</code>: <code>application/json</code> or in combination with the callback parameter: <code>application/javascript</code> (JSON-P)</li><li><code>json</code>: JSON format, identical to <code>geojson</code>.</li></ul></param>
        /// <param name="callback">The output JSON is embedded in a parameter of JavaScript function call with the name of the parameter <code>callback</code>.<br>The <code>Content-type</code> will be: <code>application/javascript</code> (JSON-P) (only when <code>format</code> is <code>geojson</code> or <code>json</code>).</param>
        /// <param name="angle">Defines how close the polygon fits the network.<br>The larger the angle the smoother the polygon, except for the value <code>0</code> which results in the Convex Hull.<br>Values between <code>0</code> and <code>45</code> are supported, default value is <code>3</code>.</param>
        /// <param name="steps">Defines how many isochrones are calculated.<br>The value of <code>calcSize</code> is used as interval.<br>When using <code>steps</code>, only the first value specified in <code>calcSize</code> will be used.</param>
        /// <param name="servicekey">Authorization key for the service. The value can be requested via <a href="mailto:helpdesk@geodan.nl">helpdesk@geodan.nl</a>.</param>
        /// <returns>OK</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public async System.Threading.Tasks.Task<object> GetAsync(string fromCoordX, string fromCoordY, string toCoordX, string toCoordY, string srs, NetworkType4? networkType, CalcMode4? calcMode, string calcSize, OutputFormat3? outputFormat, string callback, double? angle, string steps, string servicekey, System.Threading.CancellationToken cancellationToken)
        {
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/isochrone?");
            if (fromCoordX != null)
            {
                urlBuilder_.Append("fromCoordX=").Append(System.Uri.EscapeDataString(ConvertToString(fromCoordX, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            }
            if (fromCoordY != null)
            {
                urlBuilder_.Append("fromCoordY=").Append(System.Uri.EscapeDataString(ConvertToString(fromCoordY, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            }
            if (toCoordX != null)
            {
                urlBuilder_.Append("toCoordX=").Append(System.Uri.EscapeDataString(ConvertToString(toCoordX, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            }
            if (toCoordY != null)
            {
                urlBuilder_.Append("toCoordY=").Append(System.Uri.EscapeDataString(ConvertToString(toCoordY, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            }
            if (srs != null)
            {
                urlBuilder_.Append("srs=").Append(System.Uri.EscapeDataString(ConvertToString(srs, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            }
            if (networkType != null)
            {
                urlBuilder_.Append("networkType=").Append(System.Uri.EscapeDataString(ConvertToString(networkType, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            }
            if (calcMode != null)
            {
                urlBuilder_.Append("calcMode=").Append(System.Uri.EscapeDataString(ConvertToString(calcMode, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            }
            if (calcSize != null)
            {
                urlBuilder_.Append("calcSize=").Append(System.Uri.EscapeDataString(ConvertToString(calcSize, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            }
            if (outputFormat != null)
            {
                urlBuilder_.Append("outputFormat=").Append(System.Uri.EscapeDataString(ConvertToString(outputFormat, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            }
            if (callback != null)
            {
                urlBuilder_.Append("callback=").Append(System.Uri.EscapeDataString(ConvertToString(callback, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            }
            if (angle != null)
            {
                urlBuilder_.Append("angle=").Append(System.Uri.EscapeDataString(ConvertToString(angle, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            }
            if (steps != null)
            {
                urlBuilder_.Append("steps=").Append(System.Uri.EscapeDataString(ConvertToString(steps, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            }
            if (servicekey != null)
            {
                urlBuilder_.Append("servicekey=").Append(System.Uri.EscapeDataString(ConvertToString(servicekey, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            }
            urlBuilder_.Length--;

            var client_ = new System.Net.Http.HttpClient();
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    request_.Method = new System.Net.Http.HttpMethod("GET");
                    request_.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            var result_ = default(object);
                            try
                            {
                                result_ = Newtonsoft.Json.JsonConvert.DeserializeObject<object>(responseData_, _settings.Value);
                                return result_;
                            }
                            catch (System.Exception exception_)
                            {
                                throw new SwaggerException("Could not deserialize the response body.", (int)response_.StatusCode, responseData_, headers_, exception_);
                            }
                        }
                        else
                        if (status_ == "403")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new SwaggerException("No credits on given account (<code>servicekey</code> parameter) or not logged in.", (int)response_.StatusCode, responseData_, headers_, null);
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new SwaggerException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }

                        return default(object);
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (client_ != null)
                    client_.Dispose();
            }
        }

        private string ConvertToString(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value is System.Enum)
            {
                string name = System.Enum.GetName(value.GetType(), value);
                if (name != null)
                {
                    var field = System.Reflection.IntrospectionExtensions.GetTypeInfo(value.GetType()).GetDeclaredField(name);
                    if (field != null)
                    {
                        var attribute = System.Reflection.CustomAttributeExtensions.GetCustomAttribute(field, typeof(System.Runtime.Serialization.EnumMemberAttribute))
                            as System.Runtime.Serialization.EnumMemberAttribute;
                        if (attribute != null)
                        {
                            return attribute.Value;
                        }
                    }
                }
            }
            else if (value is byte[])
            {
                return System.Convert.ToBase64String((byte[])value);
            }
            else if (value != null && value.GetType().IsArray)
            {
                var array = System.Linq.Enumerable.OfType<object>((System.Array)value);
                return string.Join(",", System.Linq.Enumerable.Select(array, o => ConvertToString(o, cultureInfo)));
            }

            return System.Convert.ToString(value, cultureInfo);
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("NSwag", "11.20.1.0 (NJsonSchema v9.11.0.0 (Newtonsoft.Json v9.0.0.0))")]
    public partial class NetworkInfoClient
    {
        private string _baseUrl = "/routing";
        private System.Lazy<Newtonsoft.Json.JsonSerializerSettings> _settings;

        public NetworkInfoClient()
        {
            _settings = new System.Lazy<Newtonsoft.Json.JsonSerializerSettings>(() =>
            {
                var settings = new Newtonsoft.Json.JsonSerializerSettings();
                UpdateJsonSerializerSettings(settings);
                return settings;
            });
        }

        public string BaseUrl
        {
            get { return _baseUrl; }
            set { _baseUrl = value; }
        }

        protected Newtonsoft.Json.JsonSerializerSettings JsonSerializerSettings { get { return _settings.Value; } }

        partial void UpdateJsonSerializerSettings(Newtonsoft.Json.JsonSerializerSettings settings);
        partial void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, string url);
        partial void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, System.Text.StringBuilder urlBuilder);
        partial void ProcessResponse(System.Net.Http.HttpClient client, System.Net.Http.HttpResponseMessage response);

        /// <summary>Returns the available routing networks</summary>
        /// <param name="servicekey">Authorization key for the service. The value can be requested via <a href="mailto:helpdesk@geodan.nl">helpdesk@geodan.nl</a>.</param>
        /// <returns>OK</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<object> GetAsync(string servicekey)
        {
            return GetAsync(servicekey, System.Threading.CancellationToken.None);
        }

        /// <summary>Returns the available routing networks</summary>
        /// <param name="servicekey">Authorization key for the service. The value can be requested via <a href="mailto:helpdesk@geodan.nl">helpdesk@geodan.nl</a>.</param>
        /// <returns>OK</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public async System.Threading.Tasks.Task<object> GetAsync(string servicekey, System.Threading.CancellationToken cancellationToken)
        {
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/networkinfo?");
            if (servicekey != null)
            {
                urlBuilder_.Append("servicekey=").Append(System.Uri.EscapeDataString(ConvertToString(servicekey, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            }
            urlBuilder_.Length--;

            var client_ = new System.Net.Http.HttpClient();
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    request_.Method = new System.Net.Http.HttpMethod("GET");
                    request_.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            var result_ = default(object);
                            try
                            {
                                result_ = Newtonsoft.Json.JsonConvert.DeserializeObject<object>(responseData_, _settings.Value);
                                return result_;
                            }
                            catch (System.Exception exception_)
                            {
                                throw new SwaggerException("Could not deserialize the response body.", (int)response_.StatusCode, responseData_, headers_, exception_);
                            }
                        }
                        else
                        if (status_ == "403")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new SwaggerException("No credits on given account (<code>servicekey</code> parameter) or not logged in.", (int)response_.StatusCode, responseData_, headers_, null);
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new SwaggerException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }

                        return default(object);
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (client_ != null)
                    client_.Dispose();
            }
        }

        private string ConvertToString(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value is System.Enum)
            {
                string name = System.Enum.GetName(value.GetType(), value);
                if (name != null)
                {
                    var field = System.Reflection.IntrospectionExtensions.GetTypeInfo(value.GetType()).GetDeclaredField(name);
                    if (field != null)
                    {
                        var attribute = System.Reflection.CustomAttributeExtensions.GetCustomAttribute(field, typeof(System.Runtime.Serialization.EnumMemberAttribute))
                            as System.Runtime.Serialization.EnumMemberAttribute;
                        if (attribute != null)
                        {
                            return attribute.Value;
                        }
                    }
                }
            }
            else if (value is byte[])
            {
                return System.Convert.ToBase64String((byte[])value);
            }
            else if (value != null && value.GetType().IsArray)
            {
                var array = System.Linq.Enumerable.OfType<object>((System.Array)value);
                return string.Join(",", System.Linq.Enumerable.Select(array, o => ConvertToString(o, cultureInfo)));
            }

            return System.Convert.ToString(value, cultureInfo);
        }
    }



    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.11.0.0 (Newtonsoft.Json v9.0.0.0)")]
    public partial class RouteCoordinates : System.ComponentModel.INotifyPropertyChanged
    {
        private string _id;
        private double? _fromCoordX;
        private double? _fromCoordY;
        private double? _toCoordX;
        private double? _toCoordY;

        [Newtonsoft.Json.JsonProperty("Id", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    RaisePropertyChanged();
                }
            }
        }

        [Newtonsoft.Json.JsonProperty("FromCoordX", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public double? FromCoordX
        {
            get { return _fromCoordX; }
            set
            {
                if (_fromCoordX != value)
                {
                    _fromCoordX = value;
                    RaisePropertyChanged();
                }
            }
        }

        [Newtonsoft.Json.JsonProperty("FromCoordY", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public double? FromCoordY
        {
            get { return _fromCoordY; }
            set
            {
                if (_fromCoordY != value)
                {
                    _fromCoordY = value;
                    RaisePropertyChanged();
                }
            }
        }

        [Newtonsoft.Json.JsonProperty("ToCoordX", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public double? ToCoordX
        {
            get { return _toCoordX; }
            set
            {
                if (_toCoordX != value)
                {
                    _toCoordX = value;
                    RaisePropertyChanged();
                }
            }
        }

        [Newtonsoft.Json.JsonProperty("ToCoordY", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public double? ToCoordY
        {
            get { return _toCoordY; }
            set
            {
                if (_toCoordY != value)
                {
                    _toCoordY = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public static RouteCoordinates FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<RouteCoordinates>(data);
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.11.0.0 (Newtonsoft.Json v9.0.0.0)")]
    public partial class RouteStop : System.ComponentModel.INotifyPropertyChanged
    {
        private int? _id;
        private double? _coordX;
        private double? _coordY;

        [Newtonsoft.Json.JsonProperty("Id", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    RaisePropertyChanged();
                }
            }
        }

        [Newtonsoft.Json.JsonProperty("CoordX", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public double? CoordX
        {
            get { return _coordX; }
            set
            {
                if (_coordX != value)
                {
                    _coordX = value;
                    RaisePropertyChanged();
                }
            }
        }

        [Newtonsoft.Json.JsonProperty("CoordY", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public double? CoordY
        {
            get { return _coordY; }
            set
            {
                if (_coordY != value)
                {
                    _coordY = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public static RouteStop FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<RouteStop>(data);
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }

    }

    /// <summary>Defines the network on which the route calculation takes place</summary>
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.11.0.0 (Newtonsoft.Json v9.0.0.0)")]
    public enum NetworkType
    {
        [System.Runtime.Serialization.EnumMember(Value = "auto")]
        Auto = 0,

        [System.Runtime.Serialization.EnumMember(Value = "gsps_nl")]
        Gsps_nl = 1,

        [System.Runtime.Serialization.EnumMember(Value = "vrachtwagen")]
        Vrachtwagen = 2,

        [System.Runtime.Serialization.EnumMember(Value = "fiets_18kmu")]
        Fiets_18kmu = 3,

        [System.Runtime.Serialization.EnumMember(Value = "fiets_25kmu")]
        Fiets_25kmu = 4,

    }

    /// <summary>Defines how the route is calculated.<br>Supported values:<ul><li><code>time</code>: fastest route</li><li><code>distance</code>: shortest route</li><li><code>cost</code>: cheapest route, based on a price per hour and a price per kilometer</ul></summary>
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.11.0.0 (Newtonsoft.Json v9.0.0.0)")]
    public enum CalcMode
    {
        [System.Runtime.Serialization.EnumMember(Value = "time")]
        Time = 0,

        [System.Runtime.Serialization.EnumMember(Value = "distance")]
        Distance = 1,

        [System.Runtime.Serialization.EnumMember(Value = "cost")]
        Cost = 2,

    }

    /// <summary>Defines which information of the calculated route is returned.<br>Supported values are:<ul><li><code>coords</code>: returns all the coordinates of the route and the total travel time and total distance</li><li><code>timedistance</code>: returns only the total travel time and total distance, along with the route made of only start and end points</li></ul>The units of the output are defined by the parameter <code>format</code>.</summary>
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.11.0.0 (Newtonsoft.Json v9.0.0.0)")]
    public enum ReturnType
    {
        [System.Runtime.Serialization.EnumMember(Value = "coords")]
        Coords = 0,

        [System.Runtime.Serialization.EnumMember(Value = "timedistance")]
        Timedistance = 1,

    }

    /// <summary>Defines the units in which the calculated travel time and total distance are printed.<br>Supported values:<ul><li><code>sec-m</code>: seconds, meters</li><li><code>sec-km</code>: seconds, kilometers</li><li><code>min-m</code>: minutes, meters</li><li><code>min-km</code>: minutes, kilometers</li></ul></summary>
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.11.0.0 (Newtonsoft.Json v9.0.0.0)")]
    public enum Format
    {
        [System.Runtime.Serialization.EnumMember(Value = "sec-m")]
        SecM = 0,

        [System.Runtime.Serialization.EnumMember(Value = "sec-km")]
        SecKm = 1,

        [System.Runtime.Serialization.EnumMember(Value = "min-m")]
        MinM = 2,

        [System.Runtime.Serialization.EnumMember(Value = "min-km")]
        MinKm = 3,

    }

    /// <summary>Defines the type of format of the results.<br>Supported values:<ul><li><code>xml</code>: XML format.<br><code>Content-type</code>: <code>application/xml</code></li><li><code>geojson</code>: GeoJSON format.<br><code>Content-type</code>: <code>application/json</code> or in combination with the callback parameter: <code>application/javascript</code> (JSON-P)</li><li><code>json</code>: JSON format, identical to <code>geojson</code>.</li></ul></summary>
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.11.0.0 (Newtonsoft.Json v9.0.0.0)")]
    public enum OutputFormat
    {
        [System.Runtime.Serialization.EnumMember(Value = "xml")]
        Xml = 0,

        [System.Runtime.Serialization.EnumMember(Value = "geojson")]
        Geojson = 1,

        [System.Runtime.Serialization.EnumMember(Value = "json")]
        Json = 2,

    }

    /// <summary>Defines the network on which the route calculation takes place</summary>
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.11.0.0 (Newtonsoft.Json v9.0.0.0)")]
    public enum NetworkType2
    {
        [System.Runtime.Serialization.EnumMember(Value = "auto")]
        Auto = 0,

        [System.Runtime.Serialization.EnumMember(Value = "gsps_nl")]
        Gsps_nl = 1,

        [System.Runtime.Serialization.EnumMember(Value = "vrachtwagen")]
        Vrachtwagen = 2,

        [System.Runtime.Serialization.EnumMember(Value = "fiets_18kmu")]
        Fiets_18kmu = 3,

        [System.Runtime.Serialization.EnumMember(Value = "fiets_25kmu")]
        Fiets_25kmu = 4,

    }

    /// <summary>Defines how the route is calculated.<br>Supported values:<ul><li><code>time</code>: fastest route</li><li><code>distance</code>: shortest route</li><li><code>cost</code>: cheapest route, based on a price per hour and a price per kilometer</ul></summary>
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.11.0.0 (Newtonsoft.Json v9.0.0.0)")]
    public enum CalcMode2
    {
        [System.Runtime.Serialization.EnumMember(Value = "time")]
        Time = 0,

        [System.Runtime.Serialization.EnumMember(Value = "distance")]
        Distance = 1,

        [System.Runtime.Serialization.EnumMember(Value = "cost")]
        Cost = 2,

    }

    /// <summary>Defines which information of the calculated route is returned.<br>Supported values are:<ul><li><code>coords</code>: returns all the coordinates of the route and the total travel time and total distance</li><li><code>timedistance</code>: returns only the total travel time and total distance, along with a route with coordinates of start and end point</li></ul></summary>
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.11.0.0 (Newtonsoft.Json v9.0.0.0)")]
    public enum ReturnType2
    {
        [System.Runtime.Serialization.EnumMember(Value = "coords")]
        Coords = 0,

        [System.Runtime.Serialization.EnumMember(Value = "timedistance")]
        Timedistance = 1,

    }

    /// <summary>Defines the units in which the calculated travel time and total distance are printed.<br>Supported values:<ul><li><code>sec-m</code>: seconds, meters</li><li><code>sec-km</code>: seconds, kilometers</li><li><code>min-m</code>: minutes, meters</li><li><code>min-km</code>: minutes, kilometers</li></ul></summary>
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.11.0.0 (Newtonsoft.Json v9.0.0.0)")]
    public enum Format2
    {
        [System.Runtime.Serialization.EnumMember(Value = "sec-m")]
        SecM = 0,

        [System.Runtime.Serialization.EnumMember(Value = "sec-km")]
        SecKm = 1,

        [System.Runtime.Serialization.EnumMember(Value = "min-m")]
        MinM = 2,

        [System.Runtime.Serialization.EnumMember(Value = "min-km")]
        MinKm = 3,

    }

    /// <summary>Defines the type of format of the results.<br>Supported values:<ul><li><code>xml</code>: XML format, compatible with the old service on geoserver.nl/routeerservice.<br><code>Content-type</code>: <code>application/xml</code></li><li><code>geojson</code>: GeoJSON format.<br><code>Content-type</code>: <code>application/json</code> or in combination with the callback parameter: <code>application/javascript</code> (JSON-P)</li><li><code>json</code>: JSON format, identical to <code>geojson</code>.</li></ul></summary>
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.11.0.0 (Newtonsoft.Json v9.0.0.0)")]
    public enum OutputFormat2
    {
        [System.Runtime.Serialization.EnumMember(Value = "xml")]
        Xml = 0,

        [System.Runtime.Serialization.EnumMember(Value = "geojson")]
        Geojson = 1,

        [System.Runtime.Serialization.EnumMember(Value = "json")]
        Json = 2,

    }

    /// <summary>Supported values:<ul><li><code>Open</code>: can start and end on any given location</li><li><code>OpenEnd</code>: first given location is the starting point and the route can end on any other location</li><li><code>OpenStart</code>: last given location is ending point, the route can start on any other location</li><li><code>StartEnd</code>: first given location is the starting point and last given location is ending point</li><li><code>Round</code>: first given location is the starting point and the ending point</li></ul></summary>
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.11.0.0 (Newtonsoft.Json v9.0.0.0)")]
    public enum TspMode
    {
        [System.Runtime.Serialization.EnumMember(Value = "Open")]
        Open = 0,

        [System.Runtime.Serialization.EnumMember(Value = "OpenEnd")]
        OpenEnd = 1,

        [System.Runtime.Serialization.EnumMember(Value = "OpenStart")]
        OpenStart = 2,

        [System.Runtime.Serialization.EnumMember(Value = "StartEnd")]
        StartEnd = 3,

        [System.Runtime.Serialization.EnumMember(Value = "Round")]
        Round = 4,

    }

    /// <summary>Defines the network on which the route calculation takes place</summary>
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.11.0.0 (Newtonsoft.Json v9.0.0.0)")]
    public enum NetworkType3
    {
        [System.Runtime.Serialization.EnumMember(Value = "auto")]
        Auto = 0,

        [System.Runtime.Serialization.EnumMember(Value = "gsps_nl")]
        Gsps_nl = 1,

        [System.Runtime.Serialization.EnumMember(Value = "vrachtwagen")]
        Vrachtwagen = 2,

        [System.Runtime.Serialization.EnumMember(Value = "fiets_18kmu")]
        Fiets_18kmu = 3,

        [System.Runtime.Serialization.EnumMember(Value = "fiets_25kmu")]
        Fiets_25kmu = 4,

    }

    /// <summary>Defines how the route is calculated.<br>Supported values:<ul><li><code>time</code>: fastest route</li><li><code>distance</code>: shortest route</li><li><code>cost</code>: cheapest route, based on a price per hour and a price per kilometer</ul></summary>
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.11.0.0 (Newtonsoft.Json v9.0.0.0)")]
    public enum CalcMode3
    {
        [System.Runtime.Serialization.EnumMember(Value = "time")]
        Time = 0,

        [System.Runtime.Serialization.EnumMember(Value = "distance")]
        Distance = 1,

        [System.Runtime.Serialization.EnumMember(Value = "cost")]
        Cost = 2,

    }

    /// <summary>Defines the network on which the route calculation takes place</summary>
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.11.0.0 (Newtonsoft.Json v9.0.0.0)")]
    public enum NetworkType4
    {
        [System.Runtime.Serialization.EnumMember(Value = "auto")]
        Auto = 0,

        [System.Runtime.Serialization.EnumMember(Value = "gsps_nl")]
        Gsps_nl = 1,

        [System.Runtime.Serialization.EnumMember(Value = "vrachtwagen")]
        Vrachtwagen = 2,

        [System.Runtime.Serialization.EnumMember(Value = "fiets_18kmu")]
        Fiets_18kmu = 3,

        [System.Runtime.Serialization.EnumMember(Value = "fiets_25kmu")]
        Fiets_25kmu = 4,

    }

    /// <summary>Defines how the route is calculated.<br>Supported values:<ul><li><code>time</code>: fastest route</li><li><code>distance</code>: shortest route</li><li><code>cost</code>: cheapest route, based on a price per hour and a price per kilometer</ul></summary>
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.11.0.0 (Newtonsoft.Json v9.0.0.0)")]
    public enum CalcMode4
    {
        [System.Runtime.Serialization.EnumMember(Value = "time")]
        Time = 0,

        [System.Runtime.Serialization.EnumMember(Value = "distance")]
        Distance = 1,

        [System.Runtime.Serialization.EnumMember(Value = "cost")]
        Cost = 2,

    }

    /// <summary>Defines the type of format of the results.<br>Supported values:<ul><li><code>xml</code>: XML format, compatible with the old service on geoserver.nl/routeerservice.<br><code>Content-type</code>: <code>application/xml</code></li><li><code>geojson</code>: GeoJSON format.<br><code>Content-type</code>: <code>application/json</code> or in combination with the callback parameter: <code>application/javascript</code> (JSON-P)</li><li><code>json</code>: JSON format, identical to <code>geojson</code>.</li></ul></summary>
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.11.0.0 (Newtonsoft.Json v9.0.0.0)")]
    public enum OutputFormat3
    {
        [System.Runtime.Serialization.EnumMember(Value = "xml")]
        Xml = 0,

        [System.Runtime.Serialization.EnumMember(Value = "geojson")]
        Geojson = 1,

        [System.Runtime.Serialization.EnumMember(Value = "json")]
        Json = 2,

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.11.0.0 (Newtonsoft.Json v9.0.0.0)")]
    public partial class Response : System.ComponentModel.INotifyPropertyChanged
    {
        private string _error;
        private string _input_address;

        [Newtonsoft.Json.JsonProperty("error", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Error
        {
            get { return _error; }
            set
            {
                if (_error != value)
                {
                    _error = value;
                    RaisePropertyChanged();
                }
            }
        }

        [Newtonsoft.Json.JsonProperty("input_address", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Input_address
        {
            get { return _input_address; }
            set
            {
                if (_input_address != value)
                {
                    _input_address = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public static Response FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Response>(data);
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }

    }

    [System.CodeDom.Compiler.GeneratedCode("NSwag", "11.20.1.0 (NJsonSchema v9.11.0.0 (Newtonsoft.Json v9.0.0.0))")]
    public partial class SwaggerException : System.Exception
    {
        public int StatusCode { get; private set; }

        public string Response { get; private set; }

        public System.Collections.Generic.Dictionary<string, System.Collections.Generic.IEnumerable<string>> Headers { get; private set; }

        public SwaggerException(string message, int statusCode, string response, System.Collections.Generic.Dictionary<string, System.Collections.Generic.IEnumerable<string>> headers, System.Exception innerException)
            : base(message + "\n\nStatus: " + statusCode + "\nResponse: \n" + response.Substring(0, response.Length >= 512 ? 512 : response.Length), innerException)
        {
            StatusCode = statusCode;
            Response = response;
            Headers = headers;
        }

        public override string ToString()
        {
            return string.Format("HTTP Response: \n\n{0}\n\n{1}", Response, base.ToString());
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("NSwag", "11.20.1.0 (NJsonSchema v9.11.0.0 (Newtonsoft.Json v9.0.0.0))")]
    public partial class SwaggerException<TResult> : SwaggerException
    {
        public TResult Result { get; private set; }

        public SwaggerException(string message, int statusCode, string response, System.Collections.Generic.Dictionary<string, System.Collections.Generic.IEnumerable<string>> headers, TResult result, System.Exception innerException)
            : base(message, statusCode, response, headers, innerException)
        {
            Result = result;
        }
    }
}