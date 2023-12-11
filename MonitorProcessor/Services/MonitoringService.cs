using MonitorProcessor.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorProcessor.Services
{
    public class MonitoringService
    {
        public static int OK = 1;
        public static int NOK = 0;
        public MonitoringService() { }
        public int PostMonitoring(string APIServer, string APIServerPort, string ServerAPICloud, int ServerType, Login login, MonitoringAdapter monitoring)
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
                RestClient client = new RestClient(url);
                RestRequest request = new RestRequest("/core/monitoring/", Method.Post);
                var json = JsonConvert.SerializeObject(monitoring, Formatting.Indented, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
                request.RequestFormat = DataFormat.Json;
                request.AddBody(json);
                string tokenString = "token " + login.token;
                request.AddHeader("Authorization", tokenString);
                var response = client.Execute(request);
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
