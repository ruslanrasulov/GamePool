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
    public sealed class ImageDAO : IImageDAO
    {
        private readonly string connectionString;
        private readonly DbProviderFactory factory;

        public ImageDAO(string connectionString, string providerName)
        {
            this.connectionString = connectionString;
            this.factory = DbProviderFactories.GetFactory(providerName);
        }

        public bool Add(ImageEntity imageEntity)
        {
            using (IDbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;

                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@Id", imageEntity.Id, direction: ParameterDirection.Output);
                parameters.Add("@Content", imageEntity.Content);
                parameters.Add("@MimeType", imageEntity.MimeType);
                parameters.Add("@AlternativeText", imageEntity.AlternativeText);

                connection.Open();

                connection.Execute(
                    sql: "Image_Add",
                    param: parameters,
                    commandType: CommandType.StoredProcedure);

                imageEntity.Id = parameters.Get<int>("@Id");

                return imageEntity.Id != 0;
            }
        }

        public ImageEntity GetById(int id)
        {
            using (IDbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;

                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@Id", id);

                connection.Open();

                return connection.QuerySingleOrDefault<ImageEntity>(
                    sql: "Image_GetById",
                    param: parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public bool Remove(int id)
        {
            using (IDbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;

                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@Id", id);

                connection.Open();

                return connection.Execute(
                    sql: "Image_Remove",
                    param: parameters,
                    commandType: CommandType.StoredProcedure) > 0;
            }
        }
    }
}