using MySql.Data.MySqlClient;
using Proton_Server_Core.Models;
using Proton_Server_Core.Network;
using System;
using System.Threading.Tasks;

namespace Proton_Server_Core.Database
{
    class MessagesDB
    {
        public static string Table { get => "Messages"; }
        public static async Task SaveMessage(Message message, Client client)
        {
            var connection = Database.MySqlConnection;

            await connection.OpenAsync();

            var command = new MySqlCommand($"INSERT INTO {Table} (author_id, date_time, text) VALUES(@{client.ClientData.Id}, @{DateTime.UtcNow.ToString()}, @{message.Text})", connection);

            await command.ExecuteNonQueryAsync();
        }
    }
}
