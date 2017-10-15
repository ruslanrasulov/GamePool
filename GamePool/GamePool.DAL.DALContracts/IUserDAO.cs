using GamePool.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePool.DAL.DALContracts
{
    public interface IUserDAO
    {
        bool Add(User user);

        bool RemoveById(int id);

        bool RemoveByName(string name);

        bool IsLoginExists(string name);

        bool IsExists(User user);
    }
}