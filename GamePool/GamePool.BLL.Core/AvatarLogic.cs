using GamePool.BLL.LogicContracts;
using GamePool.DAL.DALContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePool.BLL.Core
{
    public sealed class AvatarLogic : IAvatarLogic
    {
        private IAvatarDAO avatarDAO;

        public AvatarLogic(IAvatarDAO avatarDAO)
        {
            this.avatarDAO = avatarDAO;
        }

        public bool SetForGame(int gameId, int imageId)
        {
            try
            {
                return this.avatarDAO.SetForGame(gameId, imageId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}