using MySql.Data.MySqlClient;
using NLog;
using Proton_Server_Core.Models;
using System;
using System.Data.Common;
using System.Threading.Tasks;


namespace Proton_Server_Core.Database
{
    class AccountsDB
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();
        public static async Task<int> GetAccountId(string email)
        {
            var connection = Database.MySqlConnection;

            connection.Open();

            var command = new MySqlCommand("SELECT Id, Email FROM Accounts", connection);

            var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                if (reader.GetString(1) == email)
                {
                    int id = reader.GetInt32(0);

                    await connection.CloseAsync();

                    await reader.CloseAsync();

                    await reader.DisposeAsync();

                    await connection.DisposeAsync();

                    return id;
                }
            }

            await connection.CloseAsync();

            await reader.CloseAsync();

            await reader.DisposeAsync();

            await connection.DisposeAsync();

            return 0;
        }
        public static async Task<bool> AccountExists(string email, string password)
        {
            var connection = Database.MySqlConnection;

            if (Database.IsEnabled)
            {
                connection.Open();

                var command = new MySqlCommand("SELECT Email, Password FROM accounts", connection);

                var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    if (reader.GetString(0) == email && reader.GetString(1) == password)
                    {
                        await connection.CloseAsync();

                        await reader.CloseAsync();

                        await reader.DisposeAsync();

                        await connection.DisposeAsync();

                        return true;
                    }
                }

                await connection.CloseAsync();

                await reader.CloseAsync();

                await reader.DisposeAsync();

                await connection.DisposeAsync();
            }

            return false;
        }

        public static async Task<User> GetUserAsync(int id)
        {
            var connection = Database.MySqlConnection;

            connection.Open();

            var command = new MySqlCommand("SELECT * FROM Accounts", connection);

            var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                if (reader.GetInt32(0) == id)
                {
                    var user = new User()
                    {
                        Username = reader.GetString(3)
                    };

                    await connection.CloseAsync();

                    await reader.CloseAsync();

                    await reader.DisposeAsync();

                    await connection.DisposeAsync();

                    return user;
                }
            }

            await connection.CloseAsync();

            await reader.CloseAsync();

            await reader.DisposeAsync();

            await connection.DisposeAsync();

            return new User();
        }
        public static async Task<bool> CreateAccount(string email, string password)
        {
            var connection = Database.MySqlConnection;

            if (Database.IsEnabled)
            {
                connection.Open();

                var command = new MySqlCommand($"INSERT INTO accounts (Email, Password) VALUES (\"{email}\", \"{password}\")", connection);

                try
                {
                    await command.ExecuteNonQueryAsync();
                }
                catch(DbException ex)
                {
                    Logger.Error($"SQL INJECT: {ex.Message}", ConsoleColor.Red);

                    return false;
                }
                await connection.CloseAsync();

                return true;
            }

            await connection.DisposeAsync();

            return false;
        }
        public static async Task<bool> EmailExists(string email)
        {
            var connection = Database.MySqlConnection;

            if (Database.IsEnabled)
            {
                connection.Open();

                var command = new MySqlCommand($"SELECT Email FROM accounts", connection);

                var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    if (reader.GetString(0) == email)
                    {
                        reader.Close();

                        connection.Close();

                        return true;
                    }
                }

                await reader.CloseAsync();

                await connection.CloseAsync();

                await connection.DisposeAsync();
            }

            return false;
        }
    }
}
