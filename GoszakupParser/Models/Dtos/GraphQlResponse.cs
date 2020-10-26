using System.Collections.Generic;

// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace GoszakupParser.Models.Dtos
{
    /// @author Yevgeniy Cherdantsev
    /// @date 28.29.2020 9:19:21
    /// <summary>
    /// GraphQl response object
    /// </summary>
    /// <typeparam name="T">Type of response elements</typeparam>
    public class GraphQlResponse<T>
    {
        public Data<T> data { get; set; }
        public Extensions extensions { get; set; }

        public class Data<U>
        {
            public List<U> items = new List<U>();
        }

        public class Extensions
        {
            public PageInfo pageInfo { get; set; }

            public class PageInfo
            {
                public int limitPage { get; set; }
                public int totalCount { get; set; }
                public bool hasNextPage { get; set; }
                public int lastId { get; set; }
            }
        }
    }
}