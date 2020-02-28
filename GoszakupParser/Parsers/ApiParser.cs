using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Threading.Tasks;
using GoszakupParser.Contexts;
using GoszakupParser.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace GoszakupParser.Parsers
{
    /// @author Yevgeniy Cherdantsev
    /// @date 28.02.2020 14:51:37
    /// @version 1.0
    /// <summary>
    /// INPUT
    /// </summary>
    public abstract class ApiParser<TDto, TModel> : Parser<ParserContext<TModel>, TDto, TModel>
        where TModel : DbLoggerCategory.Model, new()
    {
        protected ApiParser(Configuration.ParserSettings parserSettings, string authToken) : base(parserSettings)
        {
            AuthToken = authToken;
            var response = GetApiPageResponse(Url);
            Total = JsonSerializer.Deserialize<ApiResponse<TDto>>(response).total;
        }

        protected int Total { get; set; }
        private string AuthToken { get; set; }
        
        protected abstract TDto[] DivideList(List<TDto> list, int i);

        public async Task ParseApiAsync()
        {
            var tasks = new List<Task>();
            Logger.Info("Starting parsing");
            while (true)
            {
                var response = GetApiPageResponse(Url);
                if (response == null)
                    break;
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse<TDto>>(response);
                Url = apiResponse?.next_page;

                await Task.WhenAll(tasks);
                tasks.Clear();

                for (var i = 0; i < NumOfDbConnections; i++)
                    tasks.Add(ProcessObjects(DivideList(apiResponse.items, i)));

                if (Url != "") continue;
                await Task.WhenAll(tasks);
                tasks.Clear();
                Contexts.Clear();
                break;
            }

            Logger.Info("Parsing done");
            if (Total > 0)
                Logger.Info($"{Total} elements hasn't been parsed");
        }

        private string GetApiPageResponse(string url)
        {
            var request = WebRequest.Create($"https://ows.goszakup.gov.kz/{url}?limit=500");
            request.Method = WebRequestMethods.Http.Get;
            request.Headers["Content-Type"] = "application/json";
            request.Headers["Authorization"] = AuthToken;
            request.AuthenticationLevel = AuthenticationLevel.None;
            var response = request.GetResponse();
            if (response.GetResponseStream() == null)
                return null;
            var objReader =
                new StreamReader(response.GetResponseStream() ?? throw new NullReferenceException());

            var sLine = "";
            var pageResponse = "";
            while (sLine != null)
            {
                sLine = objReader.ReadLine();
                if (sLine != null)
                    pageResponse += sLine;
            }

            return pageResponse;
        }
    }
}