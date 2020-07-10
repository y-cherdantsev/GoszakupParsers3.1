using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using NLog;

// ReSharper disable CommentTypo

// ReSharper disable once IdentifierTypo
namespace GoszakupParser.Parsers
{
    /// @author Yevgeniy Cherdantsev
    /// @date 25.02.2020 10:53:43
    /// <summary>
    /// Parent parsing class for creating parser based on this class
    /// </summary>
    public abstract class Parser
    {
        /// <summary>
        /// Logger used by parser
        /// </summary>
        protected readonly Logger Logger;

        /// <summary>
        /// URL used by parser, which contains next request link
        /// </summary>
        protected string Url { get; set; }

        /// <summary>
        /// Proxy for sending requests
        /// </summary>
        protected WebProxy Proxy { get; }

        /// <summary>
        /// Number of threads used by parser
        /// </summary>
        protected int Threads { get; }

        /// <summary>
        /// Lock used to prevent fragmentation and implement atomic operations between several threads
        /// </summary>
        protected readonly object Lock = new object();

        /// <summary>
        /// Base constructor of each parser
        /// </summary>
        /// <param name="parserSettings">Parser settings from a configuration</param>
        /// <param name="proxy">Parsing proxy</param>
        protected Parser(Configuration.ParserSettings parserSettings, WebProxy proxy)
        {
            // Initializes logger of derived class using overrode function
            Logger = LogManager.GetLogger(GetType().Name, GetType());
            Proxy = proxy;
            Threads = parserSettings.Threads;
            Url = parserSettings.Url;

            // ReSharper disable once StringLiteralTypo
            Console.Title = $"Goszakup Parser: '{GetType().Name}'";
        }

        /// <summary>
        /// Starts parsing
        /// </summary>
        public abstract Task ParseAsync();

        /// <summary>
        /// Gets part of list of strings for the given thread
        /// </summary>
        /// <param name="list">List that should be divided</param>
        /// <param name="threadNumber">Number of current thread</param>
        /// <returns>Array of elements</returns>
        protected string[] DivideList(IEnumerable<string> list, int threadNumber)
        {
            /*
             * Splitting based on counting ASCII representations of all characters and counting modulus with Threads numbers
             * Lists might not be fully equal to each other by count, but it's easy and effective method of dividing of large lists
             */
            return list.Where(
                x => System.Text.Encoding.ASCII.GetBytes(x).Sum(Convert.ToInt32) % Threads == threadNumber).ToArray();
        }

        /// <summary>
        /// Gets part of list of objects for the given thread
        /// </summary>
        /// <param name="list">List that should be divided</param>
        /// <param name="threadNumber">Number of current thread</param>
        /// <returns>Array of elements</returns>
        protected object[] DivideList(IEnumerable<object> list, int threadNumber)
        {
            /*
             * Splitting based on index of an object in list
             */
            var enumerable = list.ToList();
            return enumerable.Where(x => enumerable.IndexOf(x) % Threads == threadNumber).ToArray();
        }
    }
}