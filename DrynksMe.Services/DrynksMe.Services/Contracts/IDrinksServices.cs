using System.Collections.Generic;
using System.Data;
using DrynksMe.DataAccess.Models;
using DrynksMe.Services.Models;

namespace DrynksMe.Services.Contracts
{
    public interface IDrinksServices
    {
        IEnumerable<Drink> GetAllDrinks();

        Drink AddDrinkForUser(Drink drink, DrinkUser drinkUser, IEnumerable<string> tags);
        int AddDrinkForDevice(Drink drink, string deviceId, DrinkUser drinkUser, IEnumerable<string> tags);
        Drink EditDrink(Drink drink, DrinkUser drinkUser, IEnumerable<string> tags);
        //void DeleteDrinkForUser(int drinkId, int userId);
    
        Drink GetDrinkById(int drinkId);
        IEnumerable<DrinksResultModel> GetDrinksForUserByDeviceIdPaged(DrinksFilterModel drinksFilterModel);
        IEnumerable<DrinksResultModel> GetDrinksByUserIdPaged(DrinksFilterModel drinksFilterModel);
        IEnumerable<DrinksResultModel> GetDrinksPaged(DrinksFilterModel drinksFilterModel);
        void UpdateDrinkForShared(User user, IDbConnection connection);
    }
}