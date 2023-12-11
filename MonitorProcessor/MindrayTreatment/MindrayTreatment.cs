using MonitorProcessor.Model;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;

namespace WavesMindray;

public class MindrayParameters
{
    public string Parametro { get; set; }
    public string Valor { get; set; }

    static Dictionary<string, List<MonitoringAdapter>> dict = new();
    int numericNumberParameter = 1;

    MonitoringAdapter adapter = new();
    public void TranslatorParameters(string dados)
    {
        using (StringReader sr = new StringReader(dados))
        {
            string linha;
            while ((linha = sr.ReadLine()) != null)
            {
                linha = linha.Replace("|", " ");
                linha = linha.Replace("^", " ");

                string[] palavras = linha.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (palavras.Length > 0 && palavras[0] == "OBX")
                {
                    if (palavras.Length > 7)
                    {
                        string parametro = TranslateParameter(palavras[4]);
                        string valor = palavras[7];
                        AdapterParameter(parametro, valor);
                    }
                }
            }
        }
        JsonPostParameters(adapter);


    }
    public void AdapterParameter(string parametro, string valor)
    {
        Type type = adapter.GetType();
        PropertyInfo property = type.GetProperty(parametro);
        if (property != null)
        {
            if (double.TryParse(valor, out double result))
                property.SetValue(adapter, result);

        }
    }
    public static string TranslateParameter(string parameter)
    {
        switch (parameter)
        {
            case "MDC_ECG_CARD_BEAT_RATE":
                return "FC";

            case "MDC_PRESS_BLD_ART_ABP_SYS":
                return "PI_SIST";

            case "MDC_PRESS_BLD_ART_ABP_DIA":
                return "PI_DIAS";

            case "MDC_PRESS_BLD_ART_ABP_MEAN":
                return "PI_MED";

            case "MDC_PULS_OXIM_SAT_O2":
                return "SPO2";

            case "MDC_PLETH_PULS_RATE":
                return "SPO2_PR";

            case "MDC_TEMP":
                return "T1";

            case "MDC_CO2_RESP_RATE":
                return "FR";

            case "MDC_ECG_AMPL_ST_V5":
                return "ST_V5";

            case "MDC_ECG_AMPL_ST_V2":
                return "ST_V2";

            case "MDC_ECG_AMPL_ST_AVR":
                return "ST_AVR";

            case "MDC_ECG_AMPL_ST_AVL":
                return "ST_AVL";

            case "MDC_ECG_AMPL_ST_AVF":
                return "ST_AVF";
        }
        return parameter;
    }
    public void JsonPostParameters(MonitoringAdapter parameters)
    {
        adapter.equipment = 1;
        adapter.patient = "32";
        adapter.bed = "40";

        var jsonSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
        };
        string convert = JsonConvert.SerializeObject(parameters, Formatting.Indented, jsonSettings);
        SendParameters(convert);

    }

    public void SendParameters(string envio)
    {
        string url = "https://api.skopien.com.br/core/monitoring/";
        string token = "7c73402079ef7964eda17bf0bd0b367cc057d789";
        using (HttpClient client = new())
        {
            client.DefaultRequestHeaders.Add("Authorization", $"token {token}");
            var content = new StringContent(envio, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {

               //Console.WriteLine("OKPARAMETER");
            }
       
        }
    }
}
