using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePool.BLL.LogicContracts
{
    public interface IAvatarLogic
    {
        bool SetForGame(int gameId, int imageId);
    }
}