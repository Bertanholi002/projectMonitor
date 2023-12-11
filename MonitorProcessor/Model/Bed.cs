using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorProcessor.Model
{
    public class Bed
    {   
        public int id { get; set; }
        public string? status { get; set; }
        public string? name { get; set; }
        public bool? is_active { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public int? created_by { get; set; }
        //public Department? department_obj { get; set; }
    }
}
