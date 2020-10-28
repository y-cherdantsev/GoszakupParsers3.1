// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
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
            
            // ReSharper disable once VirtualMemberCallInConstructor
            Total = GetTotal();
            
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
        /// Saves file in defined folder
        /// </summary>
        /// <param name="link">Link to the file</param>
        /// <param name="folderPath">Path to the storage folder</param>
        /// <param name="name">Name of the file</param>
        /// <param name="proxy">WebProxy that will be used while downloading file</param>
        protected async Task SaveFileAsync(string link, string folderPath, string name, WebProxy proxy = null)
        {
            var fullName = Path.Combine(folderPath, name);

            var client = proxy == null ? new HttpClient() : new HttpClient(new HttpClientHandler {UseProxy = true, Proxy = proxy});
            
            for (var i = 0; i < 10; ++i)
            {
                var file = new FileInfo(fullName);
                if (file.Exists && file.Length >= 10000) break;

                if (file.Exists)
                    try
                    {
                        file.Delete();
                    }
                    catch (Exception)
                    {
                        // ignored
                    }

                var result = await client.GetAsync(link);
                await using Stream contentStream =
                        await result.Content.ReadAsStreamAsync(),
                    stream = new FileStream(fullName, FileMode.Create, FileAccess.Write, FileShare.None,
                        4000000, true);
                await contentStream.CopyToAsync(stream);
                await contentStream.FlushAsync();
                contentStream.Close();
            }
        }

        /// <summary>
        /// Starts downloading
        /// </summary>
        public abstract Task DownloadAsync();

        /// <summary>
        /// Determines the message that will be showed after each iteration
        /// </summary>
        /// <returns>string - message</returns>
        protected virtual string LogMessage(object obj = null) => $"Left:[{Total}]";
    }
}