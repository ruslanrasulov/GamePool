using GamePool.BLL.LogicContracts;
using GamePool.DAL.DALContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamePool.Common.Entities;
using GamePool.BLL.Core.Helpers;

namespace GamePool.BLL.Core
{
    public sealed class UserLogic : IUserLogic
    {
        private IUserDAO userDAO;

        public UserLogic(IUserDAO userDAO)
        {
            this.userDAO = userDAO;
        }

        public bool Add(User user)
        {
            try
            {
                user.Password = user.Password.ComputeSHA256Hash();

                return this.userDAO.Add(user);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool IsExists(User user)
        {
            try
            {
                user.Password = user.Password.ComputeSHA256Hash();

                return this.userDAO.IsExists(user);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool IsLoginExists(string name)
        {
            try
            {
                return this.userDAO.IsLoginExists(name);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool RemoveById(int id)
        {
            try
            {
                return this.userDAO.RemoveById(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool RemoveByName(string name)
        {
            try
            {
                return this.userDAO.RemoveByName(name);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}