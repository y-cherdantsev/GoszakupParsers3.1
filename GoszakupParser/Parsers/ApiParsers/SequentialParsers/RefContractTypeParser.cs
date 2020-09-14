using GoszakupParser.Models.Dtos;
using GoszakupParser.Models.ParsingModels;

// ReSharper disable CommentTypo

// ReSharper disable once IdentifierTypo
namespace GoszakupParser.Parsers.ApiParsers.SequentialParsers
{
    /// @author Yevgeniy Cherdantsev
    /// @date 14.09.2020 14:39:48
    /// <summary>
    /// Ref Contract Type Parser
    /// </summary>
    // ReSharper disable once UnusedType.Global
    public sealed class RefContractTypeParser : ApiSequentialParser<RefContractTypeDto, RefContractTypeGoszakup>
    {
        /// <inheritdoc />
        public RefContractTypeParser(Configuration.ParserSettings parserSettings, string authToken) : base(
            parserSettings, authToken)
        {
        }

        /// <inheritdoc />
        protected override RefContractTypeGoszakup DtoToModel(RefContractTypeDto dto)
        {
            var refContractType = new RefContractTypeGoszakup
            {
                Id = dto.id,
                NameRu = dto.name_ru,
                NameKz = dto.name_kz
            };
            return refContractType;
        }
    }
}