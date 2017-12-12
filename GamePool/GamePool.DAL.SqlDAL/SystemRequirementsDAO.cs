using GamePool.DAL.DALContracts;
using GamePool.Common.Entities;
using System.Data;
using Dapper;
using System.Data.Common;

namespace GamePool.DAL.SqlDAL
{
    public sealed class SystemRequirementsDAO : ISystemRequirementsDAO
    {
        private readonly string connectionString;
        private readonly DbProviderFactory factory;

        public SystemRequirementsDAO(string connectionString, string providerName)
        {
            this.connectionString = connectionString;
            this.factory = DbProviderFactories.GetFactory(providerName);
        }

        public bool Add(SystemRequirements systemRequirements)
        {
            using (IDbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;

                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@Id", systemRequirements.Id, direction: ParameterDirection.Output);
                parameters.Add("@GameId", systemRequirements.GameId);
                parameters.Add("@Processor", systemRequirements.Processor);
                parameters.Add("@Memory", systemRequirements.Memory);
                parameters.Add("@Graphics", systemRequirements.Graphics);
                parameters.Add("@OperationSystem", systemRequirements.OperationSystem);
                parameters.Add("@DirectX", systemRequirements.DirectX);
                parameters.Add("@Storage", systemRequirements.Storage);
                parameters.Add("@Type", systemRequirements.Type);

                connection.Open();

                connection.Execute(
                    sql: "SystemRequirements_Add",
                    param: parameters,
                    commandType: CommandType.StoredProcedure);

                systemRequirements.Id = parameters.Get<int>("@Id");

                return systemRequirements.Id != 0;
            }
        }

        public bool Update(SystemRequirements systemRequirements)
        {
            using (IDbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;

                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@Id", systemRequirements.Id);
                parameters.Add("@Processor", systemRequirements.Processor);
                parameters.Add("@Memory", systemRequirements.Memory);
                parameters.Add("@Graphics", systemRequirements.Graphics);
                parameters.Add("@OperationSystem", systemRequirements.OperationSystem);
                parameters.Add("@DirectX", systemRequirements.DirectX);
                parameters.Add("@Storage", systemRequirements.Storage);

                connection.Open();

                return connection.Execute(
                    sql: "SystemRequirements_Update",
                    param: parameters,
                    commandType: CommandType.StoredProcedure) > 0;
            }
        }

        public SystemRequirements GetById(int id)
        {
            using (IDbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;

                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@Id", id);

                connection.Open();

                return connection.QueryFirstOrDefault<SystemRequirements>(
                    sql: "SystemRequirements_GetById",
                    param: parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}