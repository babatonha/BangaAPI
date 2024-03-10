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
    }
}
