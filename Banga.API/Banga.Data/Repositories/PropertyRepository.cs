using Banga.Data.Models;
using Banga.Domain.Interfaces.Repositories;
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


        //Property
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
                                ,[StatusID]
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
                            )
                        VALUES
        	                (
                                 @OwnerID
                                ,@AssignedLawyerID
                                ,@PropertyTypeID
                                ,@StatusID
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
                            )
                        Select SCOPE_IDENTITY()", new
                    {
                         property.OwnerId
                        ,property.AssignedLawyerId
                        ,property.PropertyTypeId
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
                ,[StatusID] = @StatusID
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

               WHERE
                   [PropertyId] = @PropertyId";

            using var connection = new SqlConnection(_configuration.GetConnectionString("SqlConnection"));
            await connection.ExecuteAsync(sql, new
            {
                property.PropertyId
                ,property.OwnerId
                ,property.AssignedLawyerId
                ,property.PropertyTypeId
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
            });
        }

        public async Task<IEnumerable<Property>> GetProperties()
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
                              FROM [dbo].[Property] P 
                              JOIN AspNetUsers OU ON OU.Id = P.OwnerID
                              JOIN PropertyType PT ON PT.PropertyTypeID = P.PropertyTypeID
                              JOIN AspNetUsers OL ON OL.Id = P.AssignedLawyerID
                              JOIN [dbo].[Status] S ON S.StatusID = P.StatusID
                              JOIN [dbo].[City] C ON C.CityID = P.CityID
                              JOIN Province PR ON PR.ProvinceID = P.ProvinceID";

                return await connection.QueryAsync<Property>(sql, new { });

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
                              FROM [dbo].[Property] P 
                              JOIN AspNetUsers OU ON OU.Id = P.OwnerID
                              JOIN PropertyType PT ON PT.PropertyTypeID = P.PropertyTypeID
                              JOIN AspNetUsers OL ON OL.Id = P.AssignedLawyerID
                              JOIN [dbo].[Status] S ON S.StatusID = P.StatusID
                              JOIN [dbo].[City] C ON C.CityID = P.CityID
                              JOIN Province PR ON PR.ProvinceID = P.ProvinceID
                              WHERE P.PropertyID  = @propertyId";

                return await connection.QueryFirstOrDefaultAsync<Property>(sql, new { propertyId });

            }
        }

        //Property Offer
        public async Task<IEnumerable<PropertyOffer>> GetPropertyOffersByPropertyId(long propertyId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var sql = @"
                            SELECT 
                                  O.[PropertyOfferID]
                                 ,O.[PropertyID]
                                 ,O.[OfferBy]
                                 ,O.[Amount]                    
                              FROM [dbo].[PropertyOffer] O
                              WHERE O.PropertyID = @propertyId";

                return await connection.QueryAsync<PropertyOffer>(sql, new { propertyId });
            }
        }

    
    }
    
}
