using Microsoft.EntityFrameworkCore;

// ReSharper disable CommentTypo

// ReSharper disable once IdentifierTypo
namespace GoszakupParser.Models
{
    /// @author Yevgeniy Cherdantsev
    /// @date 10.08.2020 16:48:23
    /// <summary>
    /// Base Model for creating DB models
    /// </summary>
    public class BaseModel : DbLoggerCategory.Model
    {
        /// <summary>
        /// Base Constructor
        /// </summary>
        protected BaseModel()
        {
        }

        /// <summary>
        /// Can be overrided to set some features of a model
        /// </summary>
        /// <param name="modelBuilder">Model Builder for setting features of a model</param>
        public virtual void BuildModel(ModelBuilder modelBuilder)
        {
        }
    }
}