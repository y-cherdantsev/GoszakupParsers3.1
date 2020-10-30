using NLog;
using System;
using System.Net;
using System.Linq;
using NLog.Config;
using System.Text;
using CommandLine;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo

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
        private static async Task Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;

            // ReSharper disable once StringLiteralTypo
            Configuration.Title = "Goszakup Parser";

            Console.WriteLine(@"
..................................................................
...%%%%....%%%%....%%%%...%%%%%%...%%%%...%%..%%..%%..%%..%%%%%...
..%%......%%..%%..%%.........%%...%%..%%..%%.%%...%%..%%..%%..%%..
..%%.%%%..%%..%%...%%%%.....%%....%%%%%%..%%%%....%%..%%..%%%%%...
..%%..%%..%%..%%......%%...%%.....%%..%%..%%.%%...%%..%%..%%......
...%%%%....%%%%....%%%%...%%%%%%..%%..%%..%%..%%...%%%%...%%......
..................................................................
......%%%%%....%%%%...%%%%%....%%%%...%%%%%%..%%%%%....%%%%.......
......%%..%%..%%..%%..%%..%%..%%......%%......%%..%%..%%..........
......%%%%%...%%%%%%..%%%%%....%%%%...%%%%....%%%%%....%%%%.......
......%%......%%..%%..%%..%%......%%..%%......%%..%%......%%......
......%%......%%..%%..%%..%%...%%%%...%%%%%%..%%..%%...%%%%.......
..................................................................
");

            // Removing checking SSL cert
            ServicePointManager.ServerCertificateValidationCallback =
                (some, kind, of, shit) => true;

            // Loading configurations
            var configuration = new ConfigurationBuilder().AddJsonFile("Configuration.json").Build();

            Configuration.AuthToken = configuration["AuthToken"];
            Configuration.Parsers = configuration.GetSection("Parsers").Get<List<Configuration.ParserSettings>>();
            Configuration.Downloaders =
                configuration.GetSection("Downloaders").Get<List<Configuration.DownloaderSettings>>();
            Configuration.ParserMonitoringNames =
                configuration.GetSection("ParserMonitoringNames").Get<Dictionary<string, string>>();
            Configuration.ParsingDbConnectionString = configuration.GetConnectionString("Parsing");
            Configuration.ProductionDbConnectionString = configuration.GetConnectionString("Production");

            // Initializing logger
            LogManager.Configuration =
                new XmlLoggingConfiguration($"{AppDomain.CurrentDomain.BaseDirectory}NLog.config");

            // Assigning ip address to a logger
            var host = await Dns.GetHostEntryAsync(Dns.GetHostName());
            var ip = host.AddressList.LastOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork)?.ToString();
            LogManager.Configuration.Variables["sourceAddress"] = ip;

            // Parsing arguments
            new Parser(with =>
                {
                    with.CaseSensitive = false;
                    with.HelpWriter = Console.Out;
                    with.AutoHelp = true;
                })
                .ParseArguments<CommandLineOptions.Parse, CommandLineOptions.Download>(args)
                .MapResult(
                    (CommandLineOptions.Parse opts) =>
                    {
                        Actions.LoggerPoliciesInstallation(opts);
                        Actions.Parse(opts).GetAwaiter().GetResult();
                        return 0;
                    },
                    (CommandLineOptions.Download opts) =>
                    {
                        Actions.LoggerPoliciesInstallation(opts);
                        Actions.Download(opts).GetAwaiter().GetResult();
                        return 0;
                    },
                    errs => 1);
        }
    }
}