using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

// ReSharper disable IdentifierTypo

namespace GoszakupParser.Downloaders.AimDownloaders
{
    public abstract class AimDownloader <TAim> : Downloader
    {
        private readonly List<TAim> _aims;
        
        /// <summary>
        /// Generates object of given aim downloader
        /// </summary>
        /// <param name="downloaderSettings">Downloader settings from configuration</param>
        protected AimDownloader(Configuration.DownloaderSettings downloaderSettings) : base(downloaderSettings)
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            _aims = LoadAims();
            Total = _aims.Count;
        }

        /// <summary>
        /// Loads list of aims for downloading
        /// </summary>
        /// <returns>List of aims</returns>
        protected abstract List<TAim> LoadAims();

        /// <summary>
        /// Generates file name from aim
        /// </summary>
        /// <param name="aim">Aim for downloading</param>
        /// <returns>File name</returns>
        protected abstract string GenerateFileName(TAim aim);

        /// <summary>
        /// Gets link from aim
        /// </summary>
        /// <param name="aim">Aim for downloading</param>
        /// <returns>Link to the file</returns>
        protected abstract string GetLink(TAim aim);

        /// <summary>
        /// Saves path to file into Db
        /// </summary>
        /// <param name="aim">Aim for downloading</param>
        protected abstract Task SavePathToDb(TAim aim);
        
        /// <inheritdoc />
        public override async Task DownloadAsync()
        {
            Logger.Info("Starting Downloading");
            var tasks = new List<Task>();
            var proxies = Proxies.GetEnumerator();
            foreach (var aim in _aims)
            {
                if (!proxies.MoveNext())
                {
                    proxies.Reset();
                    proxies.MoveNext();
                }
                tasks.Add(ProceedAim(aim, proxies.Current as WebProxy));
                if (tasks.Count < Threads) continue;
                await Task.WhenAny(tasks);
                tasks.RemoveAll(x => x.IsCompleted);
            }

            await Task.WhenAll(tasks);
            
            Logger.Info("End Of Downloading");
        }

        /// <summary>
        /// Proceeding aim
        /// </summary>
        /// <param name="aim">Aim for download</param>
        /// <param name="proxy">WebProxy if needed</param>
        private async Task ProceedAim(TAim aim, WebProxy proxy = null)
        {
            Logger.Info(LogMessage());
            var fileName = GenerateFileName(aim);
            var link = GetLink(aim);
            try
            {
                await SaveFileAsync(link, Folder,  fileName, proxy);
                await SavePathToDb(aim);
                lock (Lock)
                {
                    --Total; 
                }
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }
        }
    }
}