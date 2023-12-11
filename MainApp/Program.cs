using MainApp.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using MonitorProcessor.Services;
using MonitorProcessor.MonitorConnection;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using Timer = System.Timers.Timer;
using MonitorProcessor.Model;
using System.ComponentModel.Design;

namespace MainApp
{
    internal class Program
    {
        //variáveis de uso global
        static string APIServer, APIServerPort, UsernameAPI, PasswordAPI, ServerAPICloud;
        static double messagesServerMonitorsTime = 0;
        static double messagesClientMonitorsTime = 0;
        static Timer _timer_ServerMonitors, _timer_ClientMonitors;
        static PhilipsMpConnection connectionPhilipsMonitors;
        static int ServerType;

        //inicialização dos serviços
        static MonitorsService serviceMonitors = new MonitorsService();
        static MonitoringMapService serviceMonitoringMap = new MonitoringMapService();
        static LoginService authService = new LoginService();
        static TranslateDictionaryService dictionaryService = new TranslateDictionaryService();
        static Login IntegrationServerAuth = new Login();
        static PatientService patientService = new PatientService();
        static BedService bedService = new BedService();
        static MindrayConnection connection = new MindrayConnection();

        //variáveis de uso global para parâmetros do sistema
        static EquipmentDefautParametersService defaultParametersService = new EquipmentDefautParametersService();
        static List<EquipmentParametersDefault> equipmentParametersDefaults = new List<EquipmentParametersDefault>();
        static List<TranslateDictionary> dictionaries = new List<TranslateDictionary>();

        static void Main(string[] args)
        {
            Console.WriteLine("Inicializando Real Time Server");
            IConfiguration configuration = new ConfigurationBuilder()
             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
             .Build();
            ResourceConfiguration resourceConfiguration = new ResourceConfiguration();
            APIServer = resourceConfiguration.getAPIServer(configuration);
            APIServerPort = resourceConfiguration.getAPIServerPort(configuration);
            UsernameAPI = resourceConfiguration.getUsernameAPI(configuration);
            PasswordAPI = resourceConfiguration.getPasswordAPI(configuration);
            ServerAPICloud = resourceConfiguration.getAPIServerCloud(configuration);
            ServerType = resourceConfiguration.getServerType(configuration);
            messagesServerMonitorsTime = resourceConfiguration.getMessagesServerMonitorsTime(configuration);
            //Obtém token para autenticação e lista padrão de parâmetros de tradução
            try
            {
                IntegrationServerAuth = authService.getAuth(APIServer, APIServerPort, ServerAPICloud, ServerType, UsernameAPI, PasswordAPI);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na autenticação das APIs, verifique se o servidor está funcionando corretamente!");
                Console.WriteLine(ex.ToString());
                Environment.Exit(0);
            }

            if (IntegrationServerAuth != null)
            {
                try
                {
                    equipmentParametersDefaults = defaultParametersService.getAllDefaultParameters(APIServer, APIServerPort, ServerAPICloud, ServerType, IntegrationServerAuth);

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro no serviço de obtenção dos parâmetros padrões de tradução, verifique as APIs");
                    Console.WriteLine(ex.ToString());
                    Environment.Exit(0);
                }
                try
                {
                    dictionaries = dictionaryService.getAllDictionary(APIServer, APIServerPort, ServerAPICloud, ServerType, IntegrationServerAuth);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Erro no serviço de obtenção de dicionários para tradução de parâmetros, verifique as APIs");
                    Console.WriteLine(e.ToString());
                    Environment.Exit(0);
                }
                connectionPhilipsMonitors = new PhilipsMpConnection(IntegrationServerAuth, APIServer, APIServerPort, ServerAPICloud, ServerType);
                //connection.ConnectionMindray("170.233.51.179");
            }
            else
            {
                Console.WriteLine("Erro na autenticação das APIs, verifique se o servidor está funcionando corretamente");
                Environment.Exit(0);
            }

            //inicializando timer
            _timer_ServerMonitors = new Timer();
            _timer_ServerMonitors.AutoReset = false;
            _timer_ServerMonitors.Interval = messagesServerMonitorsTime;
            _timer_ServerMonitors.Elapsed += new ElapsedEventHandler(getMessagesFromPhillipsMonitors);
            _timer_ServerMonitors.Enabled = true;

            while (true) { }

        }

        static void getMessagesFromPhillipsMonitors(object sender, ElapsedEventArgs e)
        {
            _timer_ServerMonitors.Enabled = false;
            List<Monitors> monitors = new List<Monitors>();
            List<Patient> patients = new List<Patient>();
            List<Bed> beds = new List<Bed>();
            Console.WriteLine($"Obtendo mensagens dos monitores: {e.SignalTime}");
            try
            {
                monitors.Clear();
                patients.Clear();
                beds.Clear();
                monitors = serviceMonitors.getAllMonitors(APIServer, APIServerPort, ServerAPICloud, ServerType, IntegrationServerAuth);
                patients = patientService.getAllPatients(APIServer, APIServerPort, ServerAPICloud, ServerType, IntegrationServerAuth);
                beds = bedService.getAllBeds(APIServer, APIServerPort, ServerAPICloud, ServerType, IntegrationServerAuth);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao obter dados dos equipamentos que operam como servidores cadastrados, verifique as APIs");
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                Environment.Exit(0);
            }
            connectionPhilipsMonitors.IntermittentQueryInterfaceRequest(monitors, dictionaries, equipmentParametersDefaults, patients, beds);
            //connection.ConnectionMindray("170.233.51.179");
        }
    }
}