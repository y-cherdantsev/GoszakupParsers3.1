using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Threading.Tasks;
using GoszakupParser.Contexts;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace GoszakupParser.Parsers.WebParsers.AimParsers
{
    /// @author Yevgeniy Cherdantsev
    /// @date 28.02.2020 19:47:21
    /// @version 1.0
    /// <summary>
    /// INPUT
    /// </summary>
    public abstract class WebAimParser<TModel> : Parser where TModel : DbLoggerCategory.Model
    {
        protected Dictionary<string, string> Aims { get; }
        private int Total { get; set; } 

        protected WebAimParser(Configuration.ParserSettings parserSettings, WebProxy proxy) : base(parserSettings, proxy)
        {
            Aims = LoadAims();
            Total = Aims.Count;
        }

        protected abstract Dictionary<string, string> LoadAims();

        /// <summary>
        /// Inserts model into DB
        /// </summary>
        /// <param name="model">Model</param>
        /// <param name="context">Parsing DB context</param>
        protected async Task ProcessObject(TModel model, ParserContext<TModel> context)
        {
            try
            {
                context.Models.Add(model);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException is NpgsqlException)
                    Logger.Trace($"Message: {e.InnerException?.Data["MessageText"]}; " +
                                 $"{e.InnerException?.Data["Detail"]} " +
                                 $"{e.InnerException?.Data["SchemaName"]}.{e.InnerException?.Data["TableName"]}");
                else
                    throw;
            }
            
        }

        protected abstract Task ParseArray(string[] list, WebProxy webProxy);

        public override async Task ParseAsync()
        {
            Logger.Info("Starting Parsing");
            var tasks = new List<Task>();
            using var context = new ParserMonitoringContext();
            var proxies = context.Proxies.Where(x => x.Status==true).ToList();
            for (var i = 0; i < Threads; i++)
            {
                var ls = DivideList(Aims.Keys.ToList(), i);
                var proxy = new WebProxy(proxies[i].Address.ToString(), proxies[i].Port);
                proxy.Credentials = new NetworkCredential(proxies[i].Username, proxies[i].Password);
                tasks.Add(ParseArray(ls, proxy));
            }

            await Task.WhenAll(tasks);
            Logger.Info("End Of Parsing");
        }

        protected string GetPage(string url, WebProxy proxy)
        {
            var pageResponse = "";
            try
            {
                var request = WebRequest.Create(url);
                request.Method = WebRequestMethods.Http.Get;
                request.AuthenticationLevel = AuthenticationLevel.None;
                request.Proxy = proxy;
                request.Timeout = 10000;
                var response = request.GetResponse();
                var objReader =
                    new StreamReader(response.GetResponseStream() ?? throw new NullReferenceException());

                var sLine = "";

                while (sLine != null)
                {
                    sLine = objReader.ReadLine();
                    if (sLine != null)
                        pageResponse += sLine;
                }
                lock (Lock)
                {
                    Logger.Trace($"Left: {--Total}");
                }
                return pageResponse;
            }
            catch (Exception)
            {
                // Console.WriteLine(e.StackTrace);
                return null;
            }
        }
    }
}