using System.Net;
using GoszakupParser.Models.Dtos;
using GoszakupParser.Models.ParsingModels;

// ReSharper disable CommentTypo

// ReSharper disable once IdentifierTypo
namespace GoszakupParser.Parsers.ApiParsers.SequentialParsers
{
    /// @author Yevgeniy Cherdantsev
    /// @date 09.07.2020 16:30:44
    /// <summary>
    /// Ref Lot Status Parser
    /// </summary>
    // ReSharper disable once UnusedType.Global
    public sealed class RefLotStatusParser : ApiSequentialParser<RefLotStatusDto, RefLotStatusGoszakup>
    {
        /// <inheritdoc />
        public RefLotStatusParser(Configuration.ParserSettings parserSettings, WebProxy proxy, string authToken) : base(
            parserSettings, proxy, authToken)
        {
        }

        /// <inheritdoc />
        protected override RefLotStatusGoszakup DtoToDb(RefLotStatusDto dto)
        {
            var refLotStatus = new RefLotStatusGoszakup
            {
                Id = dto.id,
                NameRu = dto.name_ru,
                NameKz = dto.name_kz,
                Code = dto.code
            };
            return refLotStatus;
        }
    }
}