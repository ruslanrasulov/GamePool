using GamePool.Common.Entities;

namespace GamePool.DAL.DALContracts
{
    public interface IImageDao
    {
        bool Add(ImageEntity imageEntity);

        bool Remove(int id);

        ImageEntity GetById(int id);

        bool SetAvatarForGame(int gameId, int imageId);
    }
}