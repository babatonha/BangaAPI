using Banga.Data.Models;
using Banga.Data.Queries;
using Banga.Domain.DTOs;
using Banga.Domain.Interfaces.Repositories;
using CloudinaryDotNet.Actions;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Banga.Data.Repositories
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly IConfiguration _configuration;
        public PropertyRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<long> CreateProperty(Property property)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return await connection.ExecuteScalarAsync<long>(@"
                       INSERT INTO [dbo].[Property]
                            (
                                 [OwnerID]
                                ,[AssignedLawyerID]
                                ,[PropertyTypeID]
                                ,[RegistrationTypeId]
                                ,[StatusID]
                                ,[SuburbId]
                                ,[Amenities]
                                ,[CityID]
                                ,[ProvinceID]
                                ,[Address]
                                ,[Price]
                                ,[Description]
                                ,[NumberOfRooms]
                                ,[NumberOfBathrooms]
                                ,[ParkingSpots]
                                ,[ThumbnailUrl]
                                ,[YoutubeUrl]
                                ,[HasLawyer]
                                ,[NumberOfLikes]
                                ,[SquareMetres]
                                ,[CreatedDate]
                                ,[IsSold] 
                                ,[IsDeleted] 
                                ,[IsActive]
                                ,[StatusID]
                            )
                        VALUES
        	                (
                                 @OwnerID
                                ,@AssignedLawyerID
                                ,@PropertyTypeID
                                ,@RegistrationTypeId
                                ,@StatusID
                                ,@SuburbId
                                ,@Amenities
                                ,@CityID
                                ,@ProvinceID
                                ,@Address
                                ,@Price
                                ,@Description
                                ,@NumberOfRooms
                                ,@NumberOfBathrooms
                                ,@ParkingSpots
                                ,@ThumbnailUrl
                                ,@YoutubeUrl
                                ,@HasLawyer
                                ,@NumberOfLikes
                                ,@SquareMetres
                                ,GETDATE()
                                ,0
                                ,0
                                ,0
                                ,1
                            )
                        Select SCOPE_IDENTITY()", new
                    {
                         property.OwnerId
                        ,property.AssignedLawyerId
                        ,property.PropertyTypeId
                        ,property.RegistrationTypeId
                        ,property.StatusID
                        ,property.CityId
                        ,property.ProvinceId
                        ,property.Address
                        ,property.Price
                        ,property.Description
                        ,property.NumberOfRooms
                        ,property.NumberOfBathrooms
                        ,property.ParkingSpots
                        ,property.ThumbnailUrl
                        ,property.YoutubeUrl
                        ,property.HasLawyer
                        ,property.NumberOfLikes
                        ,property.SquareMetres
                        ,property.Amenities
                        ,property.SuburbId

                    });
            }
        }

        public async Task UpdateProperty(Property property)
        {
            const string sql = @"
               UPDATE 
                   [dbo].[Property]
               SET 
                 [OwnerID] = @OwnerID
                ,[AssignedLawyerID] = @AssignedLawyerID
                ,[PropertyTypeID] = @PropertyTypeID
                ,[RegistrationTypeId] = @RegistrationTypeId
                ,[StatusID] = @StatusID
                ,[SuburbId] = @SuburbId
                ,[Amenities] = @Amenities
                ,[CityID] =  @CityID
                ,[ProvinceID] = @ProvinceID
                ,[Address] = @Address
                ,[Price] = @Price
                ,[Description] = @Description
                ,[NumberOfRooms] = @NumberOfRooms
                ,[NumberOfBathrooms] = @NumberOfBathrooms
                ,[ParkingSpots] = @ParkingSpots
                ,[ThumbnailUrl] = @ThumbnailUrl
                ,[YoutubeUrl] = @YoutubeUrl
                ,[HasLawyer] = @HasLawyer
                ,[NumberOfLikes]  =@NumberOfLikes
                ,[SquareMetres] = @SquareMetres
                ,[LastUpdatedDate] = GETDATE()
                ,[IsSold] = @IsSold
                ,[IsDeleted] = @IsDeleted
                ,[IsActive] = @IsActive

               WHERE
                   [PropertyId] = @PropertyId";

            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync(sql, new
            {
                property.PropertyId
                ,property.OwnerId
                ,property.AssignedLawyerId
                ,property.PropertyTypeId
                ,property.RegistrationTypeId
                ,property.StatusID
                ,property.CityId
                ,property.ProvinceId
                ,property.Address
                ,property.Price
                ,property.Description
                ,property.NumberOfRooms
                ,property.NumberOfBathrooms
                ,property.ParkingSpots
                ,property.ThumbnailUrl
                ,property.YoutubeUrl
                ,property.HasLawyer
                ,property.NumberOfLikes
                ,property.SquareMetres
                ,property.IsActive  
                ,property.IsSold 
                ,property.IsDeleted
                ,property.Amenities
                ,property.SuburbId
            });
        }

        public async Task<IEnumerable<Property>> GetProperties(SearchFilterDTO searchFilter)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                string searchTerms = string.Join(", ", searchFilter.SearchTerms.Select(name => $"'{name}'"));

                var sql = $@"
                             SELECT 
                                    P.[PropertyID]
                                   ,P.[OwnerID]
                                   ,OU.FirstName + ' ' + OU.LastName AS OwnerName
                                   ,P.[AssignedLawyerID]
                                   ,OL.FirstName + ' ' + OL.LastName AS AssignedLawyerName
                                   ,P.[PropertyTypeID]
                                   ,PT.[Name] AS PropertyTypeName
                                   ,P.[StatusID]
                                   ,S.[Name]  AS StatusName
                                   ,P.[SuburbId]
                                   ,SB.Name AS SuburbName
                                   ,P.[CityID]
                                   ,C.[Name] AS CityName
                                   ,P.[ProvinceID]
                                   ,PR.[Name] AS ProvinceName
                                   ,P.[Address]
                                   ,P.[Price]
                                   ,P.[Description]
                                   ,P.[NumberOfRooms]
                                   ,P.[NumberOfBathrooms]
                                   ,P.[ParkingSpots]
                                   ,P.[ThumbnailUrl]
                                   ,P.[YoutubeUrl]
                                   ,P.[HasLawyer]
                                   ,P.[NumberOfLikes] 
                                   ,P.[SquareMetres]
                                   ,P.[Amenities]
		                           ,RT.[Name] AS RegistrationTypeName
                                   ,P.[CreatedDate]
                                   ,P.[LastUpdatedDate]
                                   ,P.[IsSold]  
                                   ,P.[IsActive]
                                   ,P.[IsDeleted]
                               FROM [dbo].[Property] P 
                               JOIN AspNetUsers OU ON OU.Id = P.OwnerID
                               JOIN PropertyType PT ON PT.PropertyTypeID = P.PropertyTypeID
	                           JOIN RegistrationType RT ON RT.RegistrationTypeId = P.RegistrationTypeId
                               JOIN AspNetUsers OL ON OL.Id = P.AssignedLawyerID
                               JOIN [dbo].[Status] S ON S.StatusID = P.StatusID
                               JOIN [dbo].[City] C ON C.CityID = P.CityID
                               LEFT JOIN Province PR ON PR.ProvinceID = P.ProvinceID
                               LEFT JOIN Suburb SB ON SB.SuburbId = P.SuburbId
                            WHERE
                                P.[IsActive] = 1
                                AND P.[IsDeleted] = 0
                               
                                {PropertyQueries.WhereBaths(searchFilter.Baths)}
                                {PropertyQueries.WhereBeds(searchFilter.Beds)}
                                {PropertyQueries.WherePropertyType(searchFilter.PropertyTypeId)}
                                {PropertyQueries.WhereRegistrationType(searchFilter.RegistrationTypeId)}
                                {PropertyQueries.WhereMinPrice(searchFilter.MinPrice)}
                                {PropertyQueries.WhereMaxPrice(searchFilter.MaxPrice)}
                                {PropertyQueries.WhereRegistrationType(searchFilter.RegistrationTypeId)}
                                {PropertyQueries.WhereSearchTermsInCityOrSuburb(searchTerms)}";

                return await connection.QueryAsync<Property>(sql, new {
                    searchFilter.PropertyTypeId,
                    searchFilter.MinPrice,
                    searchFilter.MaxPrice,  
                    searchFilter.RegistrationTypeId,
                    searchTerms,
                    searchFilter.Baths,
                    searchFilter.Beds
                });

            }
        }

        public async Task<Property> GetPropertyById(long propertyId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var sql = @"
                            SELECT 
                                P.[PropertyID]
                                  ,P.[OwnerID]
                               ,OU.FirstName + ' ' + OU.LastName AS OwnerName
                                  ,P.[AssignedLawyerID]
                               ,OL.FirstName + ' ' + OL.LastName AS AssignedLawyerName
                                  ,P.[PropertyTypeID]
                               ,PT.[Name] AS PropertyTypeName
                                  ,P.[StatusID]
                               ,S.[Name]  AS StatusName
                                  ,P.[CityID]
                               ,C.[Name] AS CityName
                                  ,P.[ProvinceID]
                               ,PR.[Name] AS ProvinceName
                                   ,RT.[RegistrationTypeId]
                                  ,RT.[Name] AS RegistrationTypeName
                                  ,P.[Address]
                                  ,P.[Price]
                                  ,P.[Description]
                                  ,P.[NumberOfRooms]
                                  ,P.[NumberOfBathrooms]
                                  ,P.[ParkingSpots]
                                  ,P.[ThumbnailUrl]
                                  ,P.[YoutubeUrl]
                                  ,P.[HasLawyer]
                                  ,P.[NumberOfLikes] 
                                    ,P.Amenities
                                    ,P.SuburbId
                                    ,P.[SquareMetres]
                                    ,P.IsSold
                                    ,P.IsDeleted
                                    ,P.IsActive
                              FROM [dbo].[Property] P 
                              JOIN AspNetUsers OU ON OU.Id = P.OwnerID
                              JOIN PropertyType PT ON PT.PropertyTypeID = P.PropertyTypeID
                              JOIN AspNetUsers OL ON OL.Id = P.AssignedLawyerID
                              LEFT JOIN [dbo].[Status] S ON S.StatusID = P.StatusID
                              LEFT JOIN [dbo].[City] C ON C.CityID = P.CityID
                              LEFT JOIN Province PR ON PR.ProvinceID = P.ProvinceID
                              JOIN [RegistrationType] RT ON RT.[RegistrationTypeId] = P.[RegistrationTypeId]
                              WHERE P.PropertyID  = @propertyId";

                return await connection.QueryFirstOrDefaultAsync<Property>(sql, new { propertyId });

            }
        }

        public async Task ManageProperty(ManagePropertyDTO manage)
        {
            
            const string sql = @"
               UPDATE 
                   [dbo].[Property]
               SET 
                 [IsSold] = @IsSold
                ,[IsActive] = @IsActive

               WHERE
                   [PropertyId] = @PropertyId";

            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync(sql, new
            {
                 manage.IsActive
                ,manage.IsSold
                ,manage.PropertyId
            });
        }

        public async Task<IEnumerable<Property>> GetPropertiesByOwnerId(int ownerId, string searchTerms)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var sql = $@"
                            SELECT 
                                   P.[PropertyID]
                                  ,P.[OwnerID]
                                  ,OU.FirstName + ' ' + OU.LastName AS OwnerName
                                  ,P.[AssignedLawyerID]
                                  ,OL.FirstName + ' ' + OL.LastName AS AssignedLawyerName
                                  ,P.[PropertyTypeID]
                                  ,PT.[Name] AS PropertyTypeName
                                  ,P.[StatusID]
                                  ,S.[Name]  AS StatusName
                                  ,P.[SuburbId]
                                  ,SB.Name AS SuburbName
                                  ,P.[CityID]
                                  ,C.[Name] AS CityName
                                  ,P.[ProvinceID]
                                  ,PR.[Name] AS ProvinceName
                                  ,P.[Address]
                                  ,P.[Price]
                                  ,P.[Description]
                                  ,P.[NumberOfRooms]
                                  ,P.[NumberOfBathrooms]
                                  ,P.[ParkingSpots]
                                  ,P.[ThumbnailUrl]
                                  ,P.[YoutubeUrl]
                                  ,P.[HasLawyer]
                                  ,P.[NumberOfLikes] 
                                  ,P.[SquareMetres]
                                  ,P.[Amenities]
                                  ,P.[CreatedDate]
                                  ,P.[LastUpdatedDate]
                                  ,P.[IsSold]  
                                  ,P.[IsActive]
                                  ,P.[IsDeleted]
                               FROM [dbo].[Property] P 
                                   JOIN AspNetUsers OU ON OU.Id = P.OwnerID
                                   JOIN PropertyType PT ON PT.PropertyTypeID = P.PropertyTypeID
                                   JOIN AspNetUsers OL ON OL.Id = P.AssignedLawyerID
                                   LEFT JOIN [dbo].[Status] S ON S.StatusID = P.StatusID
                                   JOIN [dbo].[City] C ON C.CityID = P.CityID
                                   JOIN Province PR ON PR.ProvinceID = C.ProviceID
                                   LEFT JOIN Suburb SB ON SB.SuburbId = P.SuburbId
                                 WHERE
                                   P.[OwnerID] = @ownerId
                                {PropertyQueries.WhereSearchTermsInCityOrSuburb(searchTerms)}";

                return await connection.QueryAsync<Property>(sql, new
                {
                  ownerId,
                    searchTerms
                });

            }
        }
    }
    
}
