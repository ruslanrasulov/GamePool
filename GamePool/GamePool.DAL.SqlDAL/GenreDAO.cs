using System.Collections.Generic;
using System.Data;
using Dapper;
using GamePool.Common.Entities;
using GamePool.DAL.DALContracts;
using Newtonsoft.Json;

namespace GamePool.DAL.SqlDAL
{
    public class GenreDao : BaseDao, IGenreDao
    {
        public GenreDao(string connectionString, string providerName)
            :base(connectionString, providerName)
        {
        }

        public bool Add(Genre genre)
        {
            using (var connection = GetConnection())
            {
                var parameters = new DynamicParameters();

                parameters.Add("@Id", genre.Id, direction: ParameterDirection.Output);
                parameters.Add("@Name", genre.Name);

                connection.Open();

                connection.Execute(
                    "Genre_Add",
                    parameters,
                    commandType: CommandType.StoredProcedure);

                genre.Id = parameters.Get<int>("@Id");

                return genre.Id != 0;
            }
        }

        public bool AddGenresByGameId(int gameId, IEnumerable<int> ids)
        {
            using (var connection = GetConnection())
            {
                var json = JsonConvert.SerializeObject(ids);

                connection.Open();

                return connection.Execute(
                    "GameGenre_AddGenresByGameId",
                    new { GameId = gameId, Ids = json },
                    commandType: CommandType.StoredProcedure) > 0;
            }
        }

        public IEnumerable<Genre> GetByGameId(int gameId)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                return connection.Query<Genre>(
                    "Genre_GetByGameId",
                    new { GameId = gameId },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<Genre> GetByIds (IEnumerable<int> ids)
        {
            using (var connection = GetConnection())
            {
                var json = JsonConvert.SerializeObject(new { ids });

                connection.Open();

                return connection.Query<Genre>(
                    "Genre_GetByIds",
                    new { Ids = json },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<Genre> GetByNamePart(string keyWord)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                return connection.Query<Genre>(
                    "Genre_GetByNamePart",
                    new { KeyWord = keyWord },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public bool RemoveGenresByGameId(int gameId, IEnumerable<int> ids)
        {
            using (var connection = GetConnection())
            {
                var json = JsonConvert.SerializeObject(ids);

                connection.Open();

                return connection.Execute(
                    "GameGenre_RemoveGenresByGameId",
                    new { GameId = gameId, @Ids = json },
                    commandType: CommandType.StoredProcedure) > 0;
            }
        }

        public bool UpdateGenresByGameId(int gameId, IEnumerable<int> ids)
        {
            using (var connection = GetConnection())
            {
                var json = JsonConvert.SerializeObject(ids);

                connection.Open();

                return connection.Execute(
                    "GameGenre_UpdateGenresByGameId",
                    new { GameId = gameId, @Ids = json },
                    commandType: CommandType.StoredProcedure) > 0;
            }
        }
    }
}