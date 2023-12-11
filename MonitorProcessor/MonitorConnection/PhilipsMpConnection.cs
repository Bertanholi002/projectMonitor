using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MonitorProcessor.Model;
using MonitorProcessor.PhillipsMPTreatment;
using MonitorProcessor.Services;

namespace MonitorProcessor.MonitorConnection
{
    public class PhilipsMpConnection
    {
        public static string DeviceId;
        public IPEndPoint m_remoteIPtarget;
        private MonitorsService monitorsService;
        private MonitoringService monitoringService;
        private Login IntegrationServerAuth;
        private JsonSerializerOptions optionsSerialize;
        private string APIServer, APIServerPort, ServerAPICloud;
        private int ServerType;
        MPudpclient udpCient = MPudpclient.getInstance;

        public class UdpState
        {
            //Udp client
            public MPudpclient udpClient;
            //RemoteIP
            public IPEndPoint remoteIP;
        }

        public PhilipsMpConnection(Login login, string APIServer, string APIServerPort, string ServerAPICloud, int ServerType)
        {
            monitorsService = new MonitorsService();
            monitoringService = new MonitoringService();
            optionsSerialize = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
            this.IntegrationServerAuth = login;
            this.APIServer = APIServer;
            this.APIServerPort = APIServerPort;
            this.ServerAPICloud = ServerAPICloud;
            this.ServerType = ServerType;
        }
        public void IntermittentQueryInterfaceRequest(List<Monitors> monitors, List<TranslateDictionary> dictionaries, List<EquipmentParametersDefault> equipmentParametersDefaults, List<Patient> patients, List<Bed> bed)
        {
            List<Monitors> monitoresPhilips = new();
            monitoresPhilips = monitors.Where(x => x.brand_obj.name.ToLower() == "phillips" && x.model_obj.name.ToLower() == "mp40").ToList();
            ConnectMp40Monitors(monitoresPhilips, dictionaries, equipmentParametersDefaults, patients, bed);


        }
        private void ConnectMp40Monitors(List<Monitors> serverMonitors, List<TranslateDictionary> dictionaries, List<EquipmentParametersDefault> equipmentParametersDefaults, List<Patient> patients, List<Bed> bed)
        {
            foreach (Monitors monitor in serverMonitors)
            {
                try
                {
                    IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(monitor.ip), Convert.ToInt32(monitor.port));
                    GetInfoMp40(udpCient, iPEndPoint);

                }
                catch (Exception e) { Console.WriteLine($"Erro ao se conectar com o monitor: {monitor.ip} - {monitor.mac_address} - {monitor.brand_obj.name} "); }

            }
        }

        public void GetInfoMp40(MPudpclient udpClient, IPEndPoint iPEndPoint)
        {
            int nInterval = 1000;
            short waveForm = 1;
            short nWaveForm = 1;

            udpCient.m_calibratewavevalues = false;

            udpClient.m_remoteIPtarget = new IPEndPoint(iPEndPoint.Address, iPEndPoint.Port);
            udpClient.m_DeviceID = DeviceId;
            try
            {
                udpClient.Connect(udpClient.m_remoteIPtarget);
                UdpState state = new()
                {
                    udpClient = udpClient,
                    remoteIP = udpClient.m_remoteIPtarget
                };


                udpCient.SendAssociationRequest();
                byte[] readPatient = udpCient.Receive(ref udpCient.m_remoteIPtarget);
                udpCient.ProcessPacket(readPatient);

                //udpClient.SendWaveAssociationRequest();
                //string deskTopPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "primeirodocumento.txt");
                //byte[] readmdsconnectbuffer = udpClient.Receive(ref udpClient.m_remoteIPtarget);
                //udpCient.ByteArrayToFile(deskTopPath, readmdsconnectbuffer, readmdsconnectbuffer.GetLength(0));

                //udpCient.ProcessPacket(readmdsconnectbuffer);

                Task.Run(() => udpCient.SendCycledExtendedPollDataRequest(nInterval));
                if (waveForm != 0)
                {
                    udpCient.GetRTSAPriorityListRequest();
                    if (nWaveForm != 11)
                    {
                        udpCient.SetRTSAPriorityList(1);
                    }

                    Task.Run(() => udpCient.SendCycledExtendedPollWaveDataRequest(nInterval));
                }
                Task.Run(() => udpCient.RecheckMDSAttributes(nInterval));

                udpCient.BeginReceive(new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private static ManualResetEvent receiveDone = new ManualResetEvent(false);
        public static void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the state object and the client   
                // from the asynchronous state object.  
                UdpState state = (UdpState)ar.AsyncState;

                MPudpclient Client = state.udpClient;

                string path = Path.Combine(Directory.GetCurrentDirectory(), "MPrawoutput.txt");

                // Read data from the remote device.  
                byte[] readbuffer = Client.EndReceive(ar, ref state.remoteIP);
                int bytesRead = readbuffer.GetLength(0);
                if (bytesRead > 0)
                {
                    //Client.ByteArrayToFile(path, readbuffer, bytesRead);

                    Client.ReadData(readbuffer);

                    //  Get the rest of the data.  
                    Client.BeginReceive(new AsyncCallback(ReceiveCallback), state);
                }
                else
                {

                    receiveDone.Set();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
