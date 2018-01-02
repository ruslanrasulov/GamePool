using GamePool.Common.Entities;

namespace GamePool.DAL.DALContracts
{
    public interface IUserDao
    {
        bool Add(UserEntity user);

        bool RemoveById(int id);

        bool RemoveByName(string name);

        bool IsLoginExists(string name);

        bool IsExists(UserEntity user);

        PagedData<UserEntity> GetAll(int pageNumber, int pageSize);
    }
}