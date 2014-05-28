//using System;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
//using System.Linq.Dynamic;
//using System.Threading.Tasks;
using System.Transactions;
using DrynksMe.Services.Models;
//using MoreLinq;
using Dapper;
using DrynksMe.DataAccess.Infrastructure;
using DrynksMe.DataAccess.Models;
using DrynksMe.Services.Contracts;
using IsolationLevel = System.Transactions.IsolationLevel;

namespace DrynksMe.Services
{
    public class MerchantService : BaseServices, IMerchantService
    {
        public MerchantService(IDatabaseContext databaseContext) 
            : base(databaseContext){}
       
        public IEnumerable<Merchant> GetMerchantsByCoordinates(double longitude, double latitude,int radius)
        {
            const string sqlToGetMechants =
                @"select * from Merchant
                where IsActive = 1
                And dbo.CoordinateDistanceMiles(Latitude, Longitude, @UserLatitude, @UserLongitude) < @Radius";
           
            using (var connection = DatabaseContext.Connection)
            {
                return
                    connection.Query<Merchant>(sqlToGetMechants,
                                               new
                                                   {
                                                       @UserLatitude = latitude,
                                                       @UserLongitude = longitude,
                                                       @Radius = radius
                                                   }).ToList();
            }
        }
        public IEnumerable<Merchant> GetVenues()
        {  
            using (var connection = DatabaseContext.Connection)
            {
                var merchants = connection.GetList<Merchant>();
                return merchants.ToList();
            }
        }

        public int AddVenue(Merchant merchant,int[] merchantTypeIds)
        {
            int returnVal;

            using (var connection = DatabaseContext.Connection)
            using (var transaction = connection.BeginTransaction())
            {
                returnVal = connection.Insert(merchant, transaction);
                InsertMerchantTypeAssociation(merchantTypeIds, returnVal, connection, transaction);
                transaction.Commit();
            }

            return returnVal;
        }

        private static void InsertMerchantTypeAssociation(int[] merchantTypeIds, int returnVal, IDbConnection connection,
                                                          IDbTransaction transaction=null)
        {
            foreach (var mAssoc in merchantTypeIds
                .Select(merchantTypeId => new MerchantTypeAssociation
                    {
                        MerchantId = returnVal,
                        MerchantTypeId = merchantTypeId
                    }))
            {
                connection.Insert(mAssoc, transaction);
            }
        }

        public int UpdateVenue(Merchant merchant,int[] merchantTypeIds)
        {
            int returnVal;

            using (var connection = SqlConnection)
            using (
                var scope = new TransactionScope(TransactionScopeOption.Required,
                                                 new TransactionOptions {IsolationLevel = IsolationLevel.Serializable}))
            {
                connection.EnlistTransaction(Transaction.Current);

                returnVal = connection.Update(merchant);
                DeleteMerchantAssociations(merchant.Id, connection);
                InsertMerchantTypeAssociation(merchantTypeIds, merchant.Id, connection);
                scope.Complete();
            }

            return returnVal;
        }

        private static void DeleteMerchantAssociations(int merchantId, SqlConnection connection)
        {
            const string sql = @"Delete from [dbo].[MerchantTypeAssociation] where MerchantId = @merchantId";
            connection.Execute(sql, new {@merchantId = merchantId});

        }

        public IEnumerable<MerchantType> GetAllMerchantTypes()
        {
            using (var connection = DatabaseContext.Connection)
            {
                return connection.GetList<MerchantType>();
            }
        }
        public MerchantGroup GetVenue(int id)
        {
            const string sqlToGetMerchantType =
               @"
                Select * from MerchantType;
 
                Select * From Merchant where Id = @Id;

                Select MT.* from MerchantType MT
                inner join MerchantTypeAssociation MTA on MT.Id = MTA.MerchantTypeId
                where MTA.MerchantId = @Id;";
                
            using (var connection = DatabaseContext.Connection)
            using (var multi = connection.QueryMultiple(sqlToGetMerchantType, new { @Id = id }))
            {
                var merchantGroup = new MerchantGroup
                    {
                        AllMerchantTypes = multi.Read<MerchantType>().ToList(),
                        Merchant = multi.Read<Merchant>().First(),
                        MerchantTypeForMerchants = multi.Read<MerchantType>().ToList()

                    };

                return merchantGroup;

            }
        }

        public MerchantWithComments GetVenueWithComments(int id)
        {
            const string sqlToGetMerchantType =
               @"
                Select * From Merchant where Id = @Id;
                Select * from UserMerchantComment where MerchantId = @Id
                         and IsShared  = 1 and IsApproved = 1;
               ";

            using (var connection = DatabaseContext.Connection)
            using (var multi = connection.QueryMultiple(sqlToGetMerchantType, new { @Id = id }))
            {
                var merchantWithComments = new MerchantWithComments
                {
                    
                    Merchant = multi.Read<Merchant>().First(),
                    Comments = multi.Read<UserMerchantComment>().ToList()

                };

                return merchantWithComments;

            }
        }

