using GamePool.Common.Entities;

namespace GamePool.DAL.DALContracts
{
    public interface IGameDAO
    {
        bool Add(GameEntity gameEntity);

        bool Remove(int id);

        bool Update(GameEntity gameEntity);

        GameEntity GetById(int id);

        PagedData<GameEntity> GetAll(int pageNumber, int pageSize);
    }
}