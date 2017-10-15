using GamePool.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePool.DAL.DALContracts
{
    public interface IUserRoleDAO
    {
        IEnumerable<Role> GetByUserLogin(string username);

        bool IsUserInRole(string username, string roleName);
    }
}