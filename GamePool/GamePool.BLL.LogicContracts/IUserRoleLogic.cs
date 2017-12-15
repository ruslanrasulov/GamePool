using GamePool.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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