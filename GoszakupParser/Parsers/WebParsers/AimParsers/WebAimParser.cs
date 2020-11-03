using System.Net;
using System.Linq;
using GoszakupParser.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable CommentTypo

// ReSharper disable once IdentifierTypo
namespace GoszakupParser.Parsers.WebParsers.AimParsers
{
    /// @author Yevgeniy Cherdantsev
    /// @date 28.02.2020 19:47:21
    /// <summary>
    /// Parent parser used for creating parsers that gets information from web by aim elements
    /// </summary>
    /// <typeparam name="TModel">Model that will be parsed and inserted into DB</typeparam>
    public abstract class WebAimParser<TModel> : WebParser<TModel> where TModel : BaseModel, new()
    {
        /// <summary>
        /// Aims that's used for parsing
        /// </summary>
        protected Dictionary<string, string> Aims { get; }

        /// <inheritdoc />
        protected WebAimParser(Configuration.ParserSettings parserSettings) : base(parserSettings)
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            Aims = LoadAims();
            Total = Aims.Count;
        }

        /// <summary>
        /// Loads aims from the (Mostly from DB, but also can be file or smt. similar);
        /// </summary>
        /// <returns>List of aims</returns>
        protected abstract Dictionary<string, string> LoadAims();

        /// <inheritdoc />
        [SuppressMessage("ReSharper.DPA", "DPA0003: Excessive memory allocations in LOH")]
        public override async Task ParseAsync()
        {
            Logger.Info("Starting Parsing");
            var tasks = new List<Task>();

            var proxies = Proxies.GetEnumerator();

            foreach (var aim in Aims.Keys)
            {
                if (!proxies.MoveNext())
                {
                    proxies.Reset();
                    proxies.MoveNext();
                }

                tasks.Add(ParseAim(aim, proxies.Current as WebProxy));
                if (tasks.Count < Threads) continue;
                foreach (var task in tasks.Where(x => x.IsFaulted))
                    Logger.Warn(task.Exception, task.Exception?.Message);
                await Task.WhenAny(tasks);
                tasks.RemoveAll(x => x.IsCompleted);
            }

            await Task.WhenAll(tasks);

            Logger.Info("End Of Parsing");
        }

        /// <summary>
        /// Parses given list of aims
        /// </summary>
        /// <param name="aim">Aim</param>
        /// <param name="webProxy">Parsing proxy</param>
        protected abstract Task ParseAim(string aim, WebProxy webProxy);
    }
}