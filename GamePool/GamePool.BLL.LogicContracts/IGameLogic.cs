using System.Collections.Generic;
using GamePool.Common.Entities;

namespace GamePool.BLL.LogicContracts
{
    public interface IGameLogic
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