using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using NLog;
using NLog.Config;

namespace GoszakupParser
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            Console.Title = "Goszakup Parser";
            var configurationString = File.ReadAllText("Configuration.json");
            LogManager.Configuration = new XmlLoggingConfiguration("NLog.config");
            LogManager.Configuration.Variables["sourceAddress"] = GetLocalIpAddress();
            var configuration = JsonSerializer.Deserialize<Configuration>(configurationString);
            var parserService = new ParserService(configuration, args);
            await parserService.StartParsing();
        }

        private static string GetLocalIpAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }

            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}