using NLog;
using System.Linq;
using System.Threading.Tasks;
using GoszakupParser.Services;

// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo

namespace GoszakupParser
{
    /// @author Yevgeniy Cherdantsev
    /// @date 27.10.2020 17:43:24
    /// <summary>
    /// Class that contains actions
    /// </summary>
    public static class Actions
    {
        /// <summary>
        /// Executes parsing activity
        /// </summary>
        /// <param name="options">Parsing options</param>
        public static async Task Parse(CommandLineOptions.Parse options)
        {
            // Start parsing using given arguments
            var parserService = new ParserService(options);
            await parserService.StartService();
        }

        /// <summary>
        /// Executes downloading activity
        /// </summary>
        /// <param name="options">Download options</param>
        public static async Task Download(CommandLineOptions.Download options)
        {
            // Start downloading using given arguments
            var downloaderService = new DownloaderService(options);
            await downloaderService.StartService();
        }

        /// <summary>
        /// Changes policies based on CLI input arguments
        /// </summary>
        /// <param name="options">GeneralOptions object that contains data about logger policies</param>
        public static void LoggerPoliciesInstallation(CommandLineOptions.GeneralOptions options)
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
        }
    }
}