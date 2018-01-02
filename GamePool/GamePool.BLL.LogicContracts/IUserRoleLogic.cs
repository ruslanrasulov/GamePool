using System.Collections.Generic;
using GamePool.Common.Entities;

namespace GamePool.BLL.LogicContracts
{
    public interface IUserRoleLogic
    {
        IEnumerable<Role> GetAll();

        IEnumerable<Role> GetByUserLogin(string username);

        bool IsUserInRole(string username, string roleName);

        bool AddRoleToUser(string username, string roleName);

        bool RemoveRoleFromUser(string username, string roleName);
    }
}