using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePool.DAL.DALContracts
{
    public interface IAvatarDAO
    {
        bool SetForGame(int gameId, int imageId);
    }
}