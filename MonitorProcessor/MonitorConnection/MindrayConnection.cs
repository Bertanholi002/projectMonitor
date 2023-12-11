using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MonitorProcessor.Model;
using MonitorProcessor.Services;
using GodSharp.Sockets;
using WavesMindray;
using MonitorProcessor.MindrayTreatment;

namespace MonitorProcessor.MonitorConnection;

public class MindrayConnection
{
    public IPEndPoint remoteIpTarget;
    MindrayParameters mid = new();

    //public void IntermittentQueryInterfaceRequest(List<Monitors> monitors)
    //{
    //    List<Monitors> monitor = new();
    //    monitorMindray = monitor.Where(x => x.brand_obj.name.ToLower() == "mindray" && x.model_obj.name.ToLower() == "mindray").ToList();
    //    ConnectionMindray(monitorMindray);
    //}
    public void ConnectionMindray(string ip)
    {
        ITcpServer tcpServer = new TcpServer(5510)
        {
            OnConnected = (x) => { Console.WriteLine($"Monitor Mindray conectado"); },
            OnReceived = (x) =>
            {
                string data = Encoding.ASCII.GetString(x.Buffers);
                TestingWave test = new();
                test.ReadWaveMindray(data);
                mid.TranslatorParameters(data);

                //if(dadosConcatenados.Count == 17)
                //{
                //    mid.TranslatorParameters(data);
                //    dadosConcatenados.Clear();
                //}
                //if(dadosConcatenados.Count == 34)
                //{
                //    midWave.TranslatorMindray(data);
                //}
            },

            OnDisconnected = (x) => { Console.WriteLine($"{x.RemoteEndPoint} Desconectado"); },
            OnStarted = (x) => { Console.WriteLine($"{x.LocalEndPoint} start "); },
            OnStopped = (x) => { Console.WriteLine($"{x.LocalEndPoint} parado "); },
            OnException = (c) =>
            {
                Console.WriteLine($"{c.RemoteEndPoint} exception:{c.Exception.StackTrace.ToString()}.");
            },
            OnServerException = (c) =>
            {
                Console.WriteLine($"{c.LocalEndPoint} exception:{c.Exception.StackTrace.ToString()}.");
            }
        };
        tcpServer.UseKeepAlive(true, 500, 500);
        tcpServer.Start();
        while (true)
        {
            Console.ReadKey();
            tcpServer.Stop();
            Console.WriteLine("");
        }
    }
}