using System.Collections.Generic;

namespace Article.API.Domain.Common
{
    public class OrdinatedPageDto
    {
        public List<Ordination> Ordination { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int TotalCount { get; set; }
        public int FilteredCount { get; set; }

        public OrdinatedPageDto()
        {
            PageSize = 250;
            PageIndex = 1;
        }
    }
}
