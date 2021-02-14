using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Proton_Bridge
{
    public class Client
    {
        public TcpClient TcpClient { get; set; }
        public NetworkStream NetworkStream { get; set; }

        public Client(TcpClient client)
        {
            TcpClient = client;
            NetworkStream = TcpClient.GetStream();
        }
    }

    public class ClientWorker
    {
        private TcpClient BridgeClient { get; set; }
        public async void SendAsync(TcpClient client, byte[] buffer)
        {
            Console.WriteLine($"Отправленная строка: {Encoding.UTF8.GetString(buffer)}");

            try
            {
                var stream = client.GetStream();

                await stream.WriteAsync(buffer, 0, buffer.Length);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public TcpClient BridgeConnection { get; set; }
        public async void ReceiveServer(Client client)
        {
            BridgeConnection = Bridge.MakeBridge();

            var stream = BridgeConnection.GetStream();
            byte[] receive_buffer;
            string json;

            while (BridgeConnection.Connected)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    byte[] buffer = new byte[256];

                    do
                    {
                        try
                        {
                            int received = await stream.ReadAsync(buffer, 0, 256);
                            await ms.WriteAsync(buffer, 0, received);
                            Console.WriteLine($"Получено {received} байт от клиента");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }

                    } while (BridgeConnection.Available > 0);

                    receive_buffer = ms.ToArray();

                    try
                    {
                        json = Encoding.UTF8.GetString(receive_buffer);

                        Console.WriteLine($"Полученная строка: {json}");

                        SendAsync(client.TcpClient, receive_buffer);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);

                        return;
                    }
                }
            }
        }
        public async void ReceiveClient(Client client)
        {
            var stream = client.TcpClient.GetStream();
            byte[] receive_buffer;
            string json;

            while (client.TcpClient.Connected)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    byte[] buffer = new byte[256];

                    do
                    {
                        try
                        {
                            int received = await stream.ReadAsync(buffer, 0, 256);
                            await ms.WriteAsync(buffer, 0, received);
                            Console.WriteLine($"Получено {received} байт от клиента");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }

                    } while (client.TcpClient.Available > 0);

                    receive_buffer = ms.ToArray();

                    try
                    {
                        json = Encoding.UTF8.GetString(receive_buffer);

                        Console.WriteLine($"Полученная строка: {json}");

                        SendAsync(BridgeConnection, receive_buffer);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);

                        return;
                    }
                }
            }
        }
        public void Work(Client client)
        {
            Thread clientThread = new Thread(() => ReceiveClient(client));
            Thread serverThread = new Thread(() => ReceiveServer(client));
            
            clientThread.Start();

            serverThread.Start();
        }
    }

    public class Connections<T> where T : Client
    {
        public Connections()
        {
            Clients = new ObservableCollection<Client>();

            Clients.CollectionChanged += Clients_CollectionChanged;
        }

        private void Clients_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                Console.WriteLine("Подключен новый клиент.");

                Console.WriteLine(sender.GetType());

                var clientWorker = new ClientWorker();

                var client = e.NewItems[0] as Client;

                Console.WriteLine(client.TcpClient.Connected);

                clientWorker.Work(client);
            }
        }

        private ObservableCollection<Client> Clients { get; set; }
        
        public void Add(T client)
        {
            Clients.Add(client);
        }
    }
    public class Bridge
    {
        /// <summary>
        /// IP - адрес сервера
        /// </summary>
        public static string ServerIPAddress { get; set; }
        /// <summary>
        /// Порт для подключения к серверу
        /// </summary>
        public static UInt16 ServerPort { get; set; }
        /// <summary>
        /// Порт для прослушивания
        /// </summary>
        public UInt16 ListenPort { get; set; }
        private TcpListener TcpListener { get; set; }
        public Connections<Client> Connections { get; set; }
        public Bridge()
        {

        }

        public static TcpClient MakeBridge()
        {
            var tcp = new TcpClient();

            try
            {
                tcp.Connect(ServerIPAddress, ServerPort);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return tcp;
        }

        public void Listen()
        {
            if (ListenPort == 0)
            {
                throw new ArgumentNullException(nameof(ListenPort), "Not a valid port");
            }

            Connections = new Connections<Client>();

            TcpListener = new TcpListener(new IPEndPoint(IPAddress.Any, ListenPort));

            TcpListener.Start();

            AcceptClients();
        }

        public void AcceptClients()
        {
            while (true)
            {
                var tcpClient = TcpListener.AcceptTcpClient();

                var client = new Client(tcpClient);

                Connections.Add(client);
            }
        }

        public bool IsConnectionAvailable()
        {
            TcpClient tcpClient = new TcpClient();

            try
            {
                tcpClient.Connect(ServerIPAddress, ServerPort);

                tcpClient.Dispose();

                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);

                return false;
            }
        }


    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Быстрая настройка");

            Console.Write("Введите IP адрес сервера или моста: ");

            string ip = Console.ReadLine();

            Console.Write("Введите порт для подключения к серверу или мосту: ");

            UInt16 port = Convert.ToUInt16(Console.ReadLine());

            Console.Write("Введите порт для прослушивания: ");

            UInt16 portListen = Convert.ToUInt16(Console.ReadLine());

            Bridge bridge = new Bridge();

            bridge.ListenPort = portListen;

            Bridge.ServerPort = port;

            Bridge.ServerIPAddress = ip;

            Console.WriteLine("Устанавливается соединение...");

            if (!bridge.IsConnectionAvailable())
            {
                Console.Clear();

                Main(args);

                Console.WriteLine("Подключение не было установлено, повторите попытку.\n\t");
            }
            else
            {
                Console.WriteLine("Соединение установлено. ;)");
            }

            bridge.Listen();

            Console.ReadLine();
        }
    }
}
