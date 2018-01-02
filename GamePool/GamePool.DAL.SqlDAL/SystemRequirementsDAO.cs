using System.Data;
using Dapper;
using GamePool.Common.Entities;
using GamePool.DAL.DALContracts;

namespace GamePool.DAL.SqlDAL
{
    public sealed class SystemRequirementsDao : BaseDao, ISystemRequirementsDao
    {
        public SystemRequirementsDao(string connectionString, string providerName)
            :base(connectionString, providerName)
        {
        }

        public bool Add(SystemRequirements systemRequirements)
        {
            using (var connection = GetConnection())
            {
                var parameters = new DynamicParameters();

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
                    "SystemRequirements_Add",
                    parameters,
                    commandType: CommandType.StoredProcedure);

                systemRequirements.Id = parameters.Get<int>("@Id");

                return systemRequirements.Id != 0;
            }
        }

        public bool Update(SystemRequirements systemRequirements)
        {
            using (var connection = GetConnection())
            {
                var parameters = new DynamicParameters();

                parameters.Add("@Id", systemRequirements.Id);
                parameters.Add("@Processor", systemRequirements.Processor);
                parameters.Add("@Memory", systemRequirements.Memory);
                parameters.Add("@Graphics", systemRequirements.Graphics);
                parameters.Add("@OperationSystem", systemRequirements.OperationSystem);
                parameters.Add("@DirectX", systemRequirements.DirectX);
                parameters.Add("@Storage", systemRequirements.Storage);

                connection.Open();

                return connection.Execute(
                    "SystemRequirements_Update",
                    parameters,
                    commandType: CommandType.StoredProcedure) > 0;
            }
        }

        public SystemRequirements GetById(int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                return connection.QueryFirstOrDefault<SystemRequirements>(
                    "SystemRequirements_GetById",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}