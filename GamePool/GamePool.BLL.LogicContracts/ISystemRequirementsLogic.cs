using GamePool.Common.Entities;

namespace GamePool.BLL.LogicContracts
{
    public interface ISystemRequirementsLogic
    {
        bool Add(SystemRequirements systemRequirements);

        bool Update(SystemRequirements systemRequirements);

        SystemRequirements GetById(int id);
    }
}
