using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using MonitorProcessor.Model;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MonitorProcessor.Services
{
    public class MonitorsService
    {
        public static int OK = 1;
        public static int NOK = 0;
        public MonitorsService() { }

        public List<Monitors> getAllMonitors(string APIServer, string APIServerPort, string ServerAPICloud, int ServerType, Login login)
        {
            string url = " ";
            if (ServerType == 1)
            {
                url = $"http://{APIServer}:{APIServerPort}";
            }
            else
            {
                url = $"https://{ServerAPICloud}";
            }
            List<Monitors> monitorsResult = new List<Monitors>();
            RestClient client = new RestClient(url);
            RestRequest request = new RestRequest("/core/list-equipment/?page_size=100", Method.Get);
            string tokenString = "token " + login.token;
            request.AddHeader("Authorization", tokenString);
            var response = client.Execute(request);
            var result = JsonConvert.DeserializeObject<JToken>(response.Content);
            var monitors = result["results"];
            foreach(var monitor in monitors)
            {
                Monitors auxMonitor = new Monitors();
                auxMonitor.id = monitor.Value<int>("id");
                auxMonitor.mac_address = monitor.Value<string>("mac_address");
                auxMonitor.ip = monitor.Value<string>("ip");
                auxMonitor.port = monitor.Value<string>("port");
                auxMonitor.is_active = monitor.Value<bool?>("is_active");
                auxMonitor.created_at = monitor.Value<DateTime?>("created_at");
                auxMonitor.updated_at = monitor.Value<DateTime>("updated_at");
                auxMonitor.type_request = monitor.Value<int?>("type_request");
                auxMonitor.brand = monitor.Value<int?>("brand");
                auxMonitor.model = monitor.Value<int?>("model");
                auxMonitor.bed = monitor.Value<int?>("bed");
                auxMonitor.protocol = monitor.Value<int?>("protocol");
                auxMonitor.department = monitor.Value<int?>("department");
                auxMonitor.created_by = monitor.Value<int?>("created_by");
                auxMonitor.status = monitor.Value<string?>("status");
                auxMonitor.brand_obj =  monitor["brand_obj"].ToObject<Brand>();
                auxMonitor.model_obj = monitor["model_obj"].ToObject<ModelMonitor>();
                //auxMonitor.protocol_obj = monitor["protocol_obj"].ToObject<Protocol>();
                auxMonitor.department_obj = monitor["department_obj"].ToObject<Department>();
                monitorsResult.Add(auxMonitor);
                
               
            }
            return monitorsResult;
        }

        public async Task<int> UpdateMonitor(string APIServer, string APIServerPort, string ServerAPICloud, int ServerType, Login login, Monitors monitor)
        {
            try
            {
                string url = " ";
                if (ServerType == 1)
                {
                    url = $"http://{APIServer}:{APIServerPort}";
                }
                else
                {
                    url = $"https://{ServerAPICloud}";
                }
                ScannerMonitorsAdapter adapter = new ScannerMonitorsAdapter();
                adapter.id = monitor.id;
                adapter.ip = monitor.ip;
                adapter.is_active = monitor.is_active;
                adapter.status = monitor.status;
                RestClient client = new RestClient(url);
                RestRequest request = new RestRequest("/core/update-equipment/", Method.Patch);
                var json = JsonConvert.SerializeObject(adapter, Formatting.Indented, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
                request.RequestFormat = DataFormat.Json;
                request.AddBody(json);
                string tokenString = "token " + login.token;
                request.AddHeader("Authorization", tokenString);
                var response = await client.ExecuteAsync(request).ConfigureAwait(false);
                var obj = JObject.Parse(response.Content);
                var statusCode = (int)response.StatusCode;
                if (statusCode == 200)
                {
                    return OK;
                }
                else
                {
                    return NOK;
                }
            }
            catch(Exception ex)
            {
                return NOK;
            }
            
        }
    }
}
