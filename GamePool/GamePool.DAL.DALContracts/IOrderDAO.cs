using GamePool.Common.Entities;

namespace GamePool.DAL.DALContracts
{
    public interface IOrderDAO
    {
        bool Add(Order order);

        PagedData<Order> GetAll(int pageNumber, int pageSize);
    }
}