using GoszakupParser.Models.Dtos;
using GoszakupParser.Models.ParsingModels;

// ReSharper disable CommentTypo

// ReSharper disable once IdentifierTypo
namespace GoszakupParser.Parsers.ApiParsers.SequentialParsers
{
    /// @author Yevgeniy Cherdantsev
    /// @date 09.07.2020 16:41:08
    /// <summary>
    /// Ref Unit Parser
    /// </summary>
    // ReSharper disable once UnusedType.Global
    public sealed class RefUnitParser : ApiSequentialParser<RefUnitDto, RefUnitGoszakup>
    {
        /// <inheritdoc />
        public RefUnitParser(Configuration.ParserSettings parserSettings, string authToken) : base(
            parserSettings, authToken)
        {
        }

        /// <inheritdoc />
        protected override RefUnitGoszakup DtoToModel(RefUnitDto dto)
        {
            var refUnit = new RefUnitGoszakup
            {
                Id = dto.id,
                NameRu = dto.name_ru,
                NameKz = dto.name_kz,
                Code = dto.code,
                Code2 = dto.code2,
                AlphaCode = dto.alpha_code
            };
            return refUnit;
        }
    }
}