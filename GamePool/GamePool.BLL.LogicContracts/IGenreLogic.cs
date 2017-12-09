﻿using System.Collections.Generic;
using GamePool.Common.Entities;

namespace GamePool.BLL.LogicContracts
{
    public interface IGenreLogic
    {
        IEnumerable<Genre> GetByNamePart(string keyWord);

        IEnumerable<Genre> GetByIds(IEnumerable<int> ids);

        IEnumerable<Genre> GetByGameId(int gameId);

        bool Add(Genre genre);

        bool AddRange(int gameId, IEnumerable<int> ids);
    }
}