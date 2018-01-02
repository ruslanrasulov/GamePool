using System.Data;
using System.Data.Common;

namespace GamePool.DAL.SqlDAL
{
    public abstract class BaseDao
    {
        private readonly string _connectionString;
        private readonly DbProviderFactory _factory;

        protected BaseDao(string connectionString, string providerName)
        {
            _connectionString = connectionString;
            _factory = DbProviderFactories.GetFactory(providerName);
        }

        protected IDbConnection GetConnection()
        {
            var connection = _factory.CreateConnection();

            if (connection != null)
            {
                connection.ConnectionString = _connectionString;
            }

            return connection;
        }
    }
}