using System;
using System.IO;
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
            var configuration = JsonSerializer.Deserialize<Configuration>(configurationString);
            var parserService = new ParserService(configuration);
           await parserService.StartParsing();
        }
    }
}