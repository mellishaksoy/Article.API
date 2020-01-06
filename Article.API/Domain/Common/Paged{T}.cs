using System.Collections.Generic;

namespace Article.API.Domain.Common
{
    public class Paged<T>
    {
        public int TotalPageCount { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int FilteredCount { get; set; }
        public ICollection<T> List { get; set; }
    }
}
