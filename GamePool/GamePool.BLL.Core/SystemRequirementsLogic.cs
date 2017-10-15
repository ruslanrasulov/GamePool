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
    public sealed class SystemRequirementsLogic : ISystemRequirementsLogic
    {
        private ISystemRequirementsDAO systemRequirementsDAO;

        public SystemRequirementsLogic(ISystemRequirementsDAO systemRequirementsDAO)
        {
            this.systemRequirementsDAO = systemRequirementsDAO;
        }

        public bool Add(SystemRequirements systemRequirements)
        {
            try
            {
                return this.systemRequirementsDAO.Add(systemRequirements);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public SystemRequirements GetById(int id)
        {
            try
            {
                return this.systemRequirementsDAO.GetById(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Update(SystemRequirements systemRequirements)
        {
            try
            {
                return this.systemRequirementsDAO.Update(systemRequirements);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}