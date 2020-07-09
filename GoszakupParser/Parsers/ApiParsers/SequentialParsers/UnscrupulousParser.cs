﻿using System;
using System.Net;
using GoszakupParser.Models.Dtos;
using GoszakupParser.Models.ParsingModels;

namespace GoszakupParser.Parsers.ApiParsers.SequentialParsers
{
    /// @author Yevgeniy Cherdantsev
    /// @date 25.02.2020 12:10:21
    /// <summary>
    /// Unscrupulous Parser
    /// </summary>
    public sealed class UnscrupulousParser : ApiSequentialParser<UnscrupulousDto, UnscrupulousGoszakup>
    {
        /// <inheritdoc />
        public UnscrupulousParser(Configuration.ParserSettings parserSettings, WebProxy proxy, string authToken) : base(
            parserSettings,
            proxy, authToken)
        {
        }

        /// <inheritdoc />
        protected override UnscrupulousGoszakup DtoToDb(UnscrupulousDto dto)
        {
            long.TryParse(dto.supplier_biin, out var supplierBiin);
            DateTime.TryParse(dto.index_date, out var indexDate);

            var unscrupulous = new UnscrupulousGoszakup
            {
                Pid = dto.pid,
                IndexDate = indexDate,
                SupplierBiin = supplierBiin,
                SupplierInnunp = !string.IsNullOrEmpty(dto.supplier_innunp) ? dto.supplier_innunp : null,
                SupplierNameRu = !string.IsNullOrEmpty(dto.supplier_name_ru) ? dto.supplier_name_ru : null,
                SupplierNameKz = !string.IsNullOrEmpty(dto.supplier_name_kz) ? dto.supplier_name_kz : null,
                SystemId = dto.system_id
            };

            return unscrupulous;
        }
    }
}