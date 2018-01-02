using GamePool.Common.Entities;

namespace GamePool.BLL.LogicContracts
{
    public interface IImageLogic
    {
        bool Add(ImageEntity imageEntity);

        bool Remove(int id);

        ImageEntity GetById(int id);

        bool SetAvatarForGame(int gameId, int imageId);
    }
}