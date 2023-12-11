using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorProcessor.Model
{
    public class Patient
    {   
        public int id { get; set; }
        public string? gender {  get; set; }
        public string? type { get; set; }
        public string? name { get; set; }
        public string? doctor_name { get; set; }
        public string? cpf { get; set; }
        public double? weight { get; set; }
        public double? height { get; set; }
        public string? blood_type { get; set; }
        public DateTime? birth_date { get; set; }
        public DateTime? admission_date { get; set; }
        public DateTime? medical_release { get; set; }
        public bool? is_active { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get;set; }
        public int? created_by { get; set; }
        //public Bed? bed_obj { get; set; }
    }
}
