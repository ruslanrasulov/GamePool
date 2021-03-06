﻿using System.Collections.Generic;
using GamePool.Common.Entities;

namespace GamePool.DAL.DALContracts
{
    public interface IGameDao
    {
        bool Add(GameEntity gameEntity);

        bool Remove(int id);

        bool Update(GameEntity gameEntity);

        GameEntity GetById(int id);

        PagedData<GameEntity> GetByIds(IEnumerable<int> ids);

        PagedData<GameEntity> GetAll(int pageNumber, int pageSize);

        PagedData<GameEntity> Search(SearchParameters searchParameters);
    }
}