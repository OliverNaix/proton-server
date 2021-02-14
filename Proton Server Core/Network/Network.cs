using NLog;
using Proton_Server_Core.Converters;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Proton_Server_Core.Network
{
    public class Network
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();
        public IPAddress IPAddress { get; set; }
        public UInt16 Port { get; set; }
        private readonly TcpListener _tcpListener;
        public List<Client> ConnectedClients { get; set; }
        public Network(NetworkSettings networkSettings)
        {
            _tcpListener = new TcpListener(networkSettings.IPAddress, networkSettings.Port);
        }


        public Network(IPAddress iPAddress, UInt16 port)
        {
            this.IPAddress = IPAddress;
            this.Port = port;

            _tcpListener = new TcpListener(IPAddress, Port);
        }
        public static async Task SendAsync(Client client, byte[] buffer)
        {
            Logger.Info($"Отправленная строка: {Encoding.UTF8.GetString(buffer)}");

            try
            {
                var stream = client.TcpClient.GetStream();
                await stream.WriteAsync(buffer, 0, buffer.Length);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        public async Task Listen()
        {
            int connections = 0;

            if (ConnectedClients == null)
            {
                ConnectedClients = new List<Client>();
            }
            
            _tcpListener.Start();
            
            while (true)
            {
                var tcp = await _tcpListener.AcceptTcpClientAsync();

                Logger.Info("Подключился новый клиент");

                var client = new Client(tcp, connections++);

                Console.WriteLine(tcp.Client.LocalEndPoint);


                ConnectedClients.Add(client);

                new Thread(async ()=> await new ClientWorker.ClientManager(client).Work()).Start();
            }
        }
    }
}
