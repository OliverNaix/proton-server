using NLog;
using NLog.Fluent;
using System;
using System.Net;

namespace Proton_Server_Core
{
    class Program
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            Logger.Info("Запуск сервера");

            Logger.Info("Подключение к базе данных: " + Database.Database.IsEnabled);

            Logger.Info("Инициализация пользовательских функций");

            Actions.Actions.Init();

            Logger.Info("Настройка сетевых параметров.");

            Network.NetworkSettings settings = new Network.NetworkSettings();

            settings.IPAddress = IPAddress.Any;

            settings.Port = 9015;

            Network.Network network = new Network.Network(settings);

            Logger.Info("Ожидание подключений.");

            network.Listen().GetAwaiter().GetResult();
        }
    }
}
