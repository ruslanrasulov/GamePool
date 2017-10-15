using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePool.DAL.DALContracts
{
    public interface IScreenshotDAO
    {
        bool AddToGame(int gameId, int imageId);

        bool RemoveFromGame(int gameId, int imageId);
    }
}