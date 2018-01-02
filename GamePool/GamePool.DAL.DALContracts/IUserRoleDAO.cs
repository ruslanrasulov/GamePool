using GamePool.Common.Entities;
using System.Collections.Generic;

namespace GamePool.DAL.DALContracts
{
    public interface IUserRoleDao
    {
        IEnumerable<Role> GetAll();

        IEnumerable<Role> GetByUserLogin(string username);

        bool IsUserInRole(string username, string roleName);

        bool AddRoleToUser(string username, string roleName);

        bool RemoveRoleFromUser(string username, string roleName);
    }
}