using System.Data;
using Dapper;
using GamePool.DAL.DALContracts;
using GamePool.Common.Entities;

namespace GamePool.DAL.SqlDAL
{
    public sealed class ImageDao : BaseDao, IImageDao
    {
        public ImageDao(string connectionString, string providerName)
            :base(connectionString, providerName)
        {
        }

        public bool Add(ImageEntity imageEntity)
        {
            using (var connection = GetConnection())
            {
                var parameters = new DynamicParameters();

                parameters.Add("@Id", imageEntity.Id, direction: ParameterDirection.Output);
                parameters.Add("@Path", imageEntity.Path);
                parameters.Add("@MimeType", imageEntity.MimeType);
                parameters.Add("@AlternativeText", imageEntity.AlternativeText);

                connection.Open();

                connection.Execute(
                    "Image_Add",
                    parameters,
                    commandType: CommandType.StoredProcedure);

                imageEntity.Id = parameters.Get<int>("@Id");

                return imageEntity.Id != 0;
            }
        }

        public ImageEntity GetById(int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                return connection.QuerySingleOrDefault<ImageEntity>(
                    "Image_GetById",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public bool Remove(int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                return connection.Execute(
                    "Image_Remove",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure) > 0;
            }
        }

        public bool SetAvatarForGame(int gameId, int imageId)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                return connection.Execute(
                    "Image_SetAvatarForGame",
                    new { GameId = gameId, ImageId = imageId},
                    commandType: CommandType.StoredProcedure) > 0;
            }
        }
    }
}