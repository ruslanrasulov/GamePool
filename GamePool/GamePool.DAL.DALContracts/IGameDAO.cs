using GamePool.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePool.DAL.DALContracts
{
    public interface IGameDAO
    {
        bool Add(GameEntity gameEntity);

        bool Remove(int id);

        bool Update(GameEntity gameEntity);

        GameEntity GetById(int id);

        IEnumerable<GameEntity> GetAll(int pageNumber, int pageSize);
    }
}