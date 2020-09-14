using GoszakupParser.Models.Dtos;
using GoszakupParser.Models.ParsingModels;

// ReSharper disable CommentTypo

// ReSharper disable once IdentifierTypo
namespace GoszakupParser.Parsers.ApiParsers.SequentialParsers
{
    /// @author Yevgeniy Cherdantsev
    /// @date 14.09.2020 14:39:48
    /// <summary>
    /// Ref Contract Status Parser
    /// </summary>
    // ReSharper disable once UnusedType.Global
    public sealed class RefContractStatusParser : ApiSequentialParser<RefContractStatusDto, RefContractStatusGoszakup>
    {
        /// <inheritdoc />
        public RefContractStatusParser(Configuration.ParserSettings parserSettings, string authToken) : base(
            parserSettings, authToken)
        {
        }

        /// <inheritdoc />
        protected override RefContractStatusGoszakup DtoToModel(RefContractStatusDto dto)
        {
            var refContractStatus = new RefContractStatusGoszakup
            {
                Id = dto.id,
                NameRu = dto.name_ru,
                NameKz = dto.name_kz,
                Code = dto.code
            };
            return refContractStatus;
        }
    }
}