using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using GamePool.DAL.DALContracts;
using GamePool.Common.Entities;
using Newtonsoft.Json;
using GamePool.DAL.SqlDAL.Helpers;

namespace GamePool.DAL.SqlDAL
{
    public sealed class GameDao : BaseDao, IGameDao
    {
        public GameDao(string connectionString, string providerName)
            :base(connectionString, providerName)
        {
        }

        public bool Add(GameEntity gameEntity)
        {
            using (var connection = GetConnection())
            {
                var parameters = new DynamicParameters();

                parameters.Add("@Id", gameEntity.Id, direction: ParameterDirection.Output);
                parameters.Add("@Name", gameEntity.Name);
                parameters.Add("@Description", gameEntity.Description);
                parameters.Add("@Price", gameEntity.Price);
                parameters.Add("@ReleaseDate", gameEntity.ReleaseDate);

                connection.Open();

                connection.Execute(
                    "Game_Add",
                    parameters,
                    commandType: CommandType.StoredProcedure);

                gameEntity.Id = parameters.Get<int>("@Id");

                return gameEntity.Id != 0;
            }
        }

        public PagedData<GameEntity> GetAll(int pageNumber, int pageSize)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                var multipleQuery = connection.QueryMultiple(
                    "Game_GetAll",
                    new { PageNumber = pageNumber, PageSize = pageSize},
                    commandType: CommandType.StoredProcedure);

                var games = multipleQuery.Read<GameEntity>().ToList();
                var count = multipleQuery.ReadFirst<int>();

                return DataHelper.GetPagedData(games, count);
            }
        }

        public GameEntity GetById(int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                var game = connection.QuerySingleOrDefault<dynamic>(
                    "Game_GetById",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);

                if (game == null)
                {
                    return null;
                }

                return GetGame(game);
            }
        }

        public PagedData<GameEntity> GetByIds(IEnumerable<int> ids)
        {
            using (var connection = GetConnection())
            {
                var json = JsonConvert.SerializeObject(ids);

                connection.Open();

                var query = connection.QueryMultiple(
                    "Game_GetByIds",
                    new { Ids = json },
                    commandType: CommandType.StoredProcedure);

                var gameEntities = MapGames(query);
                var count = query.ReadSingle<int>();

                return DataHelper.GetPagedData(gameEntities, count);
            }
        }

        public bool Remove(int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                return connection.Execute(
                    "Game_Remove",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure) > 0;
            }
        }

        public PagedData<GameEntity> Search(SearchParameters searchParameters)
        {
            using (var connection = GetConnection())
            {
                var parameters = new DynamicParameters();

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
                    "Game_Search",
                    parameters,
                    commandType: CommandType.StoredProcedure);

                var games = multipleQuery.Read<GameEntity>().ToList();
                var count = multipleQuery.ReadFirst<int>();

                return DataHelper.GetPagedData(games, count);
            }
        }

        public bool Update(GameEntity gameEntity)
        {
            using(var connection = GetConnection())
            {
                var parameters = new DynamicParameters();

                parameters.Add("@Id", gameEntity.Id);
                parameters.Add("@Name", gameEntity.Name);
                parameters.Add("@Description", gameEntity.Description);
                parameters.Add("@Price", gameEntity.Price);
                parameters.Add("@ReleaseDate", gameEntity.ReleaseDate);

                connection.Open();

                return connection.Execute(
                    "Game_Update",
                    parameters,
                    commandType: CommandType.StoredProcedure) > 0;
            }
        }

        private IEnumerable<GameEntity> MapGames(SqlMapper.GridReader query)
        {
            var games = query.Read<dynamic>();

            ICollection<GameEntity> gameEntities = new List<GameEntity>();

            foreach (var game in games)
            {
                gameEntities.Add(GetGame(game));
            }

            return gameEntities;
        }

        private static GameEntity GetGame(dynamic game)
        {
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
}