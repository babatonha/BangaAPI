using Banga.Data.Models;
using Banga.Domain.Interfaces.Repositories;
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

        public async Task<IEnumerable<string>> GetCitySuburbs()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var sql = @"
                       SELECT DISTINCT
                            [Name] 

                            FROM (
	                            SELECT
		                               S.[SuburbId] AS Id
		                              , 'Suburb' AS LocationType
		                              ,S.[Name]
	                              FROM [dbo].[Suburb] S

	                              UNION ALL 

	                              SELECT
		                               C.CityID AS Id
		                              ,'City' AS LocationType
		                              ,C.[Name]  
	                              FROM [dbo].City C
                            ) X

                         ";
                return await connection.QueryAsync<string>(sql, new { });
            }
        }
    }
}
