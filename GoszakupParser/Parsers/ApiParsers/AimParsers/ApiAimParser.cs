using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GoszakupParser.Contexts;
using GoszakupParser.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RestSharp;

namespace GoszakupParser.Parsers.ApiParsers.AimParsers
{
    /// @author Yevgeniy Cherdantsev
    /// @date 13.03.2020 14:01:53
    /// @version 1.0
    /// <summary>
    /// INPUT
    /// </summary>
    /// <code>
    /// 
    /// </code>
    public abstract class ApiAimParser<TDto, TModel, TSourceModel> : ApiParser<TDto, TModel>
        where TModel : DbLoggerCategory.Model where TSourceModel : DbLoggerCategory.Model
    {
        protected List<string> Aims { get; set; }
        private new int Total { get; set; }

        public ApiAimParser(Configuration.ParserSettings parserSettings, string authToken, WebProxy proxy) : base(parserSettings,
            authToken, proxy)
        {
            Aims = LoadAims();
            Total = Aims.Count;
        }

        protected abstract List<string> LoadAims();
        
        public override async Task ParseAsync()
        {
            Logger.Info("Starting Parsing");
            var tasks = new List<Task>();
            for (var i = 0; i < Threads; i++)
            {
                var ls = DivideList(Aims, i);
                tasks.Add(ParseArray(ls));
            }

            await Task.WhenAll(tasks);
            Logger.Info("End Of Parsing");
        }

        private async Task ParseArray(IEnumerable<string> list)
        {
            foreach (var element in list)
            {
                var context = new ParserContext<TModel>();
                string response;
                IRestResponse temp;
                (response, temp) = GetApiPageResponse($"{Url}/{element}", 0).Result;
                lock (Lock)
                {
                    Logger.Trace($"Left: {--Total}");
                }
                if (response == null)
                    continue;
                var dtos = JsonConvert.DeserializeObject<ApiResponse<TDto>>(response);

                foreach (var model in dtos.items.Select(dto => DtoToDb(dto)))
                {
                    try
                    {
                        context.Models.Add(model);
                        await context.SaveChangesAsync();
                    }
                    catch (Exception e)
                    {
                        Logger.Warn(e);
                    }
                }
            }
        }
    }
}