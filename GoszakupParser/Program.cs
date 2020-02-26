using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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
            var configuration = JsonSerializer.Deserialize<Configuration>(configurationString);
            var parserService = new ParserService(configuration);
            await parserService.StartParsing();
        }
    }
}