using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MonitorProcessor.MindrayTreatment;

public class TestingWave
{
    public string Time { get; set; }
    public string Value { get; set; }

    static Dictionary<string, List<TestingWave>> wavesMid = new()
    {
        { "MDC_ECG_ELEC_POTL_I", new List<TestingWave>() },
        { "MDC_ECG_ELEC_POTL_II", new List<TestingWave>() },
        { "MDC_ECG_ELEC_POTL_III", new List<TestingWave>() }
        
    };

    static string palavraFora;
    static int contador = 0;
    static bool Acess = false;
    string palavra;

    List<string> nameWave = new() { "MDC_ECG_ELEC_POTL_I", "MDC_ECG_ELEC_POTL_II", "MDC_ECG_ELEC_POTL_III" };

    public void ReadWaveMindray(string data)
    {
        using(StringReader sr = new StringReader(data))
        {
            palavra = sr.ReadToEnd();
        }

        palavra = palavra.Replace('-', ' ');
        palavra = palavra.Replace('|', ' ');
        palavra = palavra.Replace('^', ' ');
        string[]palavras = palavra.Split(new char[] {' ', '\t'}, StringSplitOptions.RemoveEmptyEntries);

        foreach(string str in palavras)
        {
            if (!string.IsNullOrEmpty(str))
            {
                if (Acess == true && contador < 360)
                {
                    DateTime date = DateTime.Now;
                    TestingWave wave = new();
                    wave.Time = date.ToString("dd-MM-yyyy HH:mm:ss.ffff");
                    wave.Value = str;
                    
                    wavesMid[palavraFora].Add(wave);
                    contador++;
                    if (contador == 360)
                    {
                        contador = 0;
                        Acess = false;


                    }
                }
                if (nameWave.Contains(str))
                {
                    Acess = true;
                    palavraFora = str;
                    if (!wavesMid.ContainsKey(str))
                    {
                        wavesMid[palavraFora] = new();
                    }
                }
            }
            if (wavesMid["MDC_ECG_ELEC_POTL_III"].Count >= 720)
            {
                foreach (var dict in wavesMid.Values)
                {
                    if (dict.Count > 360)
                    {
                        dict.RemoveAt(0);
                        dict.RemoveAt(0);
                        dict.RemoveAt(358);
                        dict.RemoveAt(358);
                    }
                    else
                    {
                        dict.RemoveAt(0);
                        dict.RemoveAt(0);
                    }
                }
                JsonWaveApi(wavesMid);
                foreach(var i in wavesMid.Values)
                {
                    i.Clear();
                }
            }
        }
    }
    public static void JsonWaveApi(Dictionary<string, List<TestingWave>> data)
    {
        string url = "https://api.skopien.com.br/core/websocket-unique-channel/";
        string token = "7c73402079ef7964eda17bf0bd0b367cc057d789";
        
        string convert = JsonConvert.SerializeObject(data, Formatting.Indented);
        using (HttpClient client = new())
        {
            client.DefaultRequestHeaders.Add("Authorization", $"token {token}");
            var content = new StringContent(convert, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;
        }
        
       
    }



}
