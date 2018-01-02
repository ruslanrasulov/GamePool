using GamePool.BLL.LogicContracts;
using GamePool.Common.Entities;
using GamePool.DAL.DALContracts;

namespace GamePool.BLL.Core
{
    public sealed class OrderLogic : IOrderLogic
    {
        private readonly IOrderDao _orderDao;

        public OrderLogic(IOrderDao orderDao)
        {
            _orderDao = orderDao;
        }

        public bool Add(Order order)
        {
            try
            {
                return _orderDao.Add(order);
            }
            catch
            {
                throw;
            }
        }

        public PagedData<Order> GetAll(int pageNumber, int pageSize)
        {
            try
            {
                return _orderDao.GetAll(pageNumber, pageSize);
            }
            catch
            {
                throw;
            }
        }
    }
}