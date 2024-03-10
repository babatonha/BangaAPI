using Banga.Data.Models;
using Banga.Domain.Interfaces.Repositories;
using Banga.Domain.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Banga.Data.Repositories
{
    public class PropertyLocationRepository : IPropertyLocationRepository
    {
        private readonly IConfiguration _configuration;
        public PropertyLocationRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<City>> GetCities()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var sql = @"
                        SELECT
                            * 
                        FROM [City] ";
                return await connection.QueryAsync<City>(sql, new { });
            }
        }
    }
}
