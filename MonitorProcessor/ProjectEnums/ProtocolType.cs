using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorProcessor.ProjectEnums
{
    public enum ProtocolType
    {
        [Description("HL7 Pipe")]
        Pipe = 1,
        [Description("HL7 XML")]
        XML = 2
    }
}
