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
    public class EquipmentDefautParametersService
    {
        public EquipmentDefautParametersService() { }

        public List<EquipmentParametersDefault>? getAllDefaultParameters(string APIServer, string APIServerPort, string ServerAPICloud, int ServerType, Login login)
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
            RestRequest request = new RestRequest("/core/list-params-equipment/", Method.Get);
            string tokenString = "token " + login.token;
            request.AddHeader("Authorization", tokenString);
            var response = client.Execute(request);
            var parametersDefault = JsonConvert.DeserializeObject<List<EquipmentParametersDefault>?>(response.Content);
            return parametersDefault;
            
        }
    }
}
