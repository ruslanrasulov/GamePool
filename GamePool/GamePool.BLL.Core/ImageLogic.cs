using GamePool.BLL.LogicContracts;
using GamePool.Common.Entities;
using GamePool.DAL.DALContracts;

namespace GamePool.BLL.Core
{
    public sealed class ImageLogic : IImageLogic
    {
        private readonly IImageDao _imageDao;

        public ImageLogic(IImageDao imageDao)
        {
            _imageDao = imageDao;
        }

        public bool Add(ImageEntity imageEntity)
        {
            try
            {
                return _imageDao.Add(imageEntity);
            }
            catch
            {
                throw;
            }
        }

        public ImageEntity GetById(int id)
        {
            try
            {
                return _imageDao.GetById(id);
            }
            catch
            {
                throw;
            }
        }

        public bool Remove(int id)
        {
            try
            {
                return _imageDao.Remove(id);
            }
            catch
            {
                throw;
            }
        }

        public bool SetAvatarForGame(int gameId, int imageId)
        {
            try
            {
                return _imageDao.SetAvatarForGame(gameId, imageId);
            }
            catch
            {
                throw;
            }
        }
    }
}