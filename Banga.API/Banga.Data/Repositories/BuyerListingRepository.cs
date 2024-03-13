using Banga.Domain.Interfaces.Repositories;
using Banga.Domain.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Banga.Data.Repositories
{
    public class BuyerListingRepository : IBuyerListingRepository
    {
        private readonly IConfiguration _configuration;
        public BuyerListingRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<long> CreateBuyerListing(BuyerListing buyerListing)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return await connection.ExecuteScalarAsync<int>(@"
                       INSERT INTO [dbo].[BuyerListing]
                            (
                               [UserId]
                              ,[Budget]
                              ,[Description]
                              ,[CreatedDate]
                              ,[IsDisabled]
                            )
                        VALUES
        	                (
                                @UserId
                                ,@Budget
                                ,@Description
                                ,GETDATE()
                                ,0
                            )
                        Select SCOPE_IDENTITY()", new
                {
                    buyerListing.UserId,
                    buyerListing.Budget,
                    buyerListing.Description
                });
            }
        }

        public async Task DeleteBuyerListing(long buyerListingId)
        {
            const string sql = @"
               UPDATE 
                   [dbo].[BuyerListing]
               SET 
                    [IsDisabled] = 1

               WHERE
                   [BuyerListingId] = @buyerListingId";

            using var connection = new SqlConnection(_configuration.GetConnectionString("SqlConnection"));
            await connection.ExecuteAsync(sql, new
            {
                buyerListingId
            });
        }

        public async Task<IEnumerable<BuyerListing>> GetBuyerListing()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var sql = @"
                        SELECT
                            * 
                        FROM [BuyerListing] ";
                return await connection.QueryAsync<BuyerListing>(sql, new { });
            }
        }

        public async Task UpdateBuyerListing(BuyerListing buyerListing)
        {
            const string sql = @"
               UPDATE 
                   [dbo].[BuyerListing]
               SET 
                     [UserId] = @UserId
                    ,[Budget] = @Budget
                    ,[Description] = @Description
                    ,[CreatedDate] = @CreatedDate
                    ,[IsDisabled] = @IsDisabled

               WHERE
                   [BuyerListingId] = @BuyerListingId";

            using var connection = new SqlConnection(_configuration.GetConnectionString("SqlConnection"));
            await connection.ExecuteAsync(sql, new
            {
                buyerListing.UserId,
                buyerListing.Budget,
                buyerListing.Description,
                buyerListing.BuyerListingId,
                buyerListing.IsDisabled
            });
        }
    }
}
