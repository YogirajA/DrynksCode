using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using DrynksMe.DataAccess.Infrastructure;
using DrynksMe.DataAccess.Models;
using DrynksMe.Services.Contracts;
using DrynksMe.Services.Models;


namespace DrynksMe.Services
{
    public class DrinksServices : BaseServices, IDrinksServices
    {
        public DrinksServices(IDatabaseContext databaseContext) : base(databaseContext){}
    

        public IEnumerable<Drink> GetAllDrinks()
        {   

            using (var connection = DatabaseContext.Connection)
            {
                return connection.GetList<Drink>().ToList();
            }
        }

        public IEnumerable<DrinksResultModel> GetDrinksForUserByDeviceIdPaged(DrinksFilterModel drinksFilterModel)
        {
            IEnumerable<DrinksResultModel> drinks;

            const string selectDrinkForUser =
                     @"SpGetDrinksByDeviceIdAndType";
            using (var connection = DatabaseContext.Connection)
            {
                drinks = connection.Query<DrinksResultModel>(selectDrinkForUser
                    , new
                    {
                        @DeviceId = drinksFilterModel.DeviceId,
                        @TagName = drinksFilterModel.TagName,
                        @startRow = drinksFilterModel.StartRowNum,
                        @endRow = drinksFilterModel.EndRowNum
                    }, null, true, 30, CommandType.StoredProcedure).ToList();
            }
            return drinks;
        }

        public IEnumerable<DrinksResultModel> GetDrinksByUserIdPaged(DrinksFilterModel drinksFilterModel)
        {
            IEnumerable<DrinksResultModel> drinks;

            const string selectDrinkForUser =
                     @"SpGetDrinksByUserIdAndType";
            using (var connection = DatabaseContext.Connection)
            {
                drinks = connection.Query<DrinksResultModel>(selectDrinkForUser
                    , new
                    {
                        @UserId = drinksFilterModel.UserId,
                        @TagName = drinksFilterModel.TagName,
                        @startRow = drinksFilterModel.StartRowNum,
                        @endRow = drinksFilterModel.EndRowNum
                    },null,true,30,CommandType.StoredProcedure).ToList();
            }
            return drinks;
        }

        public IEnumerable<DrinksResultModel> GetDrinksPaged(DrinksFilterModel drinksFilterModel)
        {
            IEnumerable<DrinksResultModel> drinks;

            const string selectDrinkForUser =
                     @"SpGetDrinksByType";
            using (var connection = DatabaseContext.Connection)
            {
                drinks = connection.Query<DrinksResultModel>(selectDrinkForUser
                    , new
                    {   
                        @TagName = drinksFilterModel.TagName,
                        @startRow = drinksFilterModel.StartRowNum,
                        @endRow = drinksFilterModel.EndRowNum
                    }, null, true, 30, CommandType.StoredProcedure).ToList();
            }
            return drinks;
        }

        public Drink GetDrinkById(int drinkId)
        {
            using (var connection = DatabaseContext.Connection)
            {
               return GetDrinkById(drinkId, connection);
            }
        }

        private static Drink GetDrinkById(int drinkId, IDbConnection connection)
        {
            return connection.Get<Drink>(drinkId);
        }

        public Drink AddDrinkForUser(Drink drink, DrinkUser drinkUser, IEnumerable<string> tags)
        {
            using (var connection = DatabaseContext.Connection)
            {
                int drinkId;
                using (var transaction = connection.BeginTransaction())
                {
                    var currentTime = DateTime.Now;
                    drink.UpdateDt = drink.UpdateDt.HasValue ? drink.UpdateDt : currentTime;
                    drink.CreateDt = drink.CreateDt.HasValue ? drink.CreateDt : currentTime;
                    drinkId = connection.Insert(drink, transaction);
                    drinkUser.DrinkId = drinkId;
                    drinkUser.CreateDt = currentTime;
                    connection.Insert(drinkUser, transaction);


                    var tagNames = tags.ToList();
                    if (tagNames.Any())
                    {
                        var tagIds = GetIdsForTags(drinkId, tagNames, connection, transaction).ToList();

                        tagIds.ForEach(
                            tagId =>
                            connection.Insert(
                                new DrinkTag
                                    {
                                        DrinkId = drinkId,
                                        TagId = tagId,
                                        CreateDt = currentTime,
                                        UpdateDt = currentTime
                                    }, transaction));
                    }

                    transaction.Commit();
                }
                return GetDrinkById(drinkId, connection);
            }
            
        }

