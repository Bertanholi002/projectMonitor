using MonitorProcessor.Model;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorProcessor.Services
{
    public class BedService
    {
        public BedService() { }
        public List<Bed> getAllBeds(string APIServer, string APIServerPort, string ServerAPICloud, int ServerType, Login login)
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
            List<Bed> beds = new List<Bed>();
            RestClient client = new RestClient(url);
            RestRequest request = new RestRequest("/core/list-bed/?page_size=100", Method.Get);
            string tokenString = "token " + login.token;
            request.AddHeader("Authorization", tokenString);
            var response = client.Execute(request);
            var result = JsonConvert.DeserializeObject<JToken>(response.Content);
            var bedResults = result["results"];
            foreach(var bed in bedResults)
            {
                Bed bedAux = new Bed();
                bedAux.id = bed.Value<int>("id");
                bedAux.status = bed.Value<string?>("status");
                bedAux.name = bed.Value<string?>("name");
                bedAux.is_active = bed.Value<bool?>("is_active");
                bedAux.created_at = bed.Value<DateTime?>("created_at");
                bedAux.updated_at = bed.Value<DateTime?>("updated_at");
                bedAux.created_by = bed.Value<int?>("created_by");
                beds.Add(bedAux);
            }

            return beds;
        }
    }
}
