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
                parameters.Add("@Date", order.Date);
                parameters.Add("@State", order.State);
                parameters.Add("@GameId", order.GameId);
                parameters.Add("@UserId", order.UserId);

                connection.Open();

                connection.Execute(
                    sql: "Order_Add",
                    param: parameters,
                    commandType: CommandType.StoredProcedure);

                order.Id = parameters.Get<int>("@Id");

                return order.Id != 0;
            }
        }

        public bool Update(Order order)
        {
            using (IDbConnection connection = this.factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;

                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@Id", order.Id);
                parameters.Add("@State", order.State);

                connection.Open();

                return connection.Execute(
                    sql: "Order_Update",
                    param: parameters,
                    commandType: CommandType.StoredProcedure) > 0;
            }
        }
    }
}