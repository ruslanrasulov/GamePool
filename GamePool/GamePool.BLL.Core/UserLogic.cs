using GamePool.BLL.Core.Helpers;
using GamePool.BLL.LogicContracts;
using GamePool.Common.Entities;
using GamePool.DAL.DALContracts;

namespace GamePool.BLL.Core
{
    public sealed class UserLogic : IUserLogic
    {
        private readonly IUserDao _userDao;
        
        public UserLogic(IUserDao userDao)
        {
            _userDao = userDao;
        }

        public bool Add(UserEntity user)
        {
            try
            {
                user.Password = user.Password.ComputeSha512Hash();

                return _userDao.Add(user);
            }
            catch
            {
                throw;
            }
        }

        public PagedData<UserEntity> GetAll(int pageNumber, int pageSize)
        {
            try
            {
                return _userDao.GetAll(pageNumber, pageSize);
            }
            catch
            {
                throw;
            }
        }

        public bool IsExists(UserEntity user)
        {
            try
            {
                user.Password = user.Password.ComputeSha512Hash();

                return _userDao.IsExists(user);
            }
            catch
            {
                throw;
            }
        }

        public bool IsLoginExists(string name)
        {
            try
            {
                return _userDao.IsLoginExists(name);
            }
            catch
            {
                throw;
            }
        }

        public bool RemoveById(int id)
        {
            try
            {
                return _userDao.RemoveById(id);
            }
            catch
            {
                throw;
            }
        }

        public bool RemoveByName(string name)
        {
            try
            {
                return _userDao.RemoveByName(name);
            }
            catch
            {
                throw;
            }
        }
    }
}