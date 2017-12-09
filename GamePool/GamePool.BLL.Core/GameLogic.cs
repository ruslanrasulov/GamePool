using GamePool.BLL.LogicContracts;
using System;
using GamePool.Common.Entities;
using GamePool.DAL.DALContracts;

namespace GamePool.BLL.Core
{
    public sealed class GameLogic : IGameLogic
    {
        private IGameDAO gameDAO;

        public GameLogic(IGameDAO gameDAO)
        {
            this.gameDAO = gameDAO;
        }

        public bool Add(GameEntity gameEntity)
        {
            try
            {
                return this.gameDAO.Add(gameEntity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public PagedData<GameEntity> GetAll(int pageNumber, int pageSize)
        {
            try
            {
                return this.gameDAO.GetAll(pageNumber, pageSize);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public GameEntity GetById(int id)
        {
            try
            {
                return this.gameDAO.GetById(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Remove(int id)
        {
            try
            {
                return this.gameDAO.Remove(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Update(GameEntity gameEntity)
        {
            try
            {
                return this.gameDAO.Update(gameEntity);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}