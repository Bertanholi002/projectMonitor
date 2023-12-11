using MonitorProcessor.Model;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace MonitorProcessor.Services
{
    public  class LoginService
    {
        public LoginService() { }

        public Login? getAuth(string APIServer, string APIServerPort, string ServerAPICloud, int ServerType, string username, string password)
        {
            Login? IntegrationServerLogin = new Login();
            string url = " ";
            if(ServerType == 1)
            {
                url = $"http://{APIServer}:{APIServerPort}";
            }
            else
            {
                url = $"https://{ServerAPICloud}";
            }
            RestClient client = new RestClient(url);
            RestRequest request = new RestRequest("/core/auth/", Method.Post);
            request.AlwaysMultipartFormData = true;
            request.AddParameter("username", username);
            request.AddParameter("password", password);
            var response = client.Execute(request);
            IntegrationServerLogin = JsonSerializer.Deserialize<Login>(response.Content);
            return IntegrationServerLogin;
            

        }
    }
}
