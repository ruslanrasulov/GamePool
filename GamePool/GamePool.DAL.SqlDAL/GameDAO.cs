using GamePool.DAL.DALContracts;
using GamePool.Common.Entities;
using System.Data;
using Dapper;
using System.Data.Common;
using System.Linq;

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

        public PagedData<GameEntity> GetAll(int pageNumber, int pageSize)
        {
            using (IDbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;

                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@PageNumber", pageNumber);
                parameters.Add("@PageSize", pageSize);

                connection.Open();

                var multipleQuery = connection.QueryMultiple(
                    sql: "Game_GetAll",
                    param: parameters,
                    commandType: CommandType.StoredProcedure);

                var games = multipleQuery.Read<GameEntity>().ToList();
                var count = multipleQuery.ReadFirst<int>();

                return new PagedData<GameEntity>
                {
                    Data = games,
                    Count = count
                };
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

                var game = connection.QuerySingleOrDefault<dynamic>(
                    sql: "Game_GetById",
                    param: parameters,
                    commandType: CommandType.StoredProcedure);

                return new GameEntity
                {
                    Id = game.Id,
                    AvatarId = game.AvatarId,
                    Name = game.Name,
                    Description = game.Description,
                    Price = game.Price,
                    MinimalSystemRequirements = new SystemRequirements
                    {
                        Id = game.MinId,
                        GameId = game.MinGameId,
                        Processor = game.MinProcessor,
                        OperationSystem = game.MinOperationSystem,
                        Storage = game.MinStorage,
                        Memory = game.MinMemory,
                        Graphics = game.MinGraphics,
                        DirectX = game.MinDirectX
                    },
                    ReccomendedSystemRequirements = new SystemRequirements
                    {
                        Id = game.RecId,
                        GameId = game.RecGameId,
                        Processor = game.RecProcessor,
                        OperationSystem = game.RecOperationSystem,
                        Storage = game.RecStorage,
                        Memory = game.RecMemory,
                        Graphics = game.RecGraphics,
                        DirectX = game.RecnDirectX
                    }
                };
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