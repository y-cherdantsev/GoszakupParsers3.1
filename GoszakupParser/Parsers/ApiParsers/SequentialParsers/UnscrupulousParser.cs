﻿using System;
using System.Net;
using GoszakupParser.Models.Dtos;
using GoszakupParser.Models.ParsingModels;
using NLog;

namespace GoszakupParser.Parsers.ApiParsers.SequentialParsers
{
    /// @author Yevgeniy Cherdantsev
    /// @date 25.02.2020 12:10:21
    /// @version 1.0
    /// <summary>
    /// Парсер недобросовестных учатников
    /// </summary>
    public sealed class UnscrupulousParser : ApiSequentialParser<UnscrupulousDto, UnscrupulousGoszakup>
    {
        public UnscrupulousParser(Configuration.ParserSettings parserSettings, string authToken, WebProxy proxy) : base(parserSettings,
            authToken, proxy)
        {
        }

        protected override Logger InitLogger()
        {
            return LogManager.GetCurrentClassLogger();
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
                unscrupulous.IndexDate = null;
            }


            long.TryParse(dto.supplier_biin, out var supplierBiin);
            unscrupulous.SupplierBiin = supplierBiin;
            unscrupulous.SupplierInnunp = !string.IsNullOrEmpty(dto.supplier_innunp) ? dto.supplier_innunp : null;
            unscrupulous.SupplierNameRu = !string.IsNullOrEmpty(dto.supplier_name_ru) ? dto.supplier_name_ru : null;
            unscrupulous.SupplierNameKz = !string.IsNullOrEmpty(dto.supplier_name_kz) ? dto.supplier_name_kz : null;
            unscrupulous.SystemId = dto.system_id;
            return unscrupulous;
        }
    }
}