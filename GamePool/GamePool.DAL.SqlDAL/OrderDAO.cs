using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using GamePool.Common.Entities;
using GamePool.DAL.DALContracts;

namespace GamePool.DAL.SqlDAL
{
    public sealed class OrderDAO : IOrderDAO
    {
        private readonly string connectionString;
        private readonly DbProviderFactory factory;

        public OrderDAO(string connectionString, string providerName)
        {
            this.connectionString = connectionString;
            this.factory = DbProviderFactories.GetFactory(providerName);
        }

        public bool Add(Order order)
        {
            using (IDbConnection connection = this.factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;

                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@Id", order.Id, direction: ParameterDirection.Output);
                parameters.Add("@Name", order.Name);
                parameters.Add("@Surname", order.Surname);
                parameters.Add("@Email", order.Email);
                parameters.Add("@PhoneNumber", order.PhoneNumber);
                parameters.Add("@GameId", order.GameId);
                parameters.Add("@Quantity", order.Quantity);

                connection.Open();

                connection.Execute(
                    sql: "Order_Add",
                    param: parameters,
                    commandType: CommandType.StoredProcedure);

                order.Id = parameters.Get<int>("@Id");

                return order.Id != 0;
            }
        }

        public PagedData<Order> GetAll(int pageNumber, int pageSize)
        {
            using (IDbConnection connection = this.factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;

                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@PageNumber", pageNumber);
                parameters.Add("@PageSize", pageSize);

                connection.Open();

                var query = connection.QueryMultiple(
                    sql: "Order_GetAll",
                    param: parameters,
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