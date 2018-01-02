using System.Data;
using System.Linq;
using Dapper;
using GamePool.Common.Entities;
using GamePool.DAL.DALContracts;

namespace GamePool.DAL.SqlDAL
{
    public sealed class OrderDao : BaseDao, IOrderDao
    {
        public OrderDao(string connectionString, string providerName)
            :base(connectionString, providerName)
        {
        }

        public bool Add(Order order)
        {
            using (var connection = GetConnection())
            {
                var parameters = new DynamicParameters();

                parameters.Add("@Id", order.Id, direction: ParameterDirection.Output);
                parameters.Add("@Name", order.Name);
                parameters.Add("@Surname", order.Surname);
                parameters.Add("@Email", order.Email);
                parameters.Add("@PhoneNumber", order.PhoneNumber);
                parameters.Add("@GameId", order.GameId);
                parameters.Add("@Quantity", order.Quantity);

                connection.Open();

                connection.Execute(
                    "Order_Add",
                    parameters,
                    commandType: CommandType.StoredProcedure);

                order.Id = parameters.Get<int>("@Id");

                return order.Id != 0;
            }
        }

        public PagedData<Order> GetAll(int pageNumber, int pageSize)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                var query = connection.QueryMultiple(
                    "Order_GetAll",
                    new { PageNumber = pageNumber, PageSize = pageSize},
                    commandType: CommandType.StoredProcedure);

                return new PagedData<Order>
                {
                    Data = query.Read<Order>().ToList(),
                    Count = query.ReadFirst<int>()
                };
            }
        }
    }
}