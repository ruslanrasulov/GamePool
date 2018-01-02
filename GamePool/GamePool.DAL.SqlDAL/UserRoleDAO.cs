using System.Collections.Generic;
using System.Data;
using Dapper;
using GamePool.Common.Entities;
using GamePool.DAL.DALContracts;

namespace GamePool.DAL.SqlDAL
{
    public sealed class UserRoleDao : BaseDao, IUserRoleDao
    {
        public UserRoleDao(string connectionString, string providerName)
            :base(connectionString, providerName)
        {
        }

        public bool AddRoleToUser(string username, string roleName)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                return connection.Execute(
                    "UserRole_AddRoleToUser",
                    new { UserName = username, RoleName = roleName },
                    commandType: CommandType.StoredProcedure) > 0;
            }
        }

        public IEnumerable<Role> GetAll()
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                return connection.Query<Role>(
                    "Role_GetAll",
                    commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<Role> GetByUserLogin(string username)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                return connection.Query<Role>(
                    "UserRole_GetRolesByUserLogin",
                    new { UserName = username },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public bool IsUserInRole(string username, string roleName)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                return connection.ExecuteScalar(
                    "UserRole_IsUserInRole",
                    new { UserName = username, RoleName = roleName },
                    commandType: CommandType.StoredProcedure) != null;
            }
        }

        public bool RemoveRoleFromUser(string username, string roleName)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                return connection.Execute(
                    "UserRole_RemoveRoleFromUser",
                    new { UserName = username, RoleName = roleName },
                    commandType: CommandType.StoredProcedure) > 0;
            }
        }
    }
}