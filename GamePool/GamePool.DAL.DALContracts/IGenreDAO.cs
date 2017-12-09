using System.Collections.Generic;
using GamePool.Common.Entities;

namespace GamePool.DAL.DALContracts
{
    public interface IGenreDAO
    {
        IEnumerable<Genre> GetByNamePart(string keyWord);

        IEnumerable<Genre> GetByIds(IEnumerable<int> ids);

        IEnumerable<Genre> GetByGameId(int gameId);

        bool AddRange(int gameId, IEnumerable<int> ids);

        bool Add(Genre genre);
    }
}