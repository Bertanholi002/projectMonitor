using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorProcessor.Model
{
    public class ScannerMonitorsAdapter
    {
        public int id { get; set; }
        public string ip { get; set; }
        public bool? is_active { get; set; }
        public string? status { get; set; }
    }
}
