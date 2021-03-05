using System;
using System.Net;
using System.Linq;
using GoszakupParser.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo

namespace GoszakupParser.Parsers.WebParsers.SequentialParsers
{
    /// @author Yevgeniy Cherdantsev
    /// @date 05.03.2020 16:51:32
    /// <summary>
    /// Parent parser used for creating parsers that gets information from web by aim elements
    /// </summary>
    /// <typeparam name="TModel">Model that will be parsed and inserted into DB</typeparam>
    public abstract class WebSequentialParser<TModel> : WebParser<TModel> where TModel : BaseModel, new()
    {
        /// <inheritdoc />
        protected WebSequentialParser(Configuration.ParserSettings parserSettings) : base(parserSettings)
        {
        }

        [SuppressMessage("ReSharper.DPA", "DPA0001: Memory allocation issues")]
        public override async Task ParseAsync()
        {
            Logger.Info("Started Parsing");

            var page = 1;
            var tasks = new List<Task>();
            var proxies = Proxies.GetEnumerator();
            while (true)
            {
                if (!proxies.MoveNext())
                {
                    proxies.Reset();
                    proxies.MoveNext();
                }

                if (tasks.Count < Threads)
                {
                    tasks.Add(ParsePage(page++, proxies.Current as WebProxy));
                    continue;
                }

                await Task.WhenAny(tasks);
                var faultedTasks = tasks.Where(x => x.IsFaulted).ToList();
                if (faultedTasks.Any(x => x.Exception!.Message.Contains("Page is empty")))
                    break;

                foreach (var faultedTask in faultedTasks)
                    Logger.Error(faultedTask.Exception, faultedTask.Exception!.Message);

                if (faultedTasks.Count > 0)
                    throw new AggregateException($"{faultedTasks.Count} errors appeared");

                tasks.RemoveAll(x => x.IsCompleted);
            }

            tasks.RemoveAll(x => x.IsFaulted && x.Exception!.Message.Contains("Page is empty"));
            await Task.WhenAll(tasks);
            var faulted = tasks.Where(x => x.IsFaulted).ToList();
            foreach (var faultedTask in faulted)
                Logger.Error(faultedTask.Exception, faultedTask.Exception!.Message);
            if (faulted.Count > 0)
                throw new AggregateException($"{faulted.Count} errors appeared");


            Logger.Info("End Of Parsing");
        }

        /// <summary>
        /// Parses given page
        /// </summary>
        /// <param name="page">Page number that will be parsed</param>
        /// <param name="proxy">Proxy for parsing</param>
        protected abstract Task ParsePage(int page, WebProxy proxy);
    }
}