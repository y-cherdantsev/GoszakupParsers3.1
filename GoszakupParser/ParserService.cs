using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GoszakupParser.Contexts;
using NLog;
using Parser = GoszakupParser.Parsers.Parser;

namespace GoszakupParser
{
    /// @author Yevgeniy Cherdantsev
    /// @date 26.02.2020 18:18:55
    /// <summary>
    /// Proceeding of arguments and general logic
    /// </summary>
    public class ParserService
    {
        /// <summary>
        /// Configuration defined in 'Configuration.json' file
        /// </summary>
        private readonly Configuration _configuration;

        /// <summary>
        /// Current class logger
        /// </summary>
        private readonly Logger _logger;

        /// <summary>
        /// Command line options
        /// </summary>
        private readonly CommandLineOptions _options;

        /// <summary>
        /// Mapped parsers names into parsing DB titles
        /// </summary>
        // private static Dictionary<string, string> ParserMonitoringNames { get; } = new Dictionary<string, string>
        // {
        //     {"Participant", "ParticipantGoszakup"},
        //     {"Unscrupulous", "UnscrupulousGoszakup"},
        //     {"Contract", "ContractGoszakup"},
        //     {"Announcement", "AnnouncementGoszakup"},
        //     {"Lot", "LotGoszakup"},
        //     {"Plan", "PlanGoszakup"},
        //     {"Director", "DirectorGoszakup"},
        //     {"RnuReference", "RnuReferenceGoszakup"}
        // };

        /// <summary>
        /// Constructor of ParserService class
        /// </summary>
        /// <param name="configuration">Parser configurations</param>
        /// <param name="options">Parsed command line options</param>
        /// <returns></returns>
        public ParserService(Configuration configuration, CommandLineOptions options)
        {
            _options = options;
            _configuration = configuration;
            _logger = LogManager.GetCurrentClassLogger();
        }

        /// <summary>
        /// Starts parsing activity
        /// </summary>
        public async Task StartParsingService()
        {
            _logger.Info("Starting parsing service!");

            // Trying to start each defined parser sequentially in defined order
            foreach (var parserName in _options.Parsers)
            {
                try
                {
                    if (IsAvailable(parserName))
                        await ProceedParsing(parserName);
                }
                catch (Exception e)
                {
                    _logger.Warn(e);
                }
            }
        }

        /// <summary>
        /// Generating object of given parser and starts it
        /// </summary>
        /// <param name="parserName">Parser name</param>
        private async Task ProceedParsing(string parserName)
        {
            await using var parserMonitoringContext = new ParserMonitoringContext();

            // Get proxy for parser
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
            await parserMonitoringContext.DisposeAsync();

            // Get class for parser
            var parsingClass = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .FirstOrDefault(t => t.Name == parserName + "Parser");

            // Get parser configuration
            var parserConfiguration = _configuration.Parsers.FirstOrDefault(x => x.Name.Equals(parserName));


            // Initializing and starting parser
            Parser parser;

            // ReSharper disable once PossibleNullReferenceException (Already checked)
            if (parsingClass.GetConstructors()[0].GetParameters().Any(x => x.Name.Equals("authToken")))
                parser = (Parser) Activator.CreateInstance(parsingClass, parserConfiguration, proxy,
                    _configuration.AuthToken);
            else
                parser = (Parser) Activator.CreateInstance(parsingClass, parserConfiguration, proxy);

            if (parser != null) await parser.ParseAsync();
            else throw new NullReferenceException($"{parserName} hasn't been created");

            // Update data in monitoring table
            var parsed =
                parserMonitoringContext.ParserMonitorings.FirstOrDefault(x =>
                    x.Name.Equals(_configuration.ParserMonitoringNames[parserName]));

            // ReSharper disable once PossibleNullReferenceException (Already checked)
            // Possible reason of null: field has been deleted from DB table while parsing has been proceeding
            parsed.LastParsed = DateTime.Now;
            parsed.Parsed = true;
            parserMonitoringContext.ParserMonitorings.Update(parsed);
            await parserMonitoringContext.SaveChangesAsync();
        }

        /// <summary>
        /// Check if parser available based on current system state and given values
        /// </summary>
        /// <param name="parserName">Parser name</param>
        /// <returns>Boolean</returns>
        private bool IsAvailable(string parserName)
        {
            return Exists(parserName) && CheckState(parserName);
        }

        /// <summary>
        /// Check if parser fully integrated in parsing system
        /// </summary>
        /// <param name="parserName">Parser name</param>
        /// <returns>Boolean</returns>
        private bool Exists(string parserName)
        {
            using var parserMonitoringContext = new ParserMonitoringContext();

            // Check if parser exist in map dictionary
            var existInDictionary = _configuration.ParserMonitoringNames.Keys
                .Any(x => x.Equals(parserName));
            if (!existInDictionary)
            {
                _logger.Warn($"Parser '{parserName}' doesn't exist in local dictionary");
                return false;
            }

            // Check if parser exist in monitoring table
            var existsInDatabase = parserMonitoringContext.ParserMonitorings
                .Any(x => x.Name.Equals(_configuration.ParserMonitoringNames[parserName]));
            if (!existsInDatabase)
            {
                _logger.Warn($"Parser '{parserName}' doesn't exist in database");
                return false;
            }

            // Check if parser class exists
            var existClass = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Any(x => x.Name.Equals(parserName + "Parser"));
            if (!existClass)
            {
                _logger.Warn($"Parser '{parserName}' doesn't exist in current context");
                return false;
            }

            // Check if parser exist in configuration
            var existConfiguration = _configuration.Parsers
                .Any(x => x.Name.Equals(parserName));
            if (!existConfiguration)
            {
                _logger.Warn($"There is no configuration for '{parserName}' parser");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check if parser available to use based on command line arguments
        /// </summary>
        /// <param name="parserName">Parser name</param>
        /// <returns>Boolean</returns>
        private bool CheckState(string parserName)
        {
            // Skip all check activities if 'force' flag determined
            if (_options.Force)
                return true;

            using var parserMonitoringContext = new ParserMonitoringContext();

            // Check 'active' field in monitoring table
            var isActive = parserMonitoringContext.ParserMonitorings
                .FirstOrDefault(x =>
                    x.Active
                    && x.Name.Equals(_configuration.ParserMonitoringNames[parserName]
                    )) != null;

            if (!isActive)
            {
                _logger.Warn($"Parser '{_configuration.ParserMonitoringNames[parserName]}' is not active");
                return false;
            }

            // Skip next check activities if 'ignore' flag determined
            if (_options.Ignore) return true;

            // Check 'parsed' field in monitoring table
            var isParsed = parserMonitoringContext.ParserMonitorings
                .FirstOrDefault(x =>
                    x.Parsed
                    && x.Name.Equals(_configuration.ParserMonitoringNames[parserName]
                    )) != null;

            if (isParsed)
            {
                _logger.Warn($"Parser '{_configuration.ParserMonitoringNames[parserName]}' hasn't been migrated yet");
                return false;
            }

            return true;
        }
    }
}