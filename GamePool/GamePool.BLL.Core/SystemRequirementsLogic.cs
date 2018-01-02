using GamePool.BLL.LogicContracts;
using GamePool.Common.Entities;
using GamePool.DAL.DALContracts;

namespace GamePool.BLL.Core
{
    public sealed class SystemRequirementsLogic : ISystemRequirementsLogic
    {
        private readonly ISystemRequirementsDao _systemRequirementsDao;

        public SystemRequirementsLogic(ISystemRequirementsDao systemRequirementsDao)
        {
            _systemRequirementsDao = systemRequirementsDao;
        }

        public bool Add(SystemRequirements systemRequirements)
        {
            try
            {
                return _systemRequirementsDao.Add(systemRequirements);
            }
            catch
            {
                throw;
            }
        }

        public SystemRequirements GetById(int id)
        {
            try
            {
                return _systemRequirementsDao.GetById(id);
            }
            catch
            {
                throw;
            }
        }

        public bool Update(SystemRequirements systemRequirements)
        {
            try
            {
                return _systemRequirementsDao.Update(systemRequirements);
            }
            catch
            {
                throw;
            }
        }
    }
}