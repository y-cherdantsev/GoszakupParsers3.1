using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Threading.Tasks;
using NLog;

namespace GoszakupParser.Parsers
{
    /// @author Yevgeniy Cherdantsev
    /// @date 25.02.2020 10:53:43
    /// @version 1.0
    /// <summary>
    /// Parsing abstract class
    /// </summary>
    public abstract class Parser
    {
        private readonly Logger _logger; /*!< Логгер текущего класса */
        protected string Url { get; set; }
        private int Total { get; set; }
        private int NumOfDbConnections { get; set; }
        protected string AuthToken { get; set; }

        protected Parser()
        {
            _logger = InitLogger();
        }

        protected abstract Logger InitLogger();
        public abstract Task Parse();
        public abstract Task ProcessObjects(List<object> entities);

        protected string GetApiPageResponse(string url)
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

        protected object DtoToDb()
        {
            
        }
    }
}