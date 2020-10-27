using NLog;
using System.Threading.Tasks;

// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo

namespace GoszakupParser.Services
{
    /// @author Yevgeniy Cherdantsev
    /// @date 26.02.2020 18:18:55
    /// <summary>
    /// Proceeding of arguments and general logic
    /// </summary>
    public abstract class GoszakupService
    {
        /// <summary>
        /// Current class logger
        /// </summary>
        internal readonly Logger Logger;

        internal GoszakupService()
        {
            Logger = LogManager.GetLogger(GetType().Name, GetType());
        }

        /// <summary>
        /// Starts service
        /// </summary>
        // ReSharper disable once UnusedMemberInSuper.Global
        public abstract Task StartService();
    }
}