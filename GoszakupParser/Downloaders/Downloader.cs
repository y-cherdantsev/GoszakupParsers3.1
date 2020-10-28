// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GoszakupParser.Contexts;
using GoszakupParser.Models;
using NLog;

namespace GoszakupParser.Downloaders
{
    /// @author Yevgeniy Cherdantsev
    /// @date 25.02.2020 10:53:43
    /// <summary>
    /// Parent downloader class for creating downloader based on this class
    /// </summary>
    public abstract class Downloader
    {
        /// <summary>
        /// Logger used by downloader
        /// </summary>
        protected readonly Logger Logger;

        /// <summary>
        /// Total number of elements that should be parsed (decreasing with each iteration)
        /// </summary>
        protected int Total { get; set; }

        /// <summary>
        /// Number of threads used by downloader
        /// </summary>
        protected int Threads { get; }

        /// <summary>
        /// Proxies for sending requests
        /// </summary>
        protected readonly WebProxy[] Proxies;

        /// <summary>
        /// Lock used to prevent fragmentation and implement atomic operations between several threads
        /// </summary>
        protected readonly object Lock = new object();

        /// <summary>
        /// Base constructor of each downloader
        /// </summary>
        /// <param name="downloaderSettings">Downloader settings from a configuration</param>
        protected Downloader(Configuration.DownloaderSettings downloaderSettings)
        {
            // Initializes logger of derived class using overrode function
            Logger = LogManager.GetLogger(GetType().Name, GetType());
            Threads = downloaderSettings.Threads;

            // Load proxies for downloader
            var downloaderMonitoringContext = new GeneralContext<Proxy>(Configuration.ParsingDbConnectionString);
            var proxiesDto = downloaderMonitoringContext.Models.Where(x => x.Status == true).ToList();
            Proxies = proxiesDto.Select(proxy => new WebProxy(proxy.Address.ToString(), proxy.Port)
                    {Credentials = new NetworkCredential(proxy.Username, proxy.Password)})
                .OrderBy(x => new Random().NextDouble()).ToArray();
            if (Proxies.Length == 0)
                throw new NoNullAllowedException("No proxies has been found in the system");
            downloaderMonitoringContext.Dispose();

            // ReSharper disable once StringLiteralTypo
            Configuration.Title = $"Goszakup Downloader - {GetType().Name}";
        }

        /// <summary>
        /// Starts parsing
        /// </summary>
        public abstract Task ParseAsync();

        /// <summary>
        /// Gets part of list of strings for the given thread
        /// </summary>
        /// <code>
        /// Splitting based on counting ASCII representations of all characters and counting modulus with Threads numbers
        /// Lists might not be fully equal to each other by count, but it's easy and effective method of dividing of large lists
        /// </code>
        /// <param name="list">List that should be divided</param>
        /// <param name="threadNumber">Number of current thread</param>
        /// <returns>Array of elements</returns>
        protected IEnumerable<string> DivideList(IEnumerable<string> list, int threadNumber) => list.Where(
            x => System.Text.Encoding.ASCII.GetBytes(x).Sum(Convert.ToInt32) % Threads == threadNumber).ToArray();

        /// <summary>
        /// Determines when the parser should be stopped
        /// </summary>
        /// <param name="checkElement">Element that will determine if parsing should be stopped</param>
        /// <returns>True, if parser should be stopped</returns>
        protected virtual bool StopCondition(object checkElement) => false;

        /// <summary>
        /// Determines the message that will be showed after each iteration
        /// </summary>
        /// <returns>string - message</returns>
        protected virtual string LogMessage(object obj = null) => $"Left:[{Total}]";
    }
}