using GamePool.DAL.DALContracts;
using GamePool.Common.Entities;
using System.Data;
using Dapper;
using System.Data.Common;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;

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

                if (game == null)
                {
                    return null;
                }

                return new GameEntity
                {
                    Id = game.Id,
                    AvatarId = game.AvatarId,
                    Name = game.Name,
                    Description = game.Description,
                    ReleaseDate = game.ReleaseDate,
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
                    RecommendedSystemRequirements = new SystemRequirements
                    {
                        Id = game.RecId,
                        GameId = game.RecGameId,
                        Processor = game.RecProcessor,
                        OperationSystem = game.RecOperationSystem,
                        Storage = game.RecStorage,
                        Memory = game.RecMemory,
                        Graphics = game.RecGraphics,
                        DirectX = game.RecDirectX
                    }
                };
            }
        }

        public PagedData<GameEntity> GetByIds(IEnumerable<int> ids)
        {
            using (IDbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;

                var json = JsonConvert.SerializeObject(ids);

                connection.Open();

                var query = connection.QueryMultiple(
                    sql: "Game_GetByIds",
                    param: new { Ids = json },
                    commandType: CommandType.StoredProcedure);

                var games = query.Read<dynamic>();

                List<GameEntity> gameEntities = new List<GameEntity>();

                foreach (var game in games)
                {
                    gameEntities.Add(new GameEntity
                    {
                        Id = game.Id,
                        AvatarId = game.AvatarId,
                        Name = game.Name,
                        Description = game.Description,
                        ReleaseDate = game.ReleaseDate,
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
                        RecommendedSystemRequirements = new SystemRequirements
                        {
                            Id = game.RecId,
                            GameId = game.RecGameId,
                            Processor = game.RecProcessor,
                            OperationSystem = game.RecOperationSystem,
                            Storage = game.RecStorage,
                            Memory = game.RecMemory,
                            Graphics = game.RecGraphics,
                            DirectX = game.RecDirectX
                        }
                    });
                }

                var count = query.ReadSingle<int>();

                return new PagedData<GameEntity>
                {
                    Data = gameEntities,
                    Count = count
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

        public PagedData<GameEntity> Search(SearchParameters searchParameters)
        {
            using (IDbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;

                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@Name", searchParameters.Name);
                parameters.Add("@PriceFrom", searchParameters.PriceFrom);
                parameters.Add("@PriceTo", searchParameters.PriceTo);
                parameters.Add("@ReleaseDate", searchParameters.ReleaseDate);

                var json = (searchParameters.GenreIds == null) ? null : JsonConvert.SerializeObject(new { ids = searchParameters.GenreIds });

                parameters.Add("@GenreIds", json);
                parameters.Add("@PageNumber", searchParameters.PageNumber);
                parameters.Add("@PageSize", searchParameters.PageSize);

                connection.Open();

                var multipleQuery = connection.QueryMultiple(
                    sql: "Game_Search",
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