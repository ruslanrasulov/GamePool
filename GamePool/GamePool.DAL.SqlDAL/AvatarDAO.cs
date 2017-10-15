using Dapper;
using GamePool.DAL.DALContracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePool.DAL.SqlDAL
{
    public sealed class AvatarDAO : IAvatarDAO
    {
        private readonly string connectionString;
        private readonly DbProviderFactory factory;

        public AvatarDAO(string connectionString, string providerName)
        {
            this.connectionString = connectionString;
            this.factory = DbProviderFactories.GetFactory(providerName);
        }

        public bool SetForGame(int gameId, int imageId)
        {
            using (IDbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;

                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@GameId", gameId);
                parameters.Add("@ImageId", imageId);

                connection.Open();

                return connection.Execute(
                    sql: "Image_SetAvatarForGame",
                    param: parameters,
                    commandType: CommandType.StoredProcedure) > 0;
            }
        }
    }
}