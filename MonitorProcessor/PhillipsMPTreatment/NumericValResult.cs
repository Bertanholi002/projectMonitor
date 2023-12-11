using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorProcessor.PhillipsMPTreatment
{
    public class NumericValResult
    {
        public string Timestamp { get; set; }
        public string Relativetimestamp { get; set; }
        public string SystemLocalTime { get; set; }
        public string PhysioID { get; set; }
        public string Value { get; set; }
        public string DeviceID { get; set; }
    }
}