        public void AddVenueComment(User user, int merchantId, string comment)
        {
            var userComment = new UserMerchantComment
                {
                    Comment = comment,
                    UserId = user.Id,
                    IsShared = user.Share,
                    IsApproved = true,
                    MerchantId = merchantId,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now
                    
                };

            using (var connection = SqlConnection)
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.Serializable }))
            {
                connection.EnlistTransaction(Transaction.Current);
                connection.Insert(userComment);
                scope.Complete();

            }

        }


        public void UpdateUserMerchantCommentForShared(User user, IDbConnection connection)
        {
            const string sqlToUpdateMerchantCommentForShare =
                @"Update UserMerchantComment
                Set IsShared = @IsShared
                Where UserId = @UserId";

            connection.Execute(sqlToUpdateMerchantCommentForShare, new {@IsShared = user.Share, @UserId = user.Id});
                               
        }

        public IEnumerable<Merchant> GetFeaturedMerchants()
        {  
            var todaysDate = DateTime.Now;
            // TODO:Think of better SQL
            const string sqlToGetFeaturedBrands =
                @"select * from Merchant M
                inner join 
                (select Distinct(MTA.MerchantId) from MerchantTypeAssociation MTA 
                 inner join MerchantType MT on MTA.MerchantTypeId = MT.Id 
                 inner join Campaign C on MTA.MerchantId = C.MerchantId
                  where C.IsActive = 1 And StartDate <= startDate and EndDate >= endDate  And MT.TypeDescription = 'Brand'
                ) as B on M.Id = B.MerchantId
                where  M.IsActive = 1 And M.IsPremierMerchant = 1";


              using (var connection = DatabaseContext.Connection)
              {
                  var merchants = connection.Query<Merchant>(sqlToGetFeaturedBrands,
                                                             new {@startDate = todaysDate, @endDate = todaysDate});
                  return merchants.ToList();
              }

        }

        public IEnumerable<MerchantSearchResult> Search(MerchantSearchModel merchantSearchModel)
        {
            var merchantQuery = new MerchantQuery();

            var param = new DynamicParameters();

            if (!string.IsNullOrEmpty(merchantSearchModel.ProfileName))
            {
                param.AddDynamicParams(new { @profileName = "%" + merchantSearchModel.ProfileName + "%" });
                merchantQuery = merchantQuery.ByProfileName();
            }

            if (!string.IsNullOrEmpty(merchantSearchModel.MerchantName))
            {
                param.AddDynamicParams(new { @merchantName = "%" + merchantSearchModel.MerchantName + "%" });
                merchantQuery = merchantQuery.ByMerchantName();
            }

            if (!string.IsNullOrEmpty(merchantSearchModel.VenueType))
            {
                param.AddDynamicParams(new { @venueType = "%" + merchantSearchModel.VenueType + "%" });
                merchantQuery = merchantQuery.ByVenueType();
            }

            if (!string.IsNullOrEmpty(merchantSearchModel.TwitterHandle))
            {
                param.AddDynamicParams(new { @twitterHandle = "%" + merchantSearchModel.TwitterHandle + "%" });
                merchantQuery = merchantQuery.ByTwitterHandle();
            }


// ReSharper disable RedundantAnonymousTypePropertyName
            param.AddDynamicParams(new { @startRow = merchantSearchModel.StartRowNum });
// ReSharper restore RedundantAnonymousTypePropertyName
// ReSharper disable RedundantAnonymousTypePropertyName
            param.AddDynamicParams(new { @endRow = merchantSearchModel.EndRowNum });
// ReSharper restore RedundantAnonymousTypePropertyName

            var sql = merchantQuery.Bind();
                                   
            using ( var connection=DatabaseContext.Connection)
            {
                
                var merchants = connection.Query<MerchantSearchResult>(sql,param);
                return merchants.ToList();
            }
        }

        public IEnumerable<Merchant> SearchByTags(IEnumerable<string> tags)
        {
            const string sqlToGetMerchantByTags =
                @"select Distinct(M.Id), M.* from Merchant M
                inner join MerchantTag MT on M.Id = MT.MerchantId
                inner join Tag T on MT.TagId = T.Id
                where T.TagName IN @Tags";


              using (var connection = DatabaseContext.Connection)
              {
                  return connection.Query<Merchant>(sqlToGetMerchantByTags, new {@Tags = tags}).ToList();

              }


        }

        public IEnumerable<DrinksResultModel> GetDrinks(int merchantId)
        {
            const string sqlToGetDrinksForMerchant =
                @"select D.*,MD.IsSignature from Drink D
                inner join MerchantDrink MD on D.Id = MD.DrinkId
                where D.IsShared = 1 and MD.MerchantId = @MerchantId";

            using (var connection = DatabaseContext.Connection)
            {
                return
                    connection.Query<DrinksResultModel>(sqlToGetDrinksForMerchant, new {@MerchantId = merchantId})
                              .ToList();

            }
        }


        //private bool Validator(Merchant merchant)
        //{
        //    var list = new List<Func<Merchant, bool>>()
        //        {
        //            x => x.City == "NY",
        //            x => x.Email == "NotNull"
        //        };
        //    return list.All(x => x(merchant));
            
        //}
       
    }
}