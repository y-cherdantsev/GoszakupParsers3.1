using System.Net;
using GoszakupParser.Models.Dtos;
using GoszakupParser.Models.ParsingModels;

// ReSharper disable CommentTypo

// ReSharper disable once IdentifierTypo
namespace GoszakupParser.Parsers.ApiParsers.SequentialParsers
{
    /// @author Yevgeniy Cherdantsev
    /// @date 09.07.2020 16:41:08
    /// <summary>
    /// Ref Trade Method Parser
    /// </summary>
    // ReSharper disable once UnusedType.Global
    public sealed class RefTradeMethodParser : ApiSequentialParser<RefTradeMethodDto, RefTradeMethodGoszakup>
    {
        /// <inheritdoc />
        public RefTradeMethodParser(Configuration.ParserSettings parserSettings, WebProxy proxy, string authToken) : base(
            parserSettings, proxy, authToken)
        {
        }

        /// <inheritdoc />
        protected override RefTradeMethodGoszakup DtoToModel(RefTradeMethodDto dto)
        {
            var refTradeMethod = new RefTradeMethodGoszakup
            {
                Id = dto.id,
                NameRu = dto.name_ru,
                NameKz = dto.name_kz,
                SymbolCode = dto.symbol_code,
                Code = dto.code,
                IsActive = dto.is_active,
                Type = dto.type,
                F1 = dto.f1,
                Ord = dto.ord,
                F2 = dto.f2
            };
            return refTradeMethod;
        }
    }
}