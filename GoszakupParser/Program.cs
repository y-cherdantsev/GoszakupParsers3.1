using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using CommandLine;
using NLog;
using NLog.Config;

// ReSharper disable IdentifierTypo
// ReSharper disable CommentTypo

namespace GoszakupParser
{
    /// @author Yevgeniy Cherdantsev
    /// @date 26.02.2020 18:18:55
    internal static class Program
    {
        /// <summary>
        /// Enter point
        /// </summary>
        /// <param name="args">Command Line arguments</param>
        private static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;

            // ReSharper disable once StringLiteralTypo
            Console.Title = "Goszakup Parser";

            // Loading configurations
            var configurationString = File.ReadAllText("Configuration.json");
            var configuration = JsonSerializer.Deserialize<Configuration>(configurationString);

            // Initializing logger
            LogManager.Configuration = new XmlLoggingConfiguration("NLog.config");

            // Assigning ip address to a logger
            var host = Dns.GetHostEntry(Dns.GetHostName());
            var ip = host.AddressList.LastOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork)?.ToString();
            LogManager.Configuration.Variables["sourceAddress"] = ip;

            // Parsing arguments
            var parser = new Parser(with =>
            {
                with.CaseSensitive = false;
                with.HelpWriter = Console.Out;
                with.AutoHelp = true;
            });
            var result = parser.ParseArguments<CommandLineOptions>(args);
            if (result.Tag == ParserResultType.NotParsed)
                return;
            var options = ((Parsed<CommandLineOptions>) result).Value;

            // Start parsing using given arguments
            var parserService = new ParserService(configuration, options);
            parserService.StartParsingService().GetAwaiter().GetResult();
        }
    }
}