using Banga.Data.Models;
using Banga.Domain.Interfaces.Repositories;
using Banga.Domain.ViewModels;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Banga.Data.Repositories
{
    public class PropertyOfferRepository: IPropertyOfferRepository
    {
        private readonly IConfiguration _configuration;
        public PropertyOfferRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<long> CreateOffer(PropertyOffer offer)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return await connection.ExecuteScalarAsync<long>(@"
                       INSERT INTO [dbo].[PropertyOffer]
                            (
                                 [PropertyId]
                                ,[OfferByUserId]
                                ,[Description]
                                ,[Amount]
                                ,[CreatedDate]
                                ,[LastUpdatedDate]
                                ,[IsAccepted]
                                ,[IsOfferConfirmed]
                            )
                        VALUES
        	                (
                                 @PropertyId
                                ,@OfferByUserId
                                ,@Description
                                ,@Amount
                                ,GETDATE()
                                ,GETDATE()
                                ,0
                                ,0
                            )
                        Select SCOPE_IDENTITY()", new
                    {

                         offer.PropertyId
                        ,offer.OfferByUserId
                        ,offer.Amount
                        ,offer.Description

                });
            }
        }

        public async Task DeleteOffer(long offerId)
        {
            const string sql = @"
               DELETE FROM  
                   [dbo].[PropertyOffer]
               WHERE
                   [PropertyOfferId] = @offerId";

            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync(sql, new
            {
                offerId
            });
        }

        public async Task<PropertyOffer> GetCurrentBuyerOffer(long propertyId, int buyerId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var sql = @"
                            SELECT 
                                 O.PropertyOfferId
                                ,O.[PropertyId]
                                ,O.[OfferByUserId]
                                ,O.[Amount]
                                ,O.[CreatedDate]
                                ,O.[LastUpdatedDate]
                                ,O.[Description]
                                ,O.[IsAccepted]
                                ,O.[IsOfferConfirmed]
                                , U.FirstName + ' ' + U.LastName AS BuyerName
                              FROM 
                                    [dbo].[PropertyOffer] O
                              JOIN [AspNetUsers] U ON U.Id = O.OfferByUserId
                              WHERE 
                                    O.PropertyID = @propertyId
                                    AND O.OfferByUserId = @buyerId";

                return await connection.QueryFirstOrDefaultAsync<PropertyOffer>(sql, new { propertyId, buyerId });
            }
        }

        public async Task<IEnumerable<PropertyOffer>> GetOffersByPropertyId(long propertyId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var sql = @"
                           SELECT 
                                 O.PropertyOfferId
                                ,O.[PropertyId]
                                ,O.[OfferByUserId]
                                ,O.[Amount]
                                ,O.[CreatedDate]
                                ,O.[LastUpdatedDate]
                                ,O.[Description]
                                ,O.[IsAccepted]
                                ,O.[IsOfferConfirmed]
                                , U.FirstName + ' ' + U.LastName AS BuyerName
                              FROM 
                                    [dbo].[PropertyOffer] O
                              JOIN [AspNetUsers] U ON U.Id = O.OfferByUserId
                              WHERE 
                                    O.PropertyId = @propertyId";

                return await connection.QueryAsync<PropertyOffer>(sql, new { propertyId });
            }
        }

        public async Task UpdateOffer(PropertyOffer propertyOffer)
        {
                const string sql = @"
                   UPDATE 
                       [dbo].[PropertyOffer]
                   SET 
                             [Amount] = @Amount
                            ,[LastUpdatedDate] = GETDATE()
                            ,[IsAccepted] =  @IsAccepted
                            ,[Description] = @Description
                            ,[IsOfferConfirmed] =  @IsOfferConfirmed

                   WHERE
                       [PropertyOfferId] = @PropertyOfferId";

            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync(sql, new
            {
                 propertyOffer.Amount
                ,propertyOffer.IsAccepted
                ,propertyOffer.IsOfferConfirmed
                ,propertyOffer.Description
                ,propertyOffer.PropertyOfferId
            });
        }

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

        public async Task<IEnumerable<VwUserPropertyOffers>> GetPropertyOffersByUserId(long userId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var sql = @"
                           SELECT 
	                            O.PropertyOfferId,
	                            O.PropertyId,
	                            O.[Description],
	                            O.IsAccepted,
                                O.IsOfferConfirmed,
	                            O.Amount,
	                            O.OfferByUserId,
	                            T.[Name] AS PropertyType,
	                            P.Price,
	                            P.[Address]
                            FROM PropertyOffer O
                            JOIN Property  P ON P.PropertyId = O.PropertyId
                            JOIN PropertyType T ON T.PropertyTypeID = P.PropertyTypeId
                            WHERE 
                            P.IsActive = 1
                            AND P.IsDeleted = 0
                            AND P.IsSold = 0
                            AND O.OfferByUserId = @userId"
                ;

                return await connection.QueryAsync<VwUserPropertyOffers>(sql, new { userId });
            }
        }

        public async Task<PropertyOffer> GetOfferById(long offerId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var sql = @"
                            SELECT 
                                 O.PropertyOfferId
                                ,O.[PropertyId]
                                ,O.[OfferByUserId]
                                ,O.[Amount]
                                ,O.[CreatedDate]
                                ,O.[LastUpdatedDate]
                                ,O.[Description]
                                ,O.[IsAccepted]
                                ,O.[IsOfferConfirmed]
                                , U.FirstName + ' ' + U.LastName AS BuyerName
                              FROM 
                                    [dbo].[PropertyOffer] O
                              JOIN [AspNetUsers] U ON U.Id = O.OfferByUserId
                              WHERE 
                                    O.PropertyOfferId = @offerId";

                return await connection.QueryFirstOrDefaultAsync<PropertyOffer>(sql, new { offerId });
            }
        }
    }
}
