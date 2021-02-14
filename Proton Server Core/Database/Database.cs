using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proton_Server_Core.Database
{
    public static class Database
    {
        public static string ConnecctionString = "Server=127.0.0.1;Database=proton;Uid=root;Pwd=Oliver15243@;";

        public static MySqlConnection MySqlConnection
        {
            get => new MySqlConnection(ConnecctionString);
        }

        public static bool IsEnabled    
        {
            get
            {
                try
                {
                    MySqlConnection.Open();

                    MySqlConnection.Close();

                    MySqlConnection.Dispose();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
