using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorProcessor.Model
{
    public class TypeRequest
    {
        public int id {  get; set; }
        public string name { get; set; }
        public int refresh_time { get; set; }
        public bool send_all_information { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}
