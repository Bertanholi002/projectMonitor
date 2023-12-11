using MonitorProcessor.Model;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorProcessor.Services
{
    public class PatientService
    {
        public PatientService() { }

        public List<Patient> getAllPatients(string APIServer, string APIServerPort, string ServerAPICloud, int ServerType, Login login)
        {
            string url = " ";
            if (ServerType == 1)
            {
                url = $"http://{APIServer}:{APIServerPort}";
            }
            else
            {
                url = $"https://{ServerAPICloud}";
            }
            List<Patient> patients = new List<Patient>();
            RestClient client = new RestClient(url);
            RestRequest request = new RestRequest("/core/list-patient/?page_size=100", Method.Get);
            string tokenString = "token " + login.token;
            request.AddHeader("Authorization", tokenString);
            var response = client.Execute(request);
            var result = JsonConvert.DeserializeObject<JToken>(response.Content);
            var patientsAux = result["results"];
            foreach(var patient in patientsAux)
            {
                Patient patientAux = new Patient();
                patientAux.id = patient.Value<int>("id");
                patientAux.gender = patient.Value<string?>("gender");
                patientAux.type = patient.Value<string?>("type");
                patientAux.name = patient.Value<string?>("name");
                patientAux.doctor_name = patient.Value<string?>("doctor_name");
                patientAux.cpf = patient.Value<string?>("cpf");
                patientAux.weight = patient.Value<double?>("weight");
                patientAux.height = patient.Value<double?>("height");
                patientAux.blood_type = patient.Value<string?>("blood_type");
                patientAux.birth_date = patient.Value<DateTime?>("birth_date");
                patientAux.admission_date = patient.Value<DateTime?>("admission_date");
                patientAux.medical_release = patient.Value<DateTime?>("medical_release");
                patientAux.is_active = patient.Value<bool?>("is_active");
                patientAux.created_at = patient.Value<DateTime?>("created_at");
                patientAux.updated_at = patient.Value<DateTime?>("updated_at");
                patientAux.created_by = patient.Value<int?>("created_by");
                //patientAux.bed_obj = patient.Value<Bed?>("bed_obj");
                patients.Add(patientAux);

            }


            return patients;
        }

    }
}
