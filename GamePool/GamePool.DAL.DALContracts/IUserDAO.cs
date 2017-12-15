using GamePool.Common.Entities;

namespace GamePool.DAL.DALContracts
{
    public interface IUserDAO
    {
        bool Add(User user);

        bool RemoveById(int id);

        bool RemoveByName(string name);

        bool IsLoginExists(string name);

        bool IsExists(User user);

        PagedData<User> GetAll(int pageNumber, int pageSize);
    }
}