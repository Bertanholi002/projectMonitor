using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace MainApp.Configuration
{
    public class ResourceConfiguration
    {
        public ResourceConfiguration()
        {

        }
        public string getAPIServer(IConfiguration configuration)
        {
            return configuration.GetSection("APIServer").Value;
        }
        public string getAPIServerPort(IConfiguration configuration)
        {
            return configuration.GetSection("APIServerPort").Value;
        }
        public int getServerType(IConfiguration configuration)
        {
            return Convert.ToInt16(configuration.GetSection("ServerType").Value);
        }
        public string getAPIServerCloud(IConfiguration configuration)
        {
            return configuration.GetSection("ServerAPICloud").Value;
        }
        public string getUsernameAPI(IConfiguration configuration)
        {
            return configuration.GetSection("UsernameAPI").Value;
        }
        public string getPasswordAPI(IConfiguration configuration)
        {
            return configuration.GetSection("PasswordAPI").Value;
        }
        public double getMessagesServerMonitorsTime(IConfiguration configuration)
        {
            return Convert.ToDouble(configuration.GetSection("MessagesServerMonitorsTime").Value);
        }
        public double getMessagesClientMonitorsTime(IConfiguration configuration)
        {
            return Convert.ToDouble(configuration.GetSection("MessagesClientMonitorsTime").Value);
        }
    }
}
