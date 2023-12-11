using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
//using MessageRouter.Model;
using System.Text.Json;


namespace MonitorProcessor.Model
{
    public  class Monitors
    {   
        public int id {  get; set; }
        public string mac_address { get; set; }
        public string ip { get; set; }
        public bool? is_active { get; set; }
        public string port { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public int? created_by { get; set; }
        public int? type_request { get; set; }
        public int? brand { get; set; }
        public int? department { get; set; }
        public int? model { get; set; }
        public int? protocol { get; set; }
        public int? bed { get; set; }
        public TypeRequest? type_request_obj { get; set; }
        public Brand? brand_obj { get; set; }
        public ModelMonitor? model_obj { get; set; }
        public Protocol? protocol_obj { get; set; }
        public Department? department_obj { get; set; }
        public string? monitorMessage { get; set; }
        public string? results { get; set; }
        public string? status { get; set; }
        public Monitoring? monitoring { get; set; }
        //public List<ObservationParameters>? observationParameters { get; set; }

        

    }
}
