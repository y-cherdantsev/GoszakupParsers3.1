using System.Net;
using System.Linq;
using GoszakupParser.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

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
        public override async Task ParseAsync()
        {
            Logger.Info("Starting Parsing");
            var tasks = new List<Task>();

            var proxies = Proxies.GetEnumerator();
            for (var i = 0; i < Threads; i++)
            {
                var ls = DivideList(Aims.Keys.ToList(), i);
                if (!proxies.MoveNext())
                {
                    proxies.Reset();
                    proxies.MoveNext();
                }

                tasks.Add(ParseArray(ls, proxies.Current as WebProxy));
            }

            await Task.WhenAll(tasks);
            Logger.Info("End Of Parsing");
        }

        /// <summary>
        /// Parses given list of aims
        /// </summary>
        /// <param name="list">List of aims</param>
        /// <param name="webProxy">Parsing proxy</param>
        protected abstract Task ParseArray(IEnumerable<string> list, WebProxy webProxy);
    }
}