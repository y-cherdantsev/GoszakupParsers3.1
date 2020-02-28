using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoszakupParser.Contexts;
using GoszakupParser.Models;
using NLog;

namespace GoszakupParser.Parsers
{
    /// @author Yevgeniy Cherdantsev
    /// @date 25.02.2020 12:10:21
    /// @version 1.0
    /// <summary>
    /// Парсер недобросовестных учатников
    /// </summary>
    public class UnscrupulousParser : ApiParser<UnscrupulousDto, UnscrupulousGoszakup>
    {
        public UnscrupulousParser(Configuration.ParserSettings parserSettings, string authToken) : base(parserSettings,
            authToken)
        {
        }

        protected override Logger InitLogger()
        {
            return LogManager.GetCurrentClassLogger();
        }

        protected override async Task ProcessObjects(UnscrupulousDto[] entities)
        {
            var unscrupulousGoszakupContext = Contexts[entities[0].pid % NumOfDbConnections];
            foreach (var entity in entities)
            {
                var unscrupulous = DtoToDb(entity);
                unscrupulousGoszakupContext.Models.Add(unscrupulous);
                await unscrupulousGoszakupContext.SaveChangesAsync();
                lock (Lock)
                    Logger.Trace($"Left:{--Total}");
            }
        }

        protected override UnscrupulousGoszakup DtoToDb(UnscrupulousDto dto)
        {
            var unscrupulous = new UnscrupulousGoszakup();
            unscrupulous.Pid = dto.pid;
            try
            {
                unscrupulous.IndexDate = DateTime.Parse(dto.index_date);
            }
            catch (Exception)
            {
            }

            Int64.TryParse(dto.supplier_biin, out var supplier_biin);
            unscrupulous.SupplierBiin = supplier_biin;
            unscrupulous.SupplierInnunp = dto.supplier_innunp;
            unscrupulous.SupplierNameRu = dto.supplier_name_ru;
            unscrupulous.SupplierNameKz = dto.supplier_name_kz;
            unscrupulous.SystemId = dto.system_id;
            // unscrupulous.Relevance = DateTime.Now;
            return unscrupulous;
        }

        protected override UnscrupulousDto[] DivideList(List<UnscrupulousDto> list, int i)
        {
            return list.Where(x => x.pid % NumOfDbConnections == i).ToArray();
        }
    }
}