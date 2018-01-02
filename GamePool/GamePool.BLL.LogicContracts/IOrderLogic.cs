using GamePool.Common.Entities;

namespace GamePool.BLL.LogicContracts
{
    public interface IOrderLogic
    {
        bool Add(Order order);

        PagedData<Order> GetAll(int pageNumber, int pageSize);
    }
}