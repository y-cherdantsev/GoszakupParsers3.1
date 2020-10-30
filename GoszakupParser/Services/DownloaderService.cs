using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using GoszakupParser.Downloaders;

// ReSharper disable IdentifierTypo

namespace GoszakupParser.Services
{
    /// <inheritdoc />
    public class DownloaderService : GoszakupService
    {
        /// <summary>
        /// Command line options
        /// </summary>
        private readonly CommandLineOptions.Download _options;

        /// <summary>
        /// Constructor of DownloaderService class
        /// </summary>
        /// <param name="options">Parsed command line options</param>
        /// <returns></returns>
        public DownloaderService(CommandLineOptions.Download options)
        {
            _options = options;
        }

        /// <inheritdoc />
        public override async Task StartService()
        {
            Logger.Info("Starting downloading service!");

            // Trying to start each defined downloader sequentially in defined order
            foreach (var downloaderName in _options.Downloaders)
            {
                try
                {
                    if (Exists(downloaderName))
                        await ProceedDownloading(downloaderName);
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                }
            }
        }

        /// <summary>
        /// Generating object of given downloader and starts it
        /// </summary>
        /// <param name="downloaderName">Downloader name</param>
        private async Task ProceedDownloading(string downloaderName)
        {
            // Get class for parser
            var downloadingClass = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .FirstOrDefault(t => t.Name == downloaderName + "Downloader");

            // Get downloader configuration
            var downloaderConfiguration = Configuration.Downloaders.FirstOrDefault(x => x.Name.Equals(downloaderName));

            // Redefine number of threads if needed
            if (_options.Threads != 0)
                downloaderConfiguration!.Threads = _options.Threads;
            
            // Redefine folder if needed
            if (_options.Folder != null)
                downloaderConfiguration!.Folder = _options.Folder;

            // Initializing and starting downloader

            // Generating list of arguments for a current downloader
            var args = new List<object>();

            // ReSharper disable once PossibleNullReferenceException
            foreach (var parameterInfo in downloadingClass.GetConstructors()[0].GetParameters())
            {
                switch (parameterInfo.Name)
                {
                    case "downloaderSettings":
                        args.Add(downloaderConfiguration);
                        break;
                }
            }

            // ReSharper disable once PossibleNullReferenceException (Already checked)
            // Creating instance of a downloader
            var downloader = (Downloader) Activator.CreateInstance(downloadingClass, args.ToArray());

            if (downloader != null) await downloader.DownloadAsync();
        }


        /// <summary>
        /// Check if downloader fully integrated in downloading system
        /// </summary>
        /// <param name="downloaderName">Downloader name</param>
        /// <returns>Boolean</returns>
        private bool Exists(string downloaderName)
        {
            // Check if parser class exists
            var existClass = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Any(x => x.Name.Equals(downloaderName + "Downloader"));
            if (!existClass)
            {
                Logger.Warn($"Downloader '{downloaderName}' doesn't exist in current context");
                return false;
            }

            // Check if parser exist in configuration
            var existConfiguration = Configuration.Downloaders
                .Any(x => x.Name.Equals(downloaderName));
            if (existConfiguration) return true;
            Logger.Warn($"There is no configuration for '{downloaderName}' downloader");
            return false;

        }
    }
}