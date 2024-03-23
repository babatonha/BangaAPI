using Banga.Domain.Interfaces.Repositories;
using Banga.Domain.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Banga.Data.Repositories
{
    public class LawFirmRepository : ILawFirmRepository
    {
        private readonly IConfiguration _configuration;
        public LawFirmRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<LawFirm>> GetLawFirms()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var sql = @"
                        SELECT
                            * 
                        FROM [LawFirm] ";
                return await connection.QueryAsync<LawFirm>(sql, new { });
            }
        }

        public async Task<int> CreateLawFirm(LawFirm firm)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return await connection.ExecuteScalarAsync<int>(@"
                       INSERT INTO [dbo].[LawFirm]
                            (
                                  [LogoUrl]
                                  ,[LawFirmName]
                                  ,[Description]
                                  ,[RepresentativeUserId]
                                  ,[Address]
                                  ,[CityID]
                                  ,[IsDisabled]
                            )
                        VALUES
        	                (
                                  @LogoUrl
                                  ,@LawFirmName
                                  ,@Description
                                  ,@RepresentativeUserId
                                  ,@Address
                                  ,@CityID
                                  ,0
                            )
                        Select SCOPE_IDENTITY()", new
                    {
                        firm.LogoUrl,
                        firm.LawFirmName,
                        firm.Description,
                        firm.RepresentativeUserId,
                        firm.Address,
                        firm.CityID

                });
            }
        }

        public async Task UpdateLawFirm(LawFirm firm)
        {
            const string sql = @"
               UPDATE 
                   [dbo].[LawFirm]
               SET 
               
                     [LogoUrl] = @LogoUrl
                    ,[LawFirmName] = @LawFirmName
                    ,[Description] = @Description
                    ,[RepresentativeUserId]  = @RepresentativeUserId
                    ,[Address] = @Address
                    ,[CityID] = @CityID

               WHERE
                   [LawFirmID] = @LawFirmID";

            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync(sql, new
            {
                firm.LawFirmID,
                firm.LogoUrl,
                firm.LawFirmName,
                firm.Description,
                firm.RepresentativeUserId,
                firm.Address,
                firm.CityID

            });
        }

        public async Task DisableLawFirm(LawFirm firm)
        {
            const string sql = @"
               UPDATE 
                   [dbo].[LawFirm]
               SET 

                    [IsDisabled] = 1

               WHERE
                   [LawFirmID] = @LawFirmID";

            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync(sql, new
            {
                firm.LawFirmID,

            });
        }

        public async  Task<long> CreateLawFirmRating(LawFirmRating rating)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return await connection.ExecuteScalarAsync<int>(@"
                       INSERT INTO [dbo].[LawFirmRating]
                            (  [LawFirmId]
                              ,[UserId]
                              ,[Rating]
                              ,[Review]
                            )
                        VALUES
        	                (
                               @LawFirmId
                              ,@UserId
                              ,@Rating
                              ,@Review
                            )
                        Select SCOPE_IDENTITY()", new
                {
                    rating.LawFirmId,
                    rating.UserId,
                    rating.Rating,
                    rating.Review

                });
            }
        }

        public async  Task<LawFirmRating> GetUserRating(int lawFirmId, int userId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var sql = @"
                        SELECT
                               [RatingId]
                              ,[LawFirmId]
                              ,[UserId]
                              ,[Rating]
                              ,[Review]
                        FROM [LawFirmRating] 
                        WHERE LawFirmId = @lawFirmId
                        AND UserId = @userId";
                return await connection.QueryFirstOrDefaultAsync<LawFirmRating>(sql, new { lawFirmId, userId });
            }
        }

        public async Task UpdateLawFirmTotalRating(int lawFirmId, double ratingTotal)
        {
            const string sql = @"
               UPDATE 
                   [dbo].[LawFirm]
               SET 
                     [TotalRating] = @ratingTotal
               WHERE
                   [LawFirmID] = @lawFirmId";

            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync(sql, new
            {
                lawFirmId, ratingTotal

            });
        }

        public async Task<IEnumerable<LawFirmRating>> GetLawFirmRatings(int lawFirmId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var sql = @"
                             SELECT
                                     R.[RatingId]
                                    ,R.[LawFirmId]
                                    ,R.[UserId]
                                    ,R.[Rating]
                                    ,R.[Review]
                                    ,U.[UserName]
                                    ,U.FirstName + ' ' + U.LastName AS UserFullName
                              FROM [LawFirmRating] R 
                              JOIN [dbo].[AspNetUsers] U ON U.[Id] = R.UserId
                              WHERE LawFirmId = @lawFirmId";
                return await connection.QueryAsync<LawFirmRating>(sql, new { lawFirmId });
            }
        }

        public async Task<LawFirm> GetLawFirmById(int lawFirmId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var sql = @"
                    SELECT
                        * 
                    FROM [LawFirm] 
                    WHERE LawFirmId = @lawFirmId";
                return await connection.QueryFirstOrDefaultAsync<LawFirm>(sql, new { lawFirmId });
            }
        }
    }
}
