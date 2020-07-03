using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AngleSharp.Common;
using CommandLine;
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


            //Loading configurations
            var configurationString = File.ReadAllText("Configuration.json");
            var configuration = JsonSerializer.Deserialize<Configuration>(configurationString);

            //Initializing logger
            LogManager.Configuration = new XmlLoggingConfiguration("NLog.config");

            //Assigning ip address to a logger
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    LogManager.Configuration.Variables["sourceAddress"] = ip.ToString();


            Parser.Default.ParseArguments<CommandLineOptions>(args)
                .WithParsed(options =>
                {
                    var parserService = new ParserService(configuration, options);
                    parserService.StartParsingService().GetAwaiter().GetResult();
                })
                .WithNotParsed(errors =>
                {
                    using var err = errors.GetEnumerator();
                    while (err.MoveNext())
                        if (err.Current != null)
                            Console.WriteLine(err.Current.Tag.GetMessage());
                    throw new Exception("Some error occured while parsing arguments");
                });
        }
    }
}