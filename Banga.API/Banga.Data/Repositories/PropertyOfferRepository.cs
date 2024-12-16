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
                                  ,[PaymentMethodId]
                                  ,[StatusId]
                                ,[CreatedDate]
                                ,[LastUpdatedDate]

                            )
                        VALUES
        	                (
                                 @PropertyId
                                ,@OfferByUserId
                                ,@Description
                                ,@Amount
                                ,@PaymentMethodId
                                ,@StatusId
                                ,GETDATE()
                                ,GETDATE()
                            )
                        Select SCOPE_IDENTITY()", new
                    {

                         offer.PropertyId
                        ,offer.OfferByUserId
                        ,offer.Amount
                        ,offer.Description
                        ,offer.PaymentMethodId
                        ,offer.StatusId

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
                                ,O.[PaymentMethodId]
                                ,O.[StatusId]
                                ,S.[Name] as Status
                                ,P.[Name] as PaymentMethod
                                ,U.FirstName + ' ' + U.LastName AS BuyerName
                              FROM 
                                    [dbo].[PropertyOffer] O
                              JOIN [AspNetUsers] U ON U.Id = O.OfferByUserId
                              JOIN [dbo].[Status] S ON S.StatusID = O.StatusId
                              JOIN [dbo].[PaymentMethod] P ON P.PaymentMethodId = O.PaymentMethodId
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
                                ,O.[PaymentMethodId]
                                ,O.[StatusId]
                                ,S.[Name] as Status
                                ,P.[Name] as PaymentMethod
                                , U.FirstName + ' ' + U.LastName AS BuyerName
                              FROM 
                                    [dbo].[PropertyOffer] O
                              JOIN [AspNetUsers] U ON U.Id = O.OfferByUserId
                              JOIN [dbo].[Status] S ON S.StatusID = O.StatusId
                              JOIN [dbo].[PaymentMethod] P ON P.PaymentMethodId = O.PaymentMethodId
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
                            ,[PaymentMethodId] = @PaymentMethodId
                            ,[StatusId] = @StatusId

                   WHERE
                       [PropertyOfferId] = @PropertyOfferId";

            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync(sql, new
            {
                 propertyOffer.Amount
                ,propertyOffer.StatusId
                ,propertyOffer.PaymentMethodId
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
                                ,O.[PaymentMethodId]
                                ,O.[StatusId]
                                ,S.[Name] as Status
                                ,P.[Name] as PaymentMethod
                              FROM [dbo].[PropertyOffer] O
                              JOIN [dbo].[Status] S ON S.StatusID = O.StatusId
                              JOIN [dbo].[PaymentMethod] P ON P.PaymentMethodId = O.PaymentMethodId
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
	                            O.Amount,
	                            O.OfferByUserId,
	                            T.[Name] AS PropertyType,
	                            P.Price,
	                            P.[Address]
                                ,O.[PaymentMethodId]
                                ,O.[StatusId]
                                ,S.[Name] as Status
                                ,P.[Name] as PaymentMethod
                            FROM PropertyOffer O
                            JOIN Property  P ON P.PropertyId = O.PropertyId
                            JOIN PropertyType T ON T.PropertyTypeID = P.PropertyTypeId
                              JOIN [dbo].[Status] S ON S.StatusID = O.StatusId
                              JOIN [dbo].[PaymentMethod] P ON P.PaymentMethodId = O.PaymentMethodId
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
                                ,O.[PaymentMethodId]
                                ,O.[StatusId]
                                ,S.[Name] as Status
                                ,P.[Name] as PaymentMethod
                                , U.FirstName + ' ' + U.LastName AS BuyerName
                              FROM 
                                    [dbo].[PropertyOffer] O
                              JOIN [AspNetUsers] U ON U.Id = O.OfferByUserId
                              JOIN [dbo].[Status] S ON S.StatusID = O.StatusId
                              JOIN [dbo].[PaymentMethod] P ON P.PaymentMethodId = O.PaymentMethodId
                              WHERE 
                                    O.PropertyOfferId = @offerId";

                return await connection.QueryFirstOrDefaultAsync<PropertyOffer>(sql, new { offerId });
            }
        }
    }
}
