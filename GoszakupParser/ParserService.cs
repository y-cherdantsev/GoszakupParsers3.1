using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CommandLine;
using GoszakupParser.Contexts;
using GoszakupParser.Models;
using GoszakupParser.Parsers.ApiParsers.AimParsers;
using GoszakupParser.Parsers.ApiParsers.SequentialParsers;
using GoszakupParser.Parsers.WebParsers.AimParsers;
using NLog;
using Parser = GoszakupParser.Parsers.Parser;

namespace GoszakupParser
{
    /// @author Yevgeniy Cherdantsev
    /// @date 26.02.2020 18:18:55
    /// @version 1.0
    /// <summary>
    /// Proceeding of arguments and general logic
    /// </summary>
    public class ParserService
    {
        private readonly Configuration _configuration;

        private readonly Logger _logger;

        private readonly CommandLineOptions _options;

        private Dictionary<string, string> ParserMonitoringNames { get; set; } = new Dictionary<string, string>
        {
            {"ParticipantParser", "ParticipantGoszakup"},
            {"UnscrupulousParser", "UnscrupulousGoszakup"},
            {"ContractParser", "ContractGoszakup"},
            {"AnnouncementParser", "AnnouncementGoszakup"},
            {"LotParser", "LotGoszakup"},
            {"PlanParser", "PlanGoszakup"},
            {"DirectorParser", "DirectorGoszakup"},
            {"RnuReferenceParser", "RnuReferenceGoszakup"}
        };

        public ParserService(Configuration configuration, CommandLineOptions options)
        {
            _options = options;
            _configuration = configuration;
            _logger = LogManager.GetCurrentClassLogger();
        }

        public async Task StartParsingService()
        {
            _logger.Info("Starting parsing service!");
            foreach (var parserName in _options.Parsers)
            {
                try
                {
                    if (IsAvailable(parserName))
                        ProceedParsing(parserName).GetAwaiter().GetResult();
                }
                catch (Exception e)
                {
                    _logger.Warn(e);
                }
            }
        }

        private async Task ProceedParsing(string parserName)
        {
            await using var parserMonitoringContext = new ParserMonitoringContext();

            //Get proxy for parser
            var proxyDto = parserMonitoringContext.Proxies.OrderBy(x => new Random().NextDouble()).First();
            var proxy = new WebProxy(
                $"{proxyDto.Address}:{proxyDto.Port}",
                true)
            {
                Credentials = new NetworkCredential
                {
                    UserName = proxyDto.Username,
                    Password = proxyDto.Password
                }
            };

            //Get class for parser
            var parsingClass = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .FirstOrDefault(t => t.Name == parserName);

            //Getting parser configuration
            var parserConfiguration = _configuration.Parsers.FirstOrDefault(x => x.Name.Equals(parserName));


            //Initializing and starting parser
            Parser parser;
            if (parsingClass.GetConstructors()[0].GetParameters().Any(x => x.Name.Equals("authToken")))
                parser = (Parser) Activator.CreateInstance(parsingClass, parserConfiguration, proxy,
                    _configuration.AuthToken);
            else
                parser = (Parser) Activator.CreateInstance(parsingClass, parserConfiguration, proxy);

            await parser.ParseAsync();
            var parsed =
                parserMonitoringContext.ParserMonitorings.FirstOrDefault(x =>
                    x.Name.Equals(ParserMonitoringNames[parserName]));


            if (parsed != null)
            {
                parsed.LastParsed = DateTime.Now;
                parsed.Parsed = true;
                parserMonitoringContext.ParserMonitorings.Update(parsed);
                await parserMonitoringContext.SaveChangesAsync();
            }
        }

        private bool IsAvailable(string parserName)
        {
            return Exists(parserName) && CheckState(parserName);
        }

        private bool Exists(string parserName)
        {
            using var parserMonitoringContext = new ParserMonitoringContext();

            var existInDictionary = ParserMonitoringNames.Keys
                .Any(x => x.Equals(parserName));
            if (!existInDictionary)
            {
                _logger.Warn($"Parser '{parserName}' doesn't exist in local dictionary");
                return false;
            }

            var existsInDatabase = parserMonitoringContext.ParserMonitorings
                .Any(x => x.Name.Equals(ParserMonitoringNames[parserName]));
            if (!existsInDatabase)
            {
                _logger.Warn($"Parser '{parserName}' doesn't exist in database");
                return false;
            }

            var existClass = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Any(x => x.Name.Equals(parserName));
            if (!existClass)
            {
                _logger.Warn($"Parser '{parserName}' doesn't exist in current context");
                return false;
            }

            var existConfiguration = _configuration.Parsers
                .Any(x => x.Name.Equals(parserName));
            if (!existConfiguration)
            {
                _logger.Warn($"There is no configuration for '{parserName}' parser");
                return false;
            }

            return true;
        }

        private bool CheckState(string parserName)
        {
            if (_options.Force)
                return true;


            using var parserMonitoringContext = new ParserMonitoringContext();

            //Check statuses if has no options determined
            if (!_options.Force && !_options.Ignore)
            {
                var isParsed = parserMonitoringContext.ParserMonitorings
                    .FirstOrDefault(x =>
                        x.Parsed
                        && x.Name.Equals(ParserMonitoringNames[parserName]
                        )) != null;
                if (!isParsed)
                {
                    _logger.Warn($"Parser '{ParserMonitoringNames[parserName]}' hasn't been migrated yet");
                    return false;
                }
            }

            //Check active field if ignore option determined
            if (_options.Ignore)
            {
                var isActive = parserMonitoringContext.ParserMonitorings
                    .FirstOrDefault(x =>
                        x.Active
                        && x.Name.Equals(ParserMonitoringNames[parserName]
                        )) != null;
                if (!isActive)
                {
                    _logger.Warn($"Parser '{ParserMonitoringNames[parserName]}' is not active");
                    return false;
                }
            }

            return true;
        }
    }
}