using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePool.BLL.LogicContracts
{
    public interface IScreenshotLogic
    {
        bool AddToGame(int gameId, int imageId);

        bool RemoveFromGame(int gameId, int imageId);
    }
}