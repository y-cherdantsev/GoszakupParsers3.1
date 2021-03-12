using System;
using System.Linq;
using GoszakupParser.Models;
using System.Threading.Tasks;
using GoszakupParser.Contexts;
using System.Collections.Generic;
using Parser = GoszakupParser.Parsers.Parser;

// ReSharper disable InvertIf
// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo

namespace GoszakupParser.Services
{
    /// <inheritdoc />
    public class ParserService : GoszakupService
    {
        /// <summary>
        /// Command line options
        /// </summary>
        private readonly CommandLineOptions.Parse _options;

        /// <summary>
        /// Constructor of ParserService class
        /// </summary>
        /// <param name="options">Parsed command line options</param>
        /// <returns></returns>
        public ParserService(CommandLineOptions.Parse options)
        {
            _options = options;
        }

        /// <inheritdoc />
        public override async Task StartService()
        {
            Logger.Info("Starting parsing service!");

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
                    Logger.Error(e);
                    await Rollback(parserName);
                }
            }
        }

        /// <summary>
        /// Generating object of given parser and starts it
        /// </summary>
        /// <param name="parserName">Parser name</param>
        private async Task ProceedParsing(string parserName)
        {
            // Get class for parser
            var parsingClass = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .FirstOrDefault(t => t.Name == parserName + "Parser");

            // Get parser configuration
            var parserConfiguration = Configuration.Parsers.FirstOrDefault(x => x.Name.Equals(parserName));

            // Redefine number of threads if needed
            if (_options.Threads != 0)
                parserConfiguration!.Threads = _options.Threads;

            // Initializing and starting parser

            // Generating list of arguments for a current parser
            var args = new List<object>();

            // ReSharper disable once PossibleNullReferenceException
            foreach (var parameterInfo in parsingClass.GetConstructors()[0].GetParameters())
            {
                switch (parameterInfo.Name)
                {
                    case "parserSettings":
                        args.Add(parserConfiguration);
                        break;
                }
            }

            // ReSharper disable once PossibleNullReferenceException (Already checked)
            // Creating instance of a parser
            var parser = (Parser) Activator.CreateInstance(parsingClass, args.ToArray());

            // Changes 'parsed' field value in monitoring table to null
            await ChangeParsedField(parserName, null);
            if (parser != null) await parser.ParseAsync();
            else
            {
                await ChangeParsedField(parserName, false);
                throw new NullReferenceException($"{parserName} hasn't been created");
            }

            // Changes 'parsed' field value in monitoring table to true
            await ChangeParsedField(parserName, true);
        }

        /// <summary>
        /// Truncating tables and setting parsed field to 'false'
        /// </summary>
        /// <param name="parserName">Parser name</param>
        private async Task Rollback(string parserName)
        {
            await ChangeParsedField(parserName, false);

            // Get class for parser
            var parsingClass = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .FirstOrDefault(t => t.Name == parserName + "Parser");

            // Get parser configuration
            var parserConfiguration = Configuration.Parsers.FirstOrDefault(x => x.Name.Equals(parserName));

            // Generating list of arguments for a current parser
            var args = new List<object>();

            // ReSharper disable once PossibleNullReferenceException
            foreach (var parameterInfo in parsingClass.GetConstructors()[0].GetParameters())
                switch (parameterInfo.Name)
                {
                    case "parserSettings":
                        args.Add(parserConfiguration);
                        break;
                }


            // ReSharper disable once PossibleNullReferenceException (Already checked)
            // Creating instance of a parser and truncating its tables
            var parser = (Parser) Activator.CreateInstance(parsingClass, args.ToArray());

            await parser!.TruncateParsingTables();
            Logger.Info("Changes has been rolled back");
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
            using var parserMonitoringContext =
                new GeneralContext<ParserMonitoring>(Configuration.ParsingDbConnectionString);

            // Check if parser exist in map dictionary
            var existInDictionary = Configuration.ParserMonitoringMappings.Keys
                .Any(x => x.Equals(parserName));
            if (!existInDictionary)
            {
                Logger.Warn($"Parser '{parserName}' doesn't exist in local dictionary");
                return false;
            }

            // Check if parser exist in monitoring table
            var existsInDatabase = parserMonitoringContext.Models
                .Any(x => x.Name.Equals(Configuration.ParserMonitoringMappings[parserName]));
            if (!existsInDatabase)
            {
                Logger.Warn($"Parser '{parserName}' doesn't exist in database");
                return false;
            }

            // Check if parser class exists
            var existClass = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Any(x => x.Name.Equals(parserName + "Parser"));
            if (!existClass)
            {
                Logger.Warn($"Parser '{parserName}' doesn't exist in current context");
                return false;
            }

            // Check if parser exist in configuration
            var existConfiguration = Configuration.Parsers
                .Any(x => x.Name.Equals(parserName));
            if (!existConfiguration)
            {
                Logger.Warn($"There is no configuration for '{parserName}' parser");
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

            using var parserMonitoringContext =
                new GeneralContext<ParserMonitoring>(Configuration.ParsingDbConnectionString);

            // Check 'active' field in monitoring table
            var isActive = parserMonitoringContext.Models
                .FirstOrDefault(x =>
                    x.Name.Equals(Configuration.ParserMonitoringMappings[parserName]
                    ) && x.Active) != null;

            if (!isActive)
            {
                Logger.Warn($"Parser '{Configuration.ParserMonitoringMappings[parserName]}' is not active");
                return false;
            }

            // Skip next check activities if 'ignore' flag determined
            if (_options.Ignore) return true;

            // Check 'parsed' field in monitoring table
            var isParsed = parserMonitoringContext.Models
                .FirstOrDefault(x => x.Name.Equals(Configuration.ParserMonitoringMappings[parserName]))?.Parsed;

            switch (isParsed)
            {
                case true:
                    Logger.Warn(
                        $"Parser '{Configuration.ParserMonitoringMappings[parserName]}' hasn't been migrated yet");
                    return false;
                case null:
                    Logger.Warn($"Parser '{Configuration.ParserMonitoringMappings[parserName]}' now proceeding");
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Changes value of 'parsed' field
        /// </summary>
        /// <param name="parserName">Parser name</param>
        /// <param name="flag">Value</param>
        private async Task ChangeParsedField(string parserName, bool? flag)
        {
            // Update data in monitoring table
            var parserMonitoringContext = new GeneralContext<ParserMonitoring>(Configuration.ParsingDbConnectionString);
            var parsed =
                parserMonitoringContext.Models.FirstOrDefault(x =>
                    x.Name.Equals(Configuration.ParserMonitoringMappings[parserName]));

            // ReSharper disable PossibleNullReferenceException
            // Possible reason of null: field has been deleted from DB table while parsing has been proceeding
            if (flag == true)
                parsed.LastParsed = DateTime.Now;
            parsed.Parsed = flag;
            // ReSharper restore PossibleNullReferenceException
            parserMonitoringContext.Models.Update(parsed);
            await parserMonitoringContext.SaveChangesAsync();
            Logger.Info($"{Configuration.ParserMonitoringMappings[parserName]} 'parsed' field now equals to '{flag}'"
                .Replace("''", "'null'"));
            await parserMonitoringContext.DisposeAsync();
        }
    }
}