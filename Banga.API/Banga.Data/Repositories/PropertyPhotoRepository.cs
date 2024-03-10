using Banga.Data.Models;
using Banga.Domain.Interfaces.Repositories;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Banga.Data.Repositories
{
    public class PropertyPhotoRepository : IPropertyPhotoRepository
    {

        private readonly IConfiguration _configuration;
        public PropertyPhotoRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<long> CreatePropertyPhoto(PropertyPhoto photo)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return await connection.ExecuteScalarAsync<long>(@"
                       INSERT INTO [dbo].[PropertyPhoto]
                            (
                                   [PropertyID]
                                  ,[PhotoUrl]
                                  ,[PublicID]
                                  ,[IsMainPhoto]
                            )
                        VALUES
        	                (
                                   @PropertyID
                                  ,@PhotoUrl
                                  ,@PublicID
                                  ,@IsMainPhoto
                            )
                        Select SCOPE_IDENTITY()", new
                    {

                         photo.PropertyId
                        ,photo.PhotoUrl
                        ,photo.PublicID
                        ,photo.IsMainPhoto

                });
            }
        }

        public async Task UpdatePropertyPhoto(PropertyPhoto photo)
        {
                 const string sql = @"
               UPDATE 
                   [dbo].[PropertyPhoto]
               SET 
                    [PropertyID] = @PropertyID
                    ,[PhotoUrl] =@PhotoUrl
                    ,[PublicID] = @PublicID
                    ,[IsMainPhoto] = @IsMainPhoto

               WHERE
                   [PropertyPhotoId] = @PropertyPhotoId";

            using var connection = new SqlConnection(_configuration.GetConnectionString("SqlConnection"));
            await connection.ExecuteAsync(sql, new
            {
                photo.PropertyPhotoId
                ,photo.PropertyId
                ,photo.PhotoUrl
                ,photo.PublicID
                ,photo.IsMainPhoto
            });
        }

        public async Task DeletePropertyPhoto(long propertyPhotoId)
        {
            const string sql = @"
               DELETE FROM  
                   [dbo].[PropertyPhoto]
               WHERE
                   [PropertyPhotoId] = @PropertyPhotoId";

            using var connection = new SqlConnection(_configuration.GetConnectionString("SqlConnection"));
            await connection.ExecuteAsync(sql, new
            {
                propertyPhotoId
            });
        }

        public async Task<IEnumerable<PropertyPhoto>> GetPropertyPhotosByPropertyId(long propertyId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var sql = @"
                            SELECT 
                                 *                    
                              FROM [dbo].[PropertyPhoto] 
                              WHERE PropertyID = @propertyId";

                return await connection.QueryAsync<PropertyPhoto>(sql, new { propertyId });
            }
        }

    }
}
