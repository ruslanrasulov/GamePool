using System.Collections.Generic;
using GamePool.Common.Entities;

namespace GamePool.BLL.LogicContracts
{
    public interface IGenreLogic
    {
        IEnumerable<Genre> GetByNamePart(string keyWord);

        IEnumerable<Genre> GetByIds(IEnumerable<int> ids);

        IEnumerable<Genre> GetByGameId(int gameId);

        bool Add(Genre genre);

        bool AddGenresByGameId(int gameId, IEnumerable<int> ids);

        bool UpdateGenresByGameId(int gameId, IEnumerable<int> ids);

        bool RemoveGenresByGameId(int gameId, IEnumerable<int> ids);
    }
}