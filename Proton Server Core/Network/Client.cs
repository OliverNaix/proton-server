using Proton_Server_Core.Database;
using System.Net.Sockets;

namespace Proton_Server_Core.Network
{
    public class Client
    {
        public Client(TcpClient tcpClient, int id)
        {
            this.TcpClient = tcpClient;
        }
        
        public TcpClient TcpClient { get; set; }
        public ClientData ClientData { get; set; } = new ClientData();
        public string LastActionType { get; set; }
    }
}
