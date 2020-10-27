using System.Linq;
using System.Threading.Tasks;
using NLog;

namespace GoszakupParser
{
    public class Actions
    {
        public static async Task<int> Parse(CommandLineOptions.Parse options)
        {
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
            var parserService = new ParserService(options);
            await parserService.StartParsingService();
            return 0;
        }

        public static async Task<int> Download(CommandLineOptions.Download options)
        {
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

            await Task.Delay(1);
            return 1;
        }
    }
}