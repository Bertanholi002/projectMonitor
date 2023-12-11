using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorProcessor.Model
{
    public class MonitorsContentRequest
    {
        public int count {  get; set; }
        public string? next { get; set; }
        public string? previous { get; set; }
        public List<Monitors>? results { get; set; }
    }
}
