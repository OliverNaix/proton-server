using MySql.Data.MySqlClient;
using Proton_Server_Core.Models;
using Proton_Server_Core.Network;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Proton_Server_Core.Database
{
    class DialogsDB
    {
        public static string Table { get => "Dialogs"; }
        public static async Task<List<Dialog>> GetDialogs(Client client)
        {
            List<Dialog> dialogs = new List<Dialog>();

            var connection = Database.MySqlConnection;

            connection.Open();

            var command = new MySqlCommand($"SELECT * FROM {Table}", connection);

            var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                if (reader.GetInt32(2) == client.ClientData.Id ||
                    reader.GetInt32(3) == client.ClientData.Id)
                {
                    List<User> users = new List<User>();

                    users.Add(await AccountsDB.GetUserAsync(reader.GetInt32(3)));

                    users.Add(await AccountsDB.GetUserAsync(reader.GetInt32(2)));

                    dialogs.Add(new Dialog()
                    {
                        Users = users
                    });
                }
            }

            await connection.CloseAsync();

            await reader.CloseAsync();

            return dialogs;
        }
    }
}
