﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Threading;
using System.Threading.Tasks;
using GoszakupParser.Contexts;
using GoszakupParser.Models;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace GoszakupParser.Parsers.WebParsers
{
    /// @author Yevgeniy Cherdantsev
    /// @date 28.02.2020 19:47:21
    /// @version 1.0
    /// <summary>
    /// INPUT
    /// </summary>
    public abstract class WebParser<TModel> : Parser where TModel : DbLoggerCategory.Model
    {
        protected Dictionary<string, string> Aims { get; set; }
        private int Total { get; set; } 

        protected WebParser(Configuration.ParserSettings parserSettings) : base(parserSettings)
        {
            Aims = LoadAims();
            Total = Aims.Count;
        }

        protected abstract Dictionary<string, string> LoadAims();

        protected abstract override Logger InitLogger();

        protected async Task ProcessObject(TModel model, ParserContext<TModel> context)
        {
            context.Models.Add(model);
            await context.SaveChangesAsync();
            lock (Lock)
            {
                Logger.Trace($"Left: {--Total}");
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

        public abstract string GenerateUrl(string aim);

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
                Console.WriteLine("Requesting");
                var response = request.GetResponse();
                Console.WriteLine("Got response");
                var objReader =
                    new StreamReader(response.GetResponseStream() ?? throw new NullReferenceException());

                var sLine = "";

                while (sLine != null)
                {
                    sLine = objReader.ReadLine();
                    if (sLine != null)
                        pageResponse += sLine;
                }

                return pageResponse;
            }
            catch (Exception)
            {
                // Console.WriteLine(e.StackTrace);
                return null;
            }
        }
        
        private string[] DivideList(List<string> list, int i)
        {
            return list.Where(x => int.Parse(x) % Threads == i).ToArray();
        }
    }
}