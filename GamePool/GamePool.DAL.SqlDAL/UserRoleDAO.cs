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
    public sealed class UserRoleDAO : IUserRoleDAO
    {
        private readonly string connectionString;
        private readonly DbProviderFactory factory;

        public UserRoleDAO(string connectionString, string providerName)
        {
            this.connectionString = connectionString;
            this.factory = DbProviderFactories.GetFactory(providerName);
        }

        public bool AddRoleToUser(string username, string roleName)
        {
            using (IDbConnection connection = this.factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;

                connection.Open();

                return connection.Execute(
                    sql: "UserRole_AddRoleToUser",
                    param: new { UserName = username, RoleName = roleName },
                    commandType: CommandType.StoredProcedure) > 0;
            }
        }

        public IEnumerable<Role> GetAll()
        {
            using (IDbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;

                connection.Open();

                return connection.Query<Role>(
                    sql: "Role_GetAll",
                    commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<Role> GetByUserLogin(string username)
        {
            using (IDbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;

                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@UserName", username);

                connection.Open();

                return connection.Query<Role>(
                    sql: "UserRole_GetRolesByUserLogin",
                    param: parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public bool IsUserInRole(string username, string roleName)
        {
            using (IDbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;

                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@UserName", username);
                parameters.Add("@RoleName", roleName);

                connection.Open();

                return connection.ExecuteScalar(
                    sql: "UserRole_IsUserInRole",
                    param: parameters,
                    commandType: CommandType.StoredProcedure) != null;
            }
        }

        public bool RemoveRoleFromUser(string username, string roleName)
        {
            using (IDbConnection connection = this.factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;

                connection.Open();

                return connection.Execute(
                    sql: "UserRole_RemoveRoleFromUser",
                    param: new { UserName = username, RoleName = roleName },
                    commandType: CommandType.StoredProcedure) > 0;
            }
        }
    }
}