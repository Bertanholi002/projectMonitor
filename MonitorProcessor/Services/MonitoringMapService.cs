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


namespace MonitorProcessor.Services
{
    public class MonitoringMapService
    {
        public MonitoringMapService() { }

        public List<MonitoringMap> getAllMonitoringsMap(string APIServer, string APIServerPort, Login login)
        {
            string url = $"http://{APIServer}:{APIServerPort}/api";
            RestClient client = new RestClient(url);
            RestRequest request = new RestRequest("ListMonitoringsMap", Method.Get);
            List<MonitoringMap> response = client.Execute<List<MonitoringMap>>(request).Data;
            return response;
        }
    }
}
