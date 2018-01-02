using System.Collections.Generic;

namespace GamePool.PL.MVC.Models.Shared
{
    public class PagedItems<T>
    {
        public IEnumerable<T> Data { get; set; }

        public PageInfo PageInfo { get; set; }
    }
}