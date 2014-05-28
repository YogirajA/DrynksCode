using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using DrynksMe.DataAccess.Infrastructure;
using DrynksMe.DataAccess.Models;
using Dapper;
using DrynksMe.Services.Contracts;
using IsolationLevel = System.Transactions.IsolationLevel;

namespace DrynksMe.Services
{
    public class MembershipService : BaseServices, IMembershipService
    {
        
        private readonly IDrinksServices _drinksServices;
        private readonly IMerchantService _merchantService;


        public MembershipService(IDatabaseContext databaseContext) 
            :this(databaseContext,new DrinksServices(databaseContext),new MerchantService(databaseContext)){}
        
        public MembershipService(IDatabaseContext databaseContext,IDrinksServices drinksServices,IMerchantService merchantService):base(databaseContext)
        {
            _drinksServices = drinksServices;
            _merchantService = merchantService;
        }

        public UserProfile GetUserProfile(string userName)
        {
            const string selectUserProfileByUserName =
            @"select * from UserProfile where userName=@UserName";
            using (var connection = DatabaseContext.Connection)
            {
                return connection.Query<UserProfile>(selectUserProfileByUserName, new { @UserName = userName }).SingleOrDefault();
            }
        }

        public void UpdateUserProfile(UserProfile userProfile)
        {
           
            using (var connection = DatabaseContext.Connection)
            using (var transaction = connection.BeginTransaction())
            {
                UpdateUserProfile(userProfile, connection, transaction);
               
            }
        }

        private static void UpdateUserProfile(UserProfile userProfile, IDbConnection connection, IDbTransaction transaction)
        {
            connection.Update(userProfile, transaction);
            transaction.Commit();
        }


        public int InsertUserProfile(UserProfile userProfile)
        {
            int identiyVal;
            using (var connection = DatabaseContext.Connection)
            using (var transaction = connection.BeginTransaction())
            {
                identiyVal = InsertUserProfile(userProfile, connection, transaction);
                transaction.Commit();
            }
            return identiyVal;
        }
        public User GetUserByDeviceId(string deviceId)
        {
            const string selectUser =
                    @"select * from [User] U
                       where U.DeviceId = @DeviceId";
            using (var connection = DatabaseContext.Connection)
            {
                return connection.Query<User>(selectUser, new {@DeviceId = deviceId}).First();
            }
        }

        public User GetUserByAnonymousId(string anonymousId)
        {
            const string selectUser =
                    @"select * from [User] U
                       where U.AnonymousSystemId = @AnonymousSystemId";
            using (var connection = DatabaseContext.Connection)
            {
                return connection.Query<User>(selectUser, new { @AnonymousSystemId = anonymousId }).First();
            }
        }

        public UserProfile GetUserProfileByUserId(int userId,IDbConnection connection)
        {
            const string selectProfile = @"select UP.* from [User] U
                inner join UserProfile UP on U.UserProfileId = UP.UserId
                where U.Id = @Id";
            return connection.Query<UserProfile>(selectProfile, new {@Id = userId}).Single();
        }

        public User GetUserByUserId(int id)
        {
            using (var connection = DatabaseContext.Connection)
            {
                return connection.Get<User>(id);
            }
        }

        public User GetUserByTwitterId(string twitterId)
        {
            const string selectUser =
                   @"select * from [User] U
                       where U.TwitterId = @twitterId";
            using (var connection = DatabaseContext.Connection)
            {
                return connection.Query<User>(selectUser, new {twitterId }).First();
            }
        }

        public void UpdateUser(User user)
        {
             using (var connection = SqlConnection)
             using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.Serializable }))
             {
                 connection.EnlistTransaction(Transaction.Current);

                 var userProfile = GetUserProfileByUserId(user.Id, connection);
                 user.UserProfileId = userProfile.UserId;
                 user.UpdateDt = DateTime.Now;
                 connection.Update(user);
                 //UpdateUserProfile(); // for userName
                 _drinksServices.UpdateDrinkForShared(user, connection);
                 _merchantService.UpdateUserMerchantCommentForShared(user,connection);
                 scope.Complete();
             }
        }

        public void UpdateShare(int userId, bool share)
        {
             using (var connection = SqlConnection)
             using (var scope = new TransactionScope(TransactionScopeOption.Required,
                                                  new TransactionOptions {IsolationLevel = IsolationLevel.Serializable}))
                 
             {
                 connection.EnlistTransaction(Transaction.Current);
                 var user = GetUserByUserId(userId);
                 user.Share = share;
                 connection.Update(user);
                 _drinksServices.UpdateDrinkForShared(user,connection);
                 _merchantService.UpdateUserMerchantCommentForShared(user, connection);
                 scope.Complete();
             }
        }

        public User CreateUserWithUserProfile(User user)
        {
            int userId = 0;

            using (var connection = DatabaseContext.Connection)
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    var userProfile = new UserProfile { UserName = user.AnonymousSystemId };
                    user.UserProfileId = InsertUserProfile(userProfile, connection, transaction);
                    user.CreateDt = DateTime.Now;
                    user.UpdateDt = DateTime.Now;
                    userId = connection.Insert(user, transaction);
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                }

            }

            return GetUserByUserId(userId);
        }

        private static int InsertUserProfile(UserProfile userProfile, IDbConnection connection, IDbTransaction transaction)
        {
            userProfile.IsActive = true;
            int identiyVal = connection.Insert(userProfile, transaction);
            return identiyVal;
        }
    }
}
