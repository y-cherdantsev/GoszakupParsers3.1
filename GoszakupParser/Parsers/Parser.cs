using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Text.Json;
using System.Threading.Tasks;
using GoszakupParser.Contexts;
using GoszakupParser.Models;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace GoszakupParser.Parsers
{
    /// @author Yevgeniy Cherdantsev
    /// @date 25.02.2020 10:53:43
    /// @version 1.0
    /// <summary>
    /// Parsing abstract class
    /// </summary>
    public abstract class Parser<TContext, TDto, TModel> where TContext : DbContext, new()
    {
        protected readonly Logger Logger;
        protected string Url { get; set; }
        protected int NumOfDbConnections { get; set; }

        protected readonly object Lock = new object();

        protected Parser(Configuration.ParserSettings parserSettings)
        {
            Logger = InitLogger();
            NumOfDbConnections = parserSettings.NumberOfDbConnections;
            Url = parserSettings.Url;
        }

        protected abstract Logger InitLogger();
        protected abstract Task ProcessObjects(TDto[] entities);
        protected abstract TModel DtoToDb(TDto dto);
    }
}