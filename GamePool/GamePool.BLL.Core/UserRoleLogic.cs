using GamePool.BLL.LogicContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamePool.Common.Entities;
using GamePool.DAL.DALContracts;

namespace GamePool.BLL.Core
{
    public sealed class UserRoleLogic : IUserRoleLogic
    {
        private IUserRoleDAO userRoleDAO;

        public UserRoleLogic(IUserRoleDAO userRoleDAO)
        {
            this.userRoleDAO = userRoleDAO;
        }

        public IEnumerable<Role> GetByUserLogin(string username)
        {
            try
            {
                return this.userRoleDAO.GetByUserLogin(username).ToArray();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool IsUserInRole(string username, string roleName)
        {
            try
            {
                return this.IsUserInRole(username, roleName);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}