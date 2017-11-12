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
    public sealed class UserDAO : IUserDAO
    {
        private readonly string connectionString;
        private readonly DbProviderFactory factory;

        public UserDAO(string connectionString, string providerName)
        {
            this.connectionString = connectionString;
            this.factory = DbProviderFactories.GetFactory(providerName);
        }

        public bool Add(User user)
        {
            using (IDbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;

                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@Id", user.Id, direction: ParameterDirection.Output);
                parameters.Add("@Name", user.Name);
                parameters.Add("@Password", user.Password);

                connection.Open();

                return connection.Execute(
                    sql: "User_Add",
                    param: parameters,
                    commandType: CommandType.StoredProcedure) > 0;
            }
        }

        public bool IsExists(User user)
        {
            using (IDbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;

                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@Name", user.Name);
                parameters.Add("@Password", user.Password);

                connection.Open();

                return connection.ExecuteScalar(
                    sql: "User_IsExist",
                    param: parameters,
                    commandType: CommandType.StoredProcedure) != null;
            }
        }

        public bool IsLoginExists(string name)
        {
            using (IDbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;

                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@Name", name);

                connection.Open();

                return connection.ExecuteScalar(
                    sql: "User_IsLoginExists",
                    param: parameters,
                    commandType: CommandType.StoredProcedure) != null;
            }
        }

        public bool RemoveById(int id)
        {
            return this.Remove("@Id", id);
        }

        public bool RemoveByName(string name)
        {
            return this.Remove("@Name", name);
        }

        private bool Remove(string parameterName, object value)
        {
            using (IDbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;

                DynamicParameters parameters = new DynamicParameters();

                parameters.Add(parameterName, value);

                connection.Open();

                return connection.Execute(
                    sql: "User_Remove",
                    param: parameters,
                    commandType: CommandType.StoredProcedure) > 0;
            }
        }
    }
}