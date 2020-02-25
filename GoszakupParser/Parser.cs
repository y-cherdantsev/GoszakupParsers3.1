namespace GoszakupParser
{

    /// @author Yevgeniy Cherdantsev
    /// @date 25.02.2020 10:53:43
    /// @version 1.0
    /// <summary>
    /// INPUT
    /// </summary>


    public abstract class Parser
    {
        
        public abstract void Parse();
        public abstract void ProcessObject(object entity);
        protected abstract string GetPageResponse(string url);
    }
}