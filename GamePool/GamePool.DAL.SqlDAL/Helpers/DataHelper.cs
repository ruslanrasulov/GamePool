using System.Collections.Generic;
using GamePool.Common.Entities;

namespace GamePool.DAL.SqlDAL.Helpers
{
    public static class DataHelper
    { 
        public static PagedData<T> GetPagedData<T>(IEnumerable<T> games, int count)
        {
            return new PagedData<T>
            {
                Data = games,
                Count = count
            };
        }
    }
}
