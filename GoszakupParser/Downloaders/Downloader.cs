using NLog;
using System;
using RestSharp;
using System.IO;
using System.Net;
using System.Linq;
using System.Data;
using GoszakupParser.Models;
using System.Threading.Tasks;
using GoszakupParser.Contexts;

// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo

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
        /// Folder where downloaded files will be stored
        /// </summary>
        protected string Folder { get; }

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
            Folder = downloaderSettings.Folder;

            // Load proxies for downloader
            var downloaderMonitoringContext = new GeneralContext<Proxy>(Configuration.ParsingDbConnectionString);
            var proxiesDto = downloaderMonitoringContext.Models.Where(x => x.Status == true).ToList();
            Proxies = proxiesDto.Select(proxy => new WebProxy(proxy.Address.ToString(), proxy.Port)
                    {Credentials = new NetworkCredential(proxy.Username, proxy.Password)})
                .OrderBy(x => new Random().NextDouble())
                //todo(Blocked IP addresses)
                .Where(x =>
                    x.Address.Host != "185.120.77.111" &&
                    x.Address.Host != "185.120.77.113" &&
                    x.Address.Host != "185.120.77.246"
                )
                .ToArray();
            if (Proxies.Length == 0)
                throw new NoNullAllowedException("No proxies has been found in the system");
            downloaderMonitoringContext.Dispose();


            // ReSharper disable once StringLiteralTypo
            Configuration.Title = $"Goszakup Downloader - {GetType().Name}";
        }

        /// <summary>
        /// Saves file in defined folder
        /// </summary>
        /// <param name="link">Link to the file</param>
        /// <param name="folderPath">Path to the storage folder</param>
        /// <param name="name">Name of the file</param>
        /// <param name="proxy">WebProxy that will be used while downloading file</param>
        protected async Task SaveFileAsync(string link, string folderPath, string name, WebProxy proxy = null)
        {
            var fullName = Path.Combine(folderPath, name);

            var client = proxy == null ? new RestClient() : new RestClient {Proxy = proxy};
            for (var i = 0; i < 10; ++i)
            {
                var file = new FileInfo(fullName);
                if (file.Exists && file.Length >= 10000) return;

                if (file.Exists)
                    try
                    {
                        file.Delete();
                    }
                    catch (Exception)
                    {
                        // ignored
                    }

                try
                {
                    var response = await client.ExecuteAsync(new RestRequest(link, Method.GET));
                    if (response.StatusCode == HttpStatusCode.Forbidden)
                        throw new Exception($"Forbidden: '{link}'; Proxy: '{proxy!.Address}'");

                    await File.WriteAllBytesAsync(fullName, response.RawBytes);
                }
                catch (Exception e)
                {
                    Logger.Warn(e.Message);
                    await Task.Delay(5000);
                }
            }

            throw new Exception("File hasn't been loaded after 10 tries");
        }

        /// <summary>
        /// Starts downloading
        /// </summary>
        public abstract Task DownloadAsync();

        /// <summary>
        /// Determines the message that will be showed after each iteration
        /// </summary>
        /// <returns>string - message</returns>
        // ReSharper disable once UnusedParameter.Global
        protected string LogMessage(object obj = null) => $"Left:[{Total}]";
    }
}