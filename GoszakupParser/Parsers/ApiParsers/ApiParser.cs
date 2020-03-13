using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using GoszakupParser.Models.Dtos;
using NLog;

namespace GoszakupParser.Parsers.ApiParsers
{
    /// @author Yevgeniy Cherdantsev
    /// @date 29.02.2020 11:53:37
    /// @version 1.0
    /// <summary>
    /// INPUT
    /// </summary>
    public abstract class ApiParser<TDto, TModel> : Parser
    {
        private string AuthToken { get; set; }
        protected int Total { get; set; }

        protected ApiParser(Configuration.ParserSettings parserSettings, string authToken) : base(parserSettings)
        {
            AuthToken = authToken;
            var response = GetApiPageResponse(Url);
            Total = JsonSerializer.Deserialize<ApiResponse<TDto>>(response).total;
        }

        protected abstract override Logger InitLogger();
        public abstract override Task ParseAsync();
        protected abstract TModel DtoToDb(TDto dto);

        protected string GetApiPageResponse(string url)
        {
            var i = 0;
            while (true)
            {
                var request = WebRequest.Create($"https://ows.goszakup.gov.kz/{url}?limit=500");
                request.Method = WebRequestMethods.Http.Get;
                request.Headers["Content-Type"] = "application/json";
                request.Headers["Authorization"] = AuthToken;
                request.AuthenticationLevel = AuthenticationLevel.None;
                WebResponse response;
                try
                {
                    response = request.GetResponse();
                }
                catch (Exception e)
                {
                    Thread.Sleep(30000);
                    if (++i == 5)
                    {
                        Logger.Error(e);
                        throw;
                    }
                    continue;
                }
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
}