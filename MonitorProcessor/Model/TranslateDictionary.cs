using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorProcessor.Model
{
    public class TranslateDictionary
    {
        public int id { get; set; }
        public string parameter_from { get; set; }
        public EquipmentParametersDefault? parameter_to { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public int  model { get; set; }

    }
}
