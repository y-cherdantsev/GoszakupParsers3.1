using System.Threading.Tasks;
using NLog;

namespace GoszakupParser.Parsers
{

    /// @author Yevgeniy Cherdantsev
    /// @date 28.02.2020 13:56:44
    /// @version 1.0
    /// <summary>
    /// INPUT
    /// </summary>
    public class ParticipantParser : Parser
    {
        protected override Logger InitLogger()
        {
            return LogManager.GetCurrentClassLogger();
        }

        public override Task ParseAsync()
        {
            throw new System.NotImplementedException();
        }

        public override Task ProcessObjects(object[] entities)
        {
            throw new System.NotImplementedException();
        }

        protected override object DtoToDb(object dto)
        {
            throw new System.NotImplementedException();
        }
    }
}