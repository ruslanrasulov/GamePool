using GamePool.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePool.BLL.LogicContracts
{
    public interface ISystemRequirementsLogic
    {
        bool Add(SystemRequirements systemRequirements);

        bool Update(SystemRequirements systemRequirements);

        SystemRequirements GetById(int id);
    }
}
