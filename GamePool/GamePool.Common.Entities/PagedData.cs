using System.Collections.Generic;

namespace GamePool.Common.Entities
{
    public class PagedData<T>
    {
        public IEnumerable<T> Data { get; set; }

        public int Count { get; set; }
    }
}