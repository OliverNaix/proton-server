using NLog;
using Proton_Server_Core.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Proton_Server_Core.Network.ClientWorker
{
   
    public class ClientManager
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();
        private Client _client { get; set; }
        public string LastMessageType { get; set; }
        public ClientManager(Client client)
        {
            _client = client;
        }
        public async Task SelectAction(Update updates)
        {
            var result = Actions.Actions.Run(_client, updates);

            await Network.SendAsync(_client, Converters.JsonConvert.ToByteArray(result));
        }
        public async Task<Thread> Work()
        {
            Thread thread = Thread.CurrentThread;

            var stream = _client.TcpClient.GetStream();
            byte[] receive_buffer;
            string json;

            while (_client.TcpClient.Connected)
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


                           

                            Logger.Info($"Получено {received} байт от клиента с ID: {_client.ClientData.Id}");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);

                            return Thread.CurrentThread;
                        }

                    } while (_client.TcpClient.Available > 0);
                    
                    receive_buffer = ms.ToArray();

                    try
                    {
                        json = Encoding.UTF8.GetString(receive_buffer);

                        Console.WriteLine($"Полученная строка: {json}");
                    }
                    catch
                    {
                        return Thread.CurrentThread;
                    }

                    try
                    {
                        Update updates = JsonSerializer.Deserialize<Update>(json);

                        Logger.Info($"{(string.IsNullOrEmpty(_client.ClientData.Email) ? "Неизвестный пользователь" : _client.ClientData.Email)} последний запрос: {_client.LastActionType}");

                        Logger.Info($"{(string.IsNullOrEmpty(_client.ClientData.Email) ? "Неизвестный пользователь" : _client.ClientData.Email)} сделал запрос на {updates.Type}");

                        await SelectAction(updates);
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);

                        if (receive_buffer.Length == 0)
                        {
                            return Thread.CurrentThread;
                        }
                    }
                }
            }

            return Thread.CurrentThread;
        }
    }
}
