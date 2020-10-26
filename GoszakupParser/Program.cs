﻿using NLog;
using System;
using System.IO;
using System.Net;
using System.Linq;
using NLog.Config;
using System.Text;
using CommandLine;
using System.Text.Json;
using System.Net.Sockets;
using System.Threading.Tasks;

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
            var configurationString = await File.ReadAllTextAsync($"{AppDomain.CurrentDomain.BaseDirectory}Configuration.json");
            var configuration = JsonSerializer.Deserialize<Configuration>(configurationString);

            // todo(Remove after making configuration static)
            Configuration.DbConnectionCredentialsStatic = configuration.DbConnectionCredentials;
            Configuration.ParsersStatic = configuration.Parsers;
            Configuration.AuthTokenStatic = configuration.AuthToken;

            // Initializing logger
            LogManager.Configuration =
                new XmlLoggingConfiguration($"{AppDomain.CurrentDomain.BaseDirectory}NLog.config");

            // Assigning ip address to a logger
            var host = await Dns.GetHostEntryAsync(Dns.GetHostName());
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

            // Disabling trace level of logging
            if (options.NoTrace)
                LogManager.Configuration.LoggingRules.ToList().ForEach(x => x.DisableLoggingForLevel(LogLevel.Trace));

            // Disabling info level of logging
            if (options.NoInfo)
                LogManager.Configuration.LoggingRules.ToList().ForEach(x => x.DisableLoggingForLevel(LogLevel.Info));

            // Disabling warn level of logging
            if (options.NoWarn)
                LogManager.Configuration.LoggingRules.ToList().ForEach(x => x.DisableLoggingForLevel(LogLevel.Warn));

            // Disabling error level of logging
            if (options.NoError)
                LogManager.Configuration.LoggingRules.ToList().ForEach(x => x.DisableLoggingForLevel(LogLevel.Error));

            // Disabling fatal level of logging
            if (options.NoFatal)
                LogManager.Configuration.LoggingRules.ToList().ForEach(x => x.DisableLoggingForLevel(LogLevel.Fatal));

            // Start parsing using given arguments
            var parserService = new ParserService(configuration, options);
            await parserService.StartParsingService();
        }
    }
}