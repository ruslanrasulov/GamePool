using System.Collections.Generic;
using GamePool.BLL.LogicContracts;
using GamePool.Common.Entities;
using GamePool.DAL.DALContracts;

namespace GamePool.BLL.Core
{
    public sealed class GameLogic : IGameLogic
    {
        private readonly IGameDao _gameDao;

        public GameLogic(IGameDao gameDao)
        {
            _gameDao = gameDao;
        }

        public bool Add(GameEntity gameEntity)
        {
            try
            {
                return _gameDao.Add(gameEntity);
            }
            catch
            {
                throw;
            }
        }

        public PagedData<GameEntity> GetAll(int pageNumber, int pageSize)
        {
            try
            {
                return _gameDao.GetAll(pageNumber, pageSize);
            }
            catch
            {
                throw;
            }
        }

        public GameEntity GetById(int id)
        {
            try
            {
                return _gameDao.GetById(id);
            }
            catch
            {
                throw;
            }
        }

        public PagedData<GameEntity> GetByIds(IEnumerable<int> ids)
        {
            try
            {
                return _gameDao.GetByIds(ids);
            }
            catch
            { 
                throw;
            }
        }

        public bool Remove(int id)
        {
            try
            {
                return _gameDao.Remove(id);
            }
            catch
            {
                throw;
            }
        }

        public PagedData<GameEntity> Search(SearchParameters searchParameters)
        {
            try
            {
                return _gameDao.Search(searchParameters);
            }
            catch
            {
                throw;
            }
        }

        public bool Update(GameEntity gameEntity)
        {
            try
            {
                return _gameDao.Update(gameEntity);
            }
            catch
            {
                throw;
            }
        }
    }
}