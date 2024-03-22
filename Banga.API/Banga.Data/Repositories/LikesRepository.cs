using Banga.Domain.Interfaces.Repositories;
using Banga.Domain.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Banga.Data.Repositories
{
    public class LikesRepository : ILikesRepository
    {
        private readonly IConfiguration _configuration;
        public LikesRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<long> CreateLike(Likes likes)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return await connection.ExecuteScalarAsync<long>(@"
                       INSERT INTO [dbo].[Likes]
                            (  [PropertyId]
                              ,[UserId]
                              ,[IsLiked]
                            )
                        VALUES
        	                (
                               @PropertyId
                              ,@UserId
                              ,@IsLiked
                            )
                        Select SCOPE_IDENTITY()", new
                {
                    likes.PropertyId,
                    likes.UserId,
                    likes.IsLiked

                });
            }
        }

        public async Task UpdateLike(Likes likes)
        {
            const string sql = @"
               UPDATE 
                   [dbo].[Likes]
               SET 
                    [IsLiked] =@IsLiked

               WHERE
                   [LikeId] = @LikeId";

            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync(sql, new
            {
                likes.IsLiked,
                likes.LikeId

            });
        }

        public async Task UpdatePropertyLikes(long propertyId, int numberOfLikes)
        {
            const string sql = @"
               UPDATE 
                   [dbo].[Property]
               SET 
                    [NumberOfLikes] = @numberOfLikes
               WHERE
                   [PropertyId] = @propertyId";

            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync(sql, new
            {
                propertyId,
                numberOfLikes

            });
        }

        public async Task<Likes> UserHasLiked(long propertyId, int userId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var sql = @"
                            SELECT
                                * 
                            FROM [Likes] 
                            WHERE 
                                [PropertyId] = @propertyId
                                AND [UserId] = @userId";
                 return await connection.QueryFirstOrDefaultAsync<Likes>(sql, new { propertyId, userId });
            }
        }
    }
}
