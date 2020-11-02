using System;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;

// ReSharper disable IdentifierTypo

namespace GoszakupParser.Downloaders.AimDownloaders
{
    public abstract class AimDownloader : Downloader
    {
        private readonly List<DownloadAim> _aims;

        /// <summary>
        /// Generates object of given aim downloader
        /// </summary>
        /// <param name="downloaderSettings">Downloader settings from configuration</param>
        protected AimDownloader(Configuration.DownloaderSettings downloaderSettings) : base(downloaderSettings)
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            _aims = LoadAims();
            Total = _aims.Count;
            Logger.Info($"{Total} aims has been found");
        }

        /// <summary>
        /// Loads list of aims for downloading
        /// </summary>
        /// <returns>List of aims</returns>
        protected abstract List<DownloadAim> LoadAims();

        /// <summary>
        /// Generates file name from aim
        /// </summary>
        /// <param name="aim">Aim for downloading</param>
        /// <returns>File name</returns>
        protected abstract string GenerateFileName(DownloadAim aim);

        /// <summary>
        /// Saves path to file into Db
        /// </summary>
        /// <param name="aim">Aim for downloading</param>
        protected abstract Task SavePathToDb(DownloadAim aim);

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
        private async Task ProceedAim(DownloadAim aim, WebProxy proxy = null)
        {
            var fileName = GenerateFileName(aim);
            try
            {
                await SaveFileAsync(aim.Link, Folder, fileName, proxy);
                await SavePathToDb(aim);
                await Task.Delay(500);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
            finally
            {
                lock (Lock)
                {
                    --Total;
                    Logger.Trace(LogMessage());
                }
            }
        }
    }

    /// <summary>
    /// Class for creating aim objects
    /// </summary>
    public class DownloadAim
    {
        public long Id { get; set; }
        public string Link { get; set; }
        public string Name { get; set; }
    }
}