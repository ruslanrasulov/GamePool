using System;
using System.Data;
using System.Data.Common;
using Dapper;
using GamePool.Common.Entities;
using GamePool.DAL.DALContracts;

namespace GamePool.DAL.SqlDAL
{
    public sealed class RatingDAO : IRatingDAO
    {
        private readonly string connectionString;
        private readonly DbProviderFactory factory;

        public RatingDAO(string connectionString, string providerName)
        {
            this.connectionString = connectionString;
            this.factory = DbProviderFactories.GetFactory(providerName);
        }

        public bool Add(Rating rating)
        {
            using (IDbConnection connection = this.factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;

                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@Id", rating.Id, direction: ParameterDirection.Output);
                parameters.Add("@Date", rating.Date);
                parameters.Add("@Value", rating.Value);
                parameters.Add("@GameId", rating.GameId);
                parameters.Add("@UserId", rating.UserId);

                connection.Open();

                connection.Execute(
                    sql: "GameRating_Add",
                    param: parameters,
                    commandType: CommandType.StoredProcedure);

                rating.Id = parameters.Get<int>("@Id");

                return rating.Id != 0;
            }
        }

        public bool Update(Rating rating)
        {
            using (IDbConnection connection = this.factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;

                DynamicParameters parameters = new DynamicParameters();
                
                parameters.Add("@Id", rating.Id);
                parameters.Add("@Value", rating.Value);
                parameters.Add("@Date", rating.Date);

                connection.Open();

                return connection.Execute(
                    sql: "GameRating_Update",
                    param: parameters,
                    commandType: CommandType.StoredProcedure) > 0;
            }
        }
    }
}