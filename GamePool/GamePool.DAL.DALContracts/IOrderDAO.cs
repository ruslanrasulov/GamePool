using GamePool.Common.Entities;

namespace GamePool.DAL.DALContracts
{
    public interface IOrderDao
    {
        bool Add(Order order);

        PagedData<Order> GetAll(int pageNumber, int pageSize);
    }
}