using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MonitorProcessor.PhillipsMPTreatment.DataStructure;

namespace MonitorProcessor.PhillipsMPTreatment
{
    public class WaveValResult
    {
        public string Timestamp;
        public string Relativetimestamp;
        public string SystemLocalTime;
        public string PhysioID;
        public byte[] Value;
        public string DeviceID;
        public ushort obpoll_handle;
        public SaSpec saSpecData = new SaSpec();
        public SaCalibData16 saCalibData = new SaCalibData16();
        public ScaleRangeSpec16 ScaleRangeSpec16 = new ScaleRangeSpec16();
    }
}
