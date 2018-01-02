using System.Collections.Generic;
using System.Linq;
using GamePool.BLL.LogicContracts;
using GamePool.Common.Entities;
using GamePool.DAL.DALContracts;

namespace GamePool.BLL.Core
{
    public sealed class UserRoleLogic : IUserRoleLogic
    {
        private readonly IUserRoleDao _userRoleDao;

        public UserRoleLogic(IUserRoleDao userRoleDao)
        {
            _userRoleDao = userRoleDao;
        }

        public bool AddRoleToUser(string username, string roleName)
        {
            try
            {
                return _userRoleDao.AddRoleToUser(username, roleName);
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<Role> GetAll()
        {
            try
            {
                return _userRoleDao.GetAll().ToArray();
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<Role> GetByUserLogin(string username)
        {
            try
            {
                return _userRoleDao.GetByUserLogin(username).ToArray();
            }
            catch
            {
                throw;
            }
        }

        public bool IsUserInRole(string username, string roleName)
        {
            try
            {
                return IsUserInRole(username, roleName);
            }
            catch
            {
                throw;
            }
        }

        public bool RemoveRoleFromUser(string username, string roleName)
        {
            try
            {
                return _userRoleDao.RemoveRoleFromUser(username, roleName);
            }
            catch
            { 
                throw;
            }
        }
    }
}