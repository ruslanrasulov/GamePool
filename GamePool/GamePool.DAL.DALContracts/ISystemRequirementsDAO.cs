using GamePool.Common.Entities;

namespace GamePool.DAL.DALContracts
{
    public interface ISystemRequirementsDao
    {
        bool Add(SystemRequirements systemRequirements);

        bool Update(SystemRequirements systemRequirements);

        SystemRequirements GetById(int id);
    }
}