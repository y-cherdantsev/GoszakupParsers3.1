// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo
// ReSharper disable UnusedMember.Global

namespace GoszakupParser.Contexts
{
    /// <summary>
    /// List of existing DB connections
    /// </summary>
    public enum DatabaseConnections
    {
        /// <summary>
        /// Parsing DB, avroradata schema
        /// </summary>
        ParsingAvroradata,

        /// <summary>
        /// Parsing DB, monitoring schema
        /// </summary>
        ParsingMonitoring,

        /// <summary>
        /// Web DB, adata_tender schema
        /// </summary>
        WebAdataTender,

        /// <summary>
        /// Web DB, avroradata schema
        /// </summary>
        WebAvroradata
    }
}