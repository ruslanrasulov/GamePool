using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamePool.Common.Entities;

namespace GamePool.BLL.LogicContracts
{
    public interface IRatingLogic
    {
        bool Add(Rating rating);

        bool Update(Rating rating);
    }
}