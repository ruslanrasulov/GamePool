using System.Data;
using Dapper;
using GamePool.Common.Entities;
using GamePool.DAL.DALContracts;
using GamePool.DAL.SqlDAL.Helpers;

namespace GamePool.DAL.SqlDAL
{
    public sealed class UserDao : BaseDao, IUserDao
    {
        public UserDao(string connectionString, string providerName)
            :base(connectionString, providerName)
        {
        }

        public bool Add(UserEntity user)
        {
            using (var connection = GetConnection())
            {
                var parameters = new DynamicParameters();

                parameters.Add("@Id", user.Id, direction: ParameterDirection.Output);
                parameters.Add("@Name", user.Name);
                parameters.Add("@Password", user.Password);

                connection.Open();

                return connection.Execute(
                    "User_Add",
                    parameters,
                    commandType: CommandType.StoredProcedure) > 0;
            }
        }

        public PagedData<UserEntity> GetAll(int pageNumber, int pageSize)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                var query = connection.QueryMultiple(
                    "User_GetAll",
                    new { PageNumber = pageNumber, PageSize = pageSize},
                    commandType: CommandType.StoredProcedure);

                var users = query.Read<UserEntity>();
                var count = query.ReadFirst<int>();

                return DataHelper.GetPagedData(users, count);
            }
        }

        public bool IsExists(UserEntity user)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                return connection.ExecuteScalar(
                    "User_IsExist",
                    new { user.Name, user.Password},
                    commandType: CommandType.StoredProcedure) != null;
            }
        }

        public bool IsLoginExists(string name)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                return connection.ExecuteScalar(
                    "User_IsLoginExists",
                    new { Name = name},
                    commandType: CommandType.StoredProcedure) != null;
            }
        }

        public bool RemoveById(int id)
        {
            return Remove("@Id", id);
        }

        public bool RemoveByName(string name)
        {
            return Remove("@Name", name);
        }

        private bool Remove(string parameterName, object value)
        {
            using (var connection = GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add(parameterName, value);

                connection.Open();

                return connection.Execute(
                    "User_Remove",
                    parameters,
                    commandType: CommandType.StoredProcedure) > 0;
            }
        }
    }
}