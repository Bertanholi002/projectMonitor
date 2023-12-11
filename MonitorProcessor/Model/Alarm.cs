using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorProcessor.Model
{
    public class Alarm
    {
        public string name { get; set; }
        public string parameter { get; set; }
        public int min { get; set; }
        public int max { get; set; }
        public int alarm_priority { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set;}
        public int monitor_id { get; set; }
    }
}
