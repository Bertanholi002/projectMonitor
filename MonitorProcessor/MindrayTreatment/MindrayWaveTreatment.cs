//using System.Text;
//using Newtonsoft.Json;

//namespace WavesMindray;

//public class MindrayTranslate
//{
//    static int conta = 0;
//    static bool aposChave = false;
//    static bool aposkey = false;
//    static string palavraFora = "";
//    static Dictionary<string, List<Mindray>> waveMindray = new();
//    public class Mindray
//    {
//        public string Time { get; set; }
//        public string Value { get; set; }
//    }
//    public void TranslatorMindray(string dados)
//    {
//        StringReader sr = new StringReader(dados);
//        string livro = sr.ReadToEnd();
//        sr.Close();
//        livro = livro.Replace('|', ' ');
//        livro = livro.Replace('^', ' ');
//        livro = livro.Replace('-', ' ');
//        string[] palavras = livro.Split(' ');


//        foreach (string palavra in palavras)
//        {
//            if (!string.IsNullOrEmpty(palavra))
//            {
//                Verify(palavra);
//            }
//        }
//        JsonCreated(waveMindray);
//    }

//    public static void Verify(string palavraChave)
//    {

//        List<string> hearT = new() { "MDC_ECG_ELEC_POTL_V2", "MDC_ECG_ELEC_POTL_I", "MDC_ECG_ELEC_POTL_II", "MDC_ECG_ELEC_POTL_III", "MDC_ECG_ELEC_POTL_AVR", "MDC_ECG_ELEC_POTL_AVL",
//        "MDC_ECG_ELEC_POTL_AVF", "MDC_ECG_ELEC_POTL_V1", "MDC_ECG_ELEC_POTL_V2", "MDC_ECG_ELEC_POTL_V3", "MDC_ECG_ELEC_POTL_V4", "MDC_ECG_ELEC_POTL_V5"};

//        List<string> breathing = new() { "MDC_PRESS_BLD_ART", "MDC_PRESS_BLD_VEN_CENT" };

//        if (aposChave == true && conta < 502)
//        {

//            Mindray mid = new();
//            DateTime data = DateTime.Now;
//            mid.Time = data.ToString("dd-MM-yyyy HH:mm:ss.ffff");
//            mid.Value = palavraChave;
//            waveMindray[palavraFora].Add(mid);
//            conta++;
//            if (conta >= 502)
//            {
//                conta = 0;
//                aposChave = false;
//            }
//        }
//        if (hearT.Contains(palavraChave))
//        {
//            if (!waveMindray.ContainsKey(palavraChave))
//            {
//                waveMindray[palavraChave] = new List<Mindray>();
//                palavraFora = palavraChave;
//                conta = 0;
//                aposChave = true;
//            }
//        }

//        if (aposkey == true && conta <= 130)
//        {
//            Mindray mid = new();
//            DateTime data = DateTime.Now;
//            mid.Time = data.ToString("dd-MM-yyyy HH:mm:ss.ffff");
//            mid.Value = palavraChave;
//            conta++;
//            waveMindray[palavraFora].Add(mid);
//            if (conta == 130)
//            {
//                aposkey = false;
//            }
//        }
//        if (breathing.Contains(palavraChave))
//        {
//            if (!waveMindray.ContainsKey(palavraChave))
//            {
//                waveMindray[palavraChave] = new List<Mindray>();
//                palavraFora = palavraChave;
//                conta = 0;
//                aposkey = true;
//            }
//        }
//    }
//    public static async void JsonCreated(Dictionary<string, List<Mindray>> dicionario)
//    {
//        string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "arquivo.json");
//        foreach (var dic in dicionario.Values)
//        {
//            if (dic.Count > 0)
//            {
//                dic.RemoveAt(0);
//                dic.RemoveAt(0);
//            }
//        }
//        string convert = JsonConvert.SerializeObject(dicionario, Formatting.Indented);
//        Console.WriteLine(convert);
//        using (HttpClient client = new HttpClient())
//        {
//            try
//            {
//                string url = "https://api.skopien.com.br/core/websocket-unique-channel/";
//                string token = "7c73402079ef7964eda17bf0bd0b367cc057d789";
//                client.DefaultRequestHeaders.Add("Authorization", $"token {token}");
//                var arquivo = new StringContent(convert, Encoding.UTF8, "application/json");
//                HttpResponseMessage message = client.PostAsync(url, arquivo).Result;
//                if (message.IsSuccessStatusCode)
//                {
//                    Console.WriteLine("Foi tudo certo ");
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Ocorreu uma exceção: {ex.Message}");
//            }
//        }
//    }
//}
