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
    }
}
