using GamePool.BLL.LogicContracts;
using GamePool.DAL.DALContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePool.BLL.Core
{
    public sealed class ScreenshotLogic : IScreenshotLogic
    {
        private IScreenshotDAO screenshotDAO;

        public ScreenshotLogic(IScreenshotDAO screenshotDAO)
        {
            this.screenshotDAO = screenshotDAO;
        }

        public bool AddToGame(int gameId, int imageId)
        {
            try
            {
                return this.screenshotDAO.AddToGame(gameId, imageId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool RemoveFromGame(int gameId, int imageId)
        {
            try
            {
                return this.screenshotDAO.RemoveFromGame(gameId, imageId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}