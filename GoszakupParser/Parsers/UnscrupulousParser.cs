using NLog;

namespace GoszakupParser.Parsers
{

    /// @author Yevgeniy Cherdantsev
    /// @date 25.02.2020 12:10:21
    /// @version 1.0
    /// <summary>
    /// Парсер недобросовестных учатников
    /// </summary>


    public class UnscrupulousParser : Parser
    {
        protected override Logger InitLogger()
        {
            return LogManager.GetCurrentClassLogger();
        }

        public override void Parse()
        {
            throw new System.NotImplementedException();
        }

        public override void ProcessObject(object entity)
        {
            throw new System.NotImplementedException();
        }
    }
}