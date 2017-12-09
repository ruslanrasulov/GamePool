using System.Collections.Generic;

namespace GamePool.PL.MVC.Models.Shared
{
    public class PagedItems<T>
    {
        public IEnumerable<T> Data { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int MaxPageSelectors { get; set; }
    }
}