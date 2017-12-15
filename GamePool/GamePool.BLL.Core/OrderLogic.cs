using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamePool.BLL.LogicContracts;
using GamePool.Common.Entities;
using GamePool.DAL.DALContracts;

namespace GamePool.BLL.Core
{
    public sealed class OrderLogic : IOrderLogic
    {
        private IOrderDAO orderDAO;

        public OrderLogic(IOrderDAO orderDAO)
        {
            this.orderDAO = orderDAO;
        }

        public bool Add(Order order)
        {
            try
            {
                return this.orderDAO.Add(order);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public PagedData<Order> GetAll(int pageNumber, int pageSize)
        {
            try
            {
                return this.orderDAO.GetAll(pageNumber, pageSize);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}