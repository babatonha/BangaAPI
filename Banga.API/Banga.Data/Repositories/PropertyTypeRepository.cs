﻿using Banga.Data.Models;
using Banga.Domain.Interfaces.Repositories;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Banga.Data.Repositories
{
    public class PropertyTypeRepository : IPropertyTypeRepository
    {
        private readonly IConfiguration _configuration;
        public PropertyTypeRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<PropertyType>> GetPropertyTypes()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var sql = @"
                        SELECT
                            * 
                        FROM [PropertyType] ";
                return await connection.QueryAsync<PropertyType>(sql, new { });
            }
        }
    }
}
