using Banga.Domain.Interfaces.Repositories;
using Banga.Domain.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Banga.Data.Repositories
{
    public class ViewingRepository : IViewingRepository
    {
        private readonly IConfiguration _configuration;
        public ViewingRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task<long> CreateViewing(Viewing viewing)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return await connection.ExecuteScalarAsync<long>(@"
                       INSERT INTO [dbo].[Viewing]
                            ( [PropertyId]
                              ,[Title]
                              ,[Start]
                              ,[AllocatedTo]
                              ,[StatusId]
                              ,[Note]
                            )
                        VALUES
        	                (
                               @PropertyId
                              ,@Title
                              ,@Start
                              ,@AllocatedTo
                              ,@StatusId
                              ,@Note
                            )
                        Select SCOPE_IDENTITY()", new
                {
                    viewing.PropertyId,
                    viewing.Title,
                    viewing.Start,
                    viewing.AllocatedTo,
                    viewing.StatusId,
                    viewing.Note

                });
            }
        }

        public async Task DeleteViewing(long viewingId)
        {
            const string sql = @"
               DELETE FROM  
                   [dbo].[Viewing]

               WHERE
                   [Id] = @viewingId";

            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync(sql, new
            {
                viewingId
            });
        }

        public async Task<IEnumerable<Viewing>> GetPropertyViewingsByUserId(int userId, long propertyId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var sql = $@"
                            SELECT 
                               V.[Id]
                              ,V.[PropertyId]
                              ,V.[Title]
                              ,V.[Start]
                              ,V.[AllocatedTo]
                              ,V.[StatusId]
                              ,V.[Note]
                          FROM [Viewing]  V
                          JOIN [dbo].[Property] P ON P.[PropertyId]  = V.[PropertyId]
                          WHERE P.[OwnerID] = @userId
                           AND P.[PropertyId]  = @propertyId";

                return await connection.QueryAsync<Viewing>(sql, new
                {
                    userId,
                    propertyId
                });
            }
        }

        public async Task UpdateViewing(Viewing viewing)
        {
            const string sql = @"
               UPDATE 
                   [dbo].[Viewing]
               SET 
                    [PropertyId] = @PropertyId
                    ,[Title] = @Title
                    ,[Start] = @Start
                    ,[AllocatedTo] = @AllocatedTo
                    ,[StatusId] = @StatusId
                    ,[IsConfirmed] = @IsConfirmed
                    ,[Note] = @Note

               WHERE
                   [Id] = @ViewingId";

            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync(sql, new
            {
                viewing.PropertyId,
                viewing.Title,
                viewing.Start,
                viewing.AllocatedTo,
                viewing.StatusId,
                viewing.Note,
                viewing.Id,

            });
        }
    }
}
