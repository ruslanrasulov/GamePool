using GamePool.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePool.DAL.DALContracts
{
    public interface ISystemRequirementsDAO
    {
        bool Add(SystemRequirements systemRequirements);

        bool Update(SystemRequirements systemRequirements);

        SystemRequirements GetById(int id);
    }
}