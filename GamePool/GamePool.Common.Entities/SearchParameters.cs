using System;
using System.Collections.Generic;

namespace GamePool.Common.Entities
{
    public class SearchParameters
    {
        public string Name { get; set; }

        public decimal? PriceFrom { get; set; }

        public decimal? PriceTo { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public IEnumerable<int> GenreIds { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    }
}