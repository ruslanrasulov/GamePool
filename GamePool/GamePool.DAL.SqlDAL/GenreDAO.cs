using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Dapper;
using GamePool.Common.Entities;
using GamePool.DAL.DALContracts;
using Newtonsoft.Json;

namespace GamePool.DAL.SqlDAL
{
    public class GenreDAO : IGenreDAO
    {
        private readonly string connectionString;
        private readonly DbProviderFactory factory;

        public GenreDAO(string connectionString, string providerName)
        {
            this.connectionString = connectionString;
            this.factory = DbProviderFactories.GetFactory(providerName);
        }

        public bool Add(Genre genre)
        {
            using (IDbConnection connection = this.factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;

                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@Id", genre.Id, direction: ParameterDirection.Output);
                parameters.Add("@Name", genre.Name);

                connection.Open();

                connection.Execute(
                    sql: "Genre_AddRange",
                    param: genre,
                    commandType: CommandType.StoredProcedure);

                genre.Id = parameters.Get<int>("@Id");

                return genre.Id != 0;
            }
        }

        public bool AddRange(int gameId, IEnumerable<int> ids)
        {
            using (IDbConnection connection = this.factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;

                var json = JsonConvert.SerializeObject(ids);

                connection.Open();

                return connection.Execute(
                    sql: "GameGenre_AddRange",
                    param: new { GameId = gameId, Ids = json },
                    commandType: CommandType.StoredProcedure) > 0;
            }
        }

        public IEnumerable<Genre> GetByGameId(int gameId)
        {
            using (IDbConnection connection = this.factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;

                connection.Open();

                return connection.Query<Genre>(
                    sql: "Genre_GetByGameId",
                    param: new { GameId = gameId },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<Genre> GetByIds (IEnumerable<int> ids)
        {
            using (IDbConnection connection = this.factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;

                var json = JsonConvert.SerializeObject(ids);

                connection.Open();

                return connection.Query<Genre>(
                    sql: "Genre_AddRange",
                    param: new { Ids = json },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<Genre> GetByNamePart(string keyWord)
        {
            using (IDbConnection connection = this.factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;

                connection.Open();

                return connection.Query<Genre>(
                    sql: "Genre_GetByNamePart",
                    param: new { KeyWord = keyWord },
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}