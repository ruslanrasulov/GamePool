using GamePool.DAL.DALContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamePool.Common.Entities;
using System.Data;
using Dapper;
using System.Data.Common;

namespace GamePool.DAL.SqlDAL
{
    public sealed class GameDAO : IGameDAO
    {
        private readonly string connectionString;
        private readonly DbProviderFactory factory;

        public GameDAO(string connectionString, string providerName)
        {
            this.connectionString = connectionString;
            this.factory = DbProviderFactories.GetFactory(providerName);
        }

        public bool Add(GameEntity gameEntity)
        {
            using (IDbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;

                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@Id", gameEntity.Id, direction: ParameterDirection.Output);
                parameters.Add("@AvatarId", gameEntity.AvatarId);
                parameters.Add("@Name", gameEntity.Name);
                parameters.Add("@Description", gameEntity.Description);
                parameters.Add("@Price", gameEntity.Price);
                parameters.Add("@ReleaseDate", gameEntity.ReleaseDate);

                connection.Open();

                connection.Execute(
                    sql: "Game_Add",
                    param: parameters,
                    commandType: CommandType.StoredProcedure);

                gameEntity.Id = parameters.Get<int>("@Id");

                return gameEntity.Id != 0;
            }
        }

        public IEnumerable<GameEntity> GetAll(int pageNumber, int pageSize)
        {
            using (IDbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;

                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@PageNumber", pageNumber);
                parameters.Add("@PageSize", pageSize);

                connection.Open();

                return connection.Query<GameEntity>(
                    sql: "Game_GetAll",
                    param: parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public GameEntity GetById(int id)
        {
            using (IDbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;

                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@Id", id);

                connection.Open();

                return connection.QuerySingleOrDefault<GameEntity>(
                    sql: "Game_GetById",
                    param: parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public bool Remove(int id)
        {
            using (IDbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;

                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@Id", id);

                connection.Open();

                return connection.Execute(
                    sql: "Game_Remove",
                    param: parameters,
                    commandType: CommandType.StoredProcedure) > 0;
            }
        }

        public bool Update(GameEntity gameEntity)
        {
            using(IDbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;

                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@Id", gameEntity.Id);
                parameters.Add("@AvatarId", gameEntity.AvatarId);
                parameters.Add("@Name", gameEntity.Name);
                parameters.Add("@Description", gameEntity.Description);
                parameters.Add("@Price", gameEntity.Price);
                parameters.Add("@ReleaseDate", gameEntity.ReleaseDate);

                connection.Open();

                return connection.Execute(
                    sql: "Game_Update",
                    param: parameters,
                    commandType: CommandType.StoredProcedure) > 0;
            }
        }
    }
}