using System;
using GoszakupParser.Models.Dtos;
using GoszakupParser.Models.ParsingModels;

// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo

namespace GoszakupParser.Parsers.ApiParsers.SequentialParsers
{
    /// @author Yevgeniy Cherdantsev
    /// @date 25.02.2020 12:10:21
    /// <summary>
    /// Unscrupulous Parser
    /// </summary>
    // ReSharper disable once UnusedType.Global
    public sealed class UnscrupulousParser : ApiSequentialParser<UnscrupulousDto, UnscrupulousGoszakup>
    {
        /// <inheritdoc />
        public UnscrupulousParser(Configuration.ParserSettings parserSettings) : base(
            parserSettings)
        {
        }

        /// <inheritdoc />
        protected override UnscrupulousGoszakup DtoToModel(UnscrupulousDto dto)
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