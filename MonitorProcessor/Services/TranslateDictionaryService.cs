using MonitorProcessor.Model;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorProcessor.Services
{
    public class TranslateDictionaryService
    {
        public TranslateDictionaryService() { }

        public List<TranslateDictionary> getAllDictionary(string APIServer, string APIServerPort, string ServerAPICloud, int ServerType, Login login)
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
            RestRequest request = new RestRequest("/core/list-dictionary/", Method.Get);
            string tokenString = "token " + login.token;
            request.AddHeader("Authorization", tokenString);
            var response = client.Execute(request);
            var dictionary = JsonConvert.DeserializeObject<List<TranslateDictionary>>(response.Content);
            return dictionary;
        }
    }
}
