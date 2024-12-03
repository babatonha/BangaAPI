using Banga.Data.Queries;
using Banga.Domain.Interfaces.Repositories;
using Banga.Domain.Models;
using CloudinaryDotNet.Actions;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata;
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
                              ,[ViewingDate]
                              ,[AllocatedTo]
                              ,[ViewingStatus]
                              ,[IsConfirmed]
                              ,[Note]
                            )
                        VALUES
        	                (
                               @PropertyId
                              ,@ViewingDate
                              ,@AllocatedTo
                              ,@ViewingStatus
                              ,@IsConfirmed
                              ,@Note
                            )
                        Select SCOPE_IDENTITY()", new
                {
                    viewing.PropertyId,
                    viewing.ViewingDate,
                    viewing.AllocatedTo,
                    viewing.ViewingStatus,
                    viewing.IsConfirmed,
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
                   [ViewingId] = @viewingId";

            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync(sql, new
            {
                viewingId
            });
        }

        public async Task<IEnumerable<Viewing>> GetViewingsByUserId(int userId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var sql = $@"
                            SELECT 
                               V.[ViewingId]
                              ,V.[PropertyId]
                              ,V.[ViewingDate]
                              ,V.[AllocatedTo]
                              ,V.[ViewingStatus]
                              ,V.[IsConfirmed]
                              ,V.[Note]
                          FROM [Viewing]  V
                          JOIN [dbo].[Property] P ON P.[PropertyId]  = V.[PropertyId]
                          WHERE P.[OwnerID] = @userId";

                return await connection.QueryAsync<Viewing>(sql, new
                {
                    userId
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
                    ,[ViewingDate] = @ViewingDate
                    ,[AllocatedTo] = @AllocatedTo
                    ,[ViewingStatus] = @ViewingStatus
                    ,[IsConfirmed] = @IsConfirmed
                    ,[Note] = @Note

               WHERE
                   [ViewingId] = @ViewingId";

            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync(sql, new
            {
                viewing.PropertyId,
                viewing.ViewingDate,
                viewing.AllocatedTo,
                viewing.ViewingStatus,
                viewing.IsConfirmed,
                viewing.Note,
                viewing.ViewingId,

            });
        }
    }
}