        public int AddDrinkForDevice(Drink drink, string deviceId, DrinkUser drinkUser, IEnumerable<string> tags)
        {
            const string selectUserByDeviceId = @"Select * from [User]  where DeviceId =  @DeviceId";
            using (var connection = DatabaseContext.Connection)
            {
                var userId = connection.Query<User>(selectUserByDeviceId, new { @DeviceId = deviceId }).First().Id;
               
                using (var transaction = connection.BeginTransaction())
                {
                    var currentTime = DateTime.Now;
                    drink.UpdateDt = drink.UpdateDt.HasValue ? drink.UpdateDt : currentTime;
                    drink.CreateDt = drink.CreateDt.HasValue ? drink.CreateDt : currentTime;
                    var drinkId = connection.Insert(drink, transaction);
                    drinkUser.DrinkId = drinkId;
                    drinkUser.UserId = userId;
                    drinkUser.CreateDt = currentTime;
                    connection.Insert(drinkUser, transaction);

                    var tagNames =  tags.ToList();
                    if (tagNames.Any())
                    {
                        var tagIds = GetIdsForTags(drinkId, tagNames, connection, transaction).ToList();

                        tagIds.ForEach(tagId => connection.Insert(new DrinkTag
                            {
                                DrinkId = drinkId,
                                TagId = tagId,
                                CreateDt = currentTime,
                                UpdateDt = currentTime
                            }, transaction));
                    }
                    transaction.Commit();
                    return drinkId;
                }
            }
        }

        private IEnumerable<int> GetIdsForTags(int drinkId, IEnumerable<string> tags,IDbConnection connection,IDbTransaction transaction)
        {
            var tagIds= new List<int>();
            const string sql = @"Select * from Tag where TagName = @TagName";
          
            foreach (var tag in tags)
            {
                var idsInDb = connection.Query<Tag>(sql, new {@TagName = tag},transaction).Select(x => x.Id).ToList();
                if (idsInDb.Any())
                {
                    tagIds.AddRange(idsInDb);
                }
                else
                {
                    //insert
                    var id = connection.Insert(new Tag {TagName = tag},transaction);
                    tagIds.Add(id);
                }
                
            }

            return tagIds;
        }

        public Drink EditDrink(Drink drink, DrinkUser drinkUser, IEnumerable<string> tags)
        {
            const string deleteTagsSQL = "Delete From DrinkTag where DrinkId=@DrinkId";
            const string selectDrinkUserSQL = "Select From DrinkUser where DrinkId=@DrinkId and UserId=@UserId";
            using (var connection = DatabaseContext.Connection)
            {   
                using (var transaction = connection.BeginTransaction())
                {
                    var currentTime = DateTime.Now;
                    drink.UpdateDt = drink.UpdateDt.HasValue ? drink.UpdateDt : currentTime;

                    connection.Update(drink, transaction);

                    var dbDrinkUser = connection.Query<DrinkUser>(selectDrinkUserSQL,
                                                                  new {@DrinkId = drink.Id, @UserId = drinkUser.UserId},
                                                                  transaction).First();

                    if (dbDrinkUser != null)
                    {
                        dbDrinkUser.IsLiked = drinkUser.IsLiked;
                        dbDrinkUser.UserNotes = drinkUser.UserNotes;
                        connection.Update(dbDrinkUser, transaction);
                    }


                    var tagNames = tags.ToList();
                    if (tagNames.Any())
                    {
                        connection.Execute(deleteTagsSQL, new {@DrinkId = drink.Id}, transaction);
                        var tagIds = GetIdsForTags(drink.Id, tagNames, connection, transaction).ToList();

                        tagIds.ForEach(tagId => connection.Insert(new DrinkTag
                            {
                                DrinkId = drink.Id,
                                TagId = tagId,
                                CreateDt = currentTime,
                                UpdateDt = currentTime
                            }, transaction));
                    }


                    transaction.Commit();
                }

                return GetDrinkById(drink.Id);
            }

        }

        //public void DeleteDrinkForUser(int drinkId, int userId)
        //{
        //    var drinkUser = new DrinkUser {UserId = userId, DrinkId = drinkId};
        //    using (var connection = DatabaseContext.Connection)
        //    using (var transaction = connection.BeginTransaction())
        //    {
               
        //        connection.Delete(drinkUser,transaction);
        //        transaction.Commit();
        //    }
           
        //}

        public void UpdateDrinkForShared(User user, IDbConnection connection)
        {
            const string sqlToUpdateDrinkForShare = @"UPDATE D
                                                    SET D.IsShared = @IsShared
                                                    FROM Drink D
                                                    inner join DrinkUser DU on D.Id = DU.DrinkId
                                                    where DU.UserId = @UserId";
            connection.Execute(sqlToUpdateDrinkForShare, new {@IsShared = user.Share, @UserId = user.Id});
        }
    }
}
