using GamePool.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePool.BLL.LogicContracts
{
    public interface IImageLogic
    {
        bool Add(ImageEntity imageEntity);

        bool Remove(int id);

        ImageEntity GetById(int id);
    }
}