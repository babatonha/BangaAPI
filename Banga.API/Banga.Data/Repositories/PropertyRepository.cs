using Banga.Data.Models;
using Banga.Data.ViewModels;
using Banga.Domain.Interfaces.Repositories;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.Design;

namespace Banga.Data.Repositories
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly IConfiguration _configuration;
        public PropertyRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //   public async Task<ClaimPerson> GetPerssonAssured(int personAssured)
        //   {
        //       const string sql = @"
        //         SELECT
        //               user_id as UserId
        //               ,person_id as PersonId
        //               ,age as Age
        //               ,date_inserted as DateInserted
        //               ,dob as DOB
        //               ,title
        //               ,initials
        //               ,firstname as FirstName
        //               ,surname as Surname
        //               ,nickname
        //               ,gender
        //               ,id_number_type as IdNumberType
        //               ,id_number as IdNumber
        //               ,passport_number as PassportNumber
        //               ,occupation
        //               ,last_updated as LastUpdatedDate
        //               ,last_updated_user_id as LastUpdatedUserId
        //               ,person_status as PersonStatus
        //               ,preferred_language as PreferredLanguage
        //         FROM
        //               [PLDVSQL01].StaticPinotage.dbo.person
        //         WHERE
        //               person_id = @personAssured";

        //       using var connection = new SqlConnection(_configuration.GetConnectionString("SqlConnection"));
        //       return await connection.QueryFirstOrDefaultAsync<ClaimPerson>(sql, new { personAssured }, null, 900);
        //   }

        //   public async Task<IEnumerable<ClaimPayerDetail>> GetPayerDetails(Claim claim)
        //   {
        //       const string sql = @"
        //            SELECT
        //               age as Age
        //               ,gender
        //               ,policy_id as PolicyID
        //               ,vldt as Vldt
        //               ,Description
        //               ,initials
        //               ,surname as Surname
        //               ,person_assured as PersonAssured
        //               ,product_id as ProductId
        //               ,Tranche 
        //               ,TotalCover
        //               ,MunichCover
        //               ,GuardriskCover
        //               ,PlatinumCover
        //               ,RGACover          
        //           FROM
        //               [PLDVSQL01].StaticLife.dbo.rein_t_claims
        //           WHERE
        //               policy_id = @PolicyID 
        //           AND
        //               vldt = datefromparts(YEAR(@IncidentDate), month(@IncidentDate),01)
        //           AND 
        //               initials = @Initials 
        //           AND 
        //               surname = @Surname 
        //           AND
        //               Description = @Description  
        //           AND 
        //               person_assured = @PersonAssured";

        //       using var connection = new SqlConnection(_configuration.GetConnectionString("SqlConnection"));
        //       return await connection.QueryAsync<ClaimPayerDetail>(sql,
        //           new
        //           {
        //               claim.PolicyID,
        //               claim.IncidentDate,
        //               claim.Initials,
        //               claim.Surname,
        //               claim.Description,
        //               claim.PersonAssured
        //           }, null, 900);
        //   }

        //   public async Task<IEnumerable<PolicyCover>> GetPolicyCovers(int policyId, string incidentDate)
        //   {
        //       const string sql = @"
        //            SELECT
        //            policy_id AS PolicyID, 
        //            vldt AS Vldt, 
        //            Description, 
        //            initials AS Initials, 
        //            surname , 
        //            person_assured AS PersonAssured, 
        //            TotalCover,
        //               initials + ' ' + surname + ' - ' + Description + ' - (' + FORMAT(TotalCover, 'C', 'en-ZA') + ')'as DisplayValue
        //           FROM
        //               [PLDVSQL01].StaticLife.dbo.rein_t_claims
        //           WHERE 
        //               policy_id = @policyId 
        //           AND
        //               vldt = datefromparts(YEAR(@incidentDate), month(@incidentDate),01)";

        //       using var connection = new SqlConnection(_configuration.GetConnectionString("SqlConnection"));

        //       return await connection.QueryAsync<PolicyCover>(sql, new { policyId, incidentDate }, null, 900);
        //   }

        //   public async Task<long> CreateClaim(Claim claim)
        //   {
        //       using (var connection = new SqlConnection(_configuration.GetConnectionString("SqlConnection")))
        //       {
        //           return await connection.ExecuteScalarAsync<long>(@"
        //               INSERT INTO [dbo].[Claim]
        //    ([PolicyID]
        //                   ,[ClaimDate]
        //                   ,[IncidentDate]
        //                   ,[ApprovedDate]
        //                   ,[Description]
        //                   ,[Vldt]
        //                   ,[Surname]
        //                   ,[Initials]
        //                   ,[PersonAssured]
        //                   ,[ClaimAmount]
        //                   ,[TotalCover]
        //                   ,CreatedDate
        //                   ,[CreatedBy]
        //                   ,[IsDeleted]
        //                   ,[LastUpdatedDate]
        //                   ,[LastUpdatedBy])
        //VALUES
        //	(@PolicyID,
        //	@ClaimDate,
        //	@IncidentDate,
        //	@ApprovedDate,
        //	@Description,
        //	@Vldt,
        //	@Surname,
        //                   @Initials,
        //                   @PersonAssured,
        //                   @ClaimAmount,
        //                   @TotalCover,
        //                   GETDATE(),
        //                   @CreatedBy, 
        //	@IsDeleted,
        //                   GETDATE(),
        //                   @CreatedBy)
        //Select SCOPE_IDENTITY()", new
        //           {
        //               claim.PolicyID,
        //               claim.ClaimDate,
        //               claim.IncidentDate,
        //               claim.ApprovedDate,
        //               claim.Description,
        //               claim.Vldt,
        //               claim.Surname,
        //               claim.Initials,
        //               claim.PersonAssured,
        //               claim.ClaimAmount,
        //               claim.TotalCover,
        //               claim.CreatedBy,
        //               claim.IsDeleted,
        //           });
        //       }
        //   }

        //   public async Task<System.Int64> CreateClaimPayer(ClaimPayerDetail payer, long claimId)
        //   {
        //       payer.ClaimID = claimId;
        //       using (var connection = new SqlConnection(_configuration.GetConnectionString("SqlConnection")))
        //       {
        //           return await connection.ExecuteScalarAsync<long>(@"
        //               INSERT INTO [dbo].[ClaimPayer]
        //    ( [ClaimID]
        //                     ,[ProductId]
        //                     ,[Tranche]
        //                     ,[TotalCover]
        //                     ,[MunichCover]
        //                     ,[GuardriskCover]
        //                     ,[PlatinumCover]
        //                     ,[RGACover])
        //VALUES
        //	(  @ClaimID
        //                     ,@ProductId
        //                     ,@Tranche
        //                     ,@TotalCover
        //                     ,@MunichCover
        //                     ,@GuardriskCover
        //                     ,@PlatinumCover
        //                     ,@RGACover)
        //Select SCOPE_IDENTITY()", new
        //           {
        //               payer.ClaimID,
        //               payer.ProductId,
        //               payer.Tranche,
        //               payer.TotalCover,
        //               payer.MunichCover,
        //               payer.GuardriskCover,
        //               payer.PlatinumCover,
        //               payer.RGACover,
        //           });
        //       }

        //   }

        //   public async Task<System.Int64> CreateClaimPerson(ClaimPerson person, long claimId)
        //   {
        //       person.ClaimID = claimId;
        //       using (var connection = new SqlConnection(_configuration.GetConnectionString("SqlConnection")))
        //       {
        //           return await connection.ExecuteScalarAsync<long>(@"
        //               INSERT INTO [dbo].[ClaimPerson]
        //    (   [ClaimID]
        //                       ,[PersonId]
        //                       ,[UserId]
        //                       ,[Title]
        //                       ,[Initials]
        //                       ,[FirstName]
        //                       ,[Surname]
        //                       ,[NickName]
        //                       ,[Age]
        //                       ,[DOB]
        //                       ,[Gender]
        //                       ,[IdNumberType]
        //                       ,[IdNumber]
        //                       ,[PassportNumber]
        //                       ,[Occupation]
        //                       ,[PersonStatus]
        //                       ,[PreferredLanguage]
        //                       ,[DateInserted]
        //                       ,[LastUpdatedDate]
        //                       ,[LastUpdatedUserId])
        //VALUES
        //	(    @ClaimID
        //                       ,@PersonId
        //                       ,@UserId
        //                       ,@Title
        //                       ,@Initials
        //                       ,@FirstName
        //                       ,@Surname
        //                       ,@NickName
        //                       ,@Age
        //                       ,@DOB
        //                       ,@Gender
        //                       ,@IdNumberType
        //                       ,@IdNumber
        //                       ,@PassportNumber
        //                       ,@Occupation
        //                       ,@PersonStatus
        //                       ,@PreferredLanguage
        //                       ,@DateInserted
        //                       ,@LastUpdatedDate
        //                       ,@LastUpdatedUserId)
        //Select SCOPE_IDENTITY()", new
        //           {
        //               person.ClaimID,
        //               person.PersonId,
        //               person.UserId,
        //               person.Title,
        //               person.Initials,
        //               person.FirstName,
        //               person.Surname,
        //               person.NickName,
        //               person.Age,
        //               person.DOB,
        //               person.Gender,
        //               person.IdNumberType,
        //               person.IdNumber,
        //               person.PassportNumber,
        //               person.Occupation,
        //               person.PersonStatus,
        //               person.PreferredLanguage,
        //               person.DateInserted,
        //               person.LastUpdatedDate,
        //               person.LastUpdatedUserId
        //           });
        //       }
        //   }

        //   public async Task<Claim> GetClaimById(long claimId)
        //   {
        //       const string sql = @"
        //         SELECT
        //                   *
        //         FROM
        //               [Claim]
        //         WHERE
        //               ClaimID = @claimId";

        //       using var connection = new SqlConnection(_configuration.GetConnectionString("SqlConnection"));
        //       return await connection.QueryFirstOrDefaultAsync<Claim>(sql, new { claimId });
        //   }

        //   public async Task<IEnumerable<Claim>> GetClaimsByUserId(int userId)
        //   {
        //       const string sql = @"
        //         SELECT
        //                   *
        //         FROM
        //               [Claim]
        //         WHERE
        //               CreatedBy = @userId";

        //       using var connection = new SqlConnection(_configuration.GetConnectionString("SqlConnection"));
        //       return await connection.QueryAsync<Claim>(sql, new { userId });
        //   }
        //   public async Task<ClaimPerson> GetClaimPerson(long claimId)
        //   {
        //       const string sql = @"
        //         SELECT
        //                   *
        //         FROM
        //               [ClaimPerson]
        //         WHERE
        //               ClaimID = @claimId";

        //       using var connection = new SqlConnection(_configuration.GetConnectionString("SqlConnection"));
        //       return await connection.QueryFirstOrDefaultAsync<ClaimPerson>(sql, new { claimId });
        //   }
        //   public async Task<IEnumerable<ClaimPayerDetail>> GetClaimPayer(long claimId)
        //   {
        //       const string sql = @"
        //            SELECT
        //                   *        
        //           FROM
        //               [ClaimPayer]
        //           WHERE
        //               ClaimID = @claimId";

        //       using var connection = new SqlConnection(_configuration.GetConnectionString("SqlConnection"));
        //       return await connection.QueryAsync<ClaimPayerDetail>(sql, new { claimId });
        //   }

        //   public async Task<long> CreateClaimStatus(ClaimStatus claimStatus)
        //   {
        //       using (var connection = new SqlConnection(_configuration.GetConnectionString("SqlConnection")))
        //       {
        //           return await connection.ExecuteScalarAsync<long>(@"
        //               INSERT INTO [dbo].[ClaimStatus]
        //    ([ClaimID]
        //                   ,[StatusID]
        //                   ,[CreatedBy]
        //                   ,[AssignedTo]
        //                   ,[CreatedDate]
        //                  )
        //VALUES
        //	(@ClaimID,
        //	@StatusID,
        //	@CreatedBy,
        //                   @AssignedTo,
        //	GETDATE())
        //Select SCOPE_IDENTITY()", new
        //           {
        //               claimStatus.ClaimID,
        //               claimStatus.StatusID,
        //               claimStatus.CreatedBy,
        //               claimStatus.AssignedTo

        //           });
        //       }
        //   }

        //   public async Task<IEnumerable<ClaimStatus>> GetClaimStatuses(long claimId)
        //   {
        //       const string sql = @"
        //         SELECT
        //                   C.*,
        //                   S.StatusName,
        //                   U.FirstName +' '+ U.LastName AS FullName,
        //                   A.FirstName +' '+ A.LastName AS AssignedToFullName

        //         FROM
        //               [ClaimStatus] C
        //         JOIN 
        //               [Status] S ON S.StatusID = C.StatusID
        //         JOIN 
        //               [User] U ON U.UserID = C.CreatedBy
        //         JOIN 
        //               [User] A ON A.UserID = C.AssignedTo
        //         WHERE
        //               C.ClaimID = @claimId
        //          ORDER BY C.ClaimStatusID DESC";

        //       using var connection = new SqlConnection(_configuration.GetConnectionString("SqlConnection"));
        //       return await connection.QueryAsync<ClaimStatus>(sql, new { claimId });
        //   }

        //   public async Task<ClaimStatus> GetCurrentClaimStatus(long claimId)
        //   {
        //       const string sql = @"
        //         SELECT TOP 1
        //                   C.*,
        //                   S.StatusName,
        //                   U.FirstName +' '+ U.LastName AS FullName,
        //                   A.FirstName +' '+ A.LastName AS AssignedToFullName

        //         FROM
        //               [ClaimStatus] C
        //         JOIN 
        //               [Status] S ON S.StatusID = C.StatusID
        //         JOIN 
        //               [User] U ON U.UserID = C.CreatedBy
        //         JOIN 
        //               [User] A ON A.UserID = C.AssignedTo
        //         WHERE
        //               C.ClaimID = @claimId
        //          ORDER BY C.ClaimStatusID DESC";

        //       using var connection = new SqlConnection(_configuration.GetConnectionString("SqlConnection"));
        //       return await connection.QueryFirstOrDefaultAsync<ClaimStatus>(sql, new { claimId });
        //   }

        //   public async Task<IEnumerable<Claim>> GetClaimsByAssignedUserId(int assignedUserId)
        //   {
        //       const string sql = @"
        //     WITH RankedStatuses AS (
        //           SELECT
        //               ClaimStatusID,
        //               ClaimID,
        //               ROW_NUMBER() OVER (PARTITION BY ClaimID ORDER BY ClaimStatusID DESC) AS RowNum
        //           FROM
        //               [ClaimStatus]
        //        WHERE AssignedTo = @assignedUserId
        //       )
        //       SELECT
        //           C.*
        //       FROM
        //           RankedStatuses S
        //       JOIN [Claim] C  ON c.ClaimID = S.ClaimID
        //       WHERE
        //           S.RowNum = 1;";

        //       using var connection = new SqlConnection(_configuration.GetConnectionString("SqlConnection"));
        //       return await connection.QueryAsync<Claim>(sql, new { assignedUserId });
        //   }

        //   public async Task UpdateClaim(Claim claim)
        //   {
        //       const string sql = @"
        //       UPDATE 
        //           [dbo].[Claim]
        //       SET 
        //           [PolicyID] = @PolicyID
        //           ,[ClaimDate] = @ClaimDate
        //           ,[IncidentDate] = @IncidentDate
        //           ,[ApprovedDate] = @ApprovedDate
        //           ,[Description] = @Description
        //           ,[Vldt] = @Vldt
        //           ,[Surname] = @Surname
        //           ,[Initials] = @Initials
        //           ,[PersonAssured] = @PersonAssured
        //           ,[ClaimAmount] = @ClaimAmount
        //           ,[TotalCover] = @TotalCover
        //           ,CreatedDate = @CreatedDate
        //           ,[CreatedBy] = @CreatedBy
        //           ,[IsDeleted] = @IsDeleted
        //           ,[LastUpdatedDate] = @LastUpdatedDate
        //           ,[LastUpdatedBy] = @LastUpdatedBy

        //       WHERE
        //           [ClaimID] = @ClaimID";

        //       using var connection = new SqlConnection(_configuration.GetConnectionString("SqlConnection"));
        //       await connection.ExecuteAsync(sql, new
        //       {
        //           claim.PolicyID,
        //           claim.ClaimDate,
        //           claim.IncidentDate,
        //           claim.ApprovedDate,
        //           claim.Description,
        //           claim.Vldt,
        //           claim.Surname,
        //           claim.Initials,
        //           claim.PersonAssured,
        //           claim.ClaimAmount,
        //           claim.TotalCover,
        //           claim.CreatedBy,
        //           claim.IsDeleted,
        //           claim.LastUpdatedDate,
        //           claim.LastUpdatedBy,
        //           claim.CreatedDate,
        //           claim.ClaimID
        //       });
        //   }


        public Task<long> CreateProperty(Property property)
        {
            throw new NotImplementedException();
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

        public async Task<IEnumerable<PropertyPhoto>> GetPropertyPhotosByPropertyId(long propertyId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var sql = @"
                            SELECT 
                                  PH.[PropertyPhotoID]
                                 ,PH.[PropertyID]
                                 ,PH.[PhotoUrl]                       
                              FROM [dbo].[PropertyPhoto] PH
                              WHERE PH.PropertyID = @propertyId";

                return await connection.QueryAsync<PropertyPhoto>(sql, new { propertyId });
            }
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

                return await connection.QueryAsync<Property>(sql,new {} ); 

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
    }
    
}
