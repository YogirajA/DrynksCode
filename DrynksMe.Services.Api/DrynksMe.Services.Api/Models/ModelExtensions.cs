using AutoMapper;
using DrynksMe.DataAccess.Models;
using DrynksMe.Services.Models;

namespace DrynksMe.Services.Api.Models
{
    public static class ModelExtensions
    {
        public static Drink ToDrink(this DrinksModel drinksModel)
        {
            return Mapper.Map<Drink>(drinksModel);
        }

        public static DrinksModel ToDrinksModel(this DrinksResultModel drink)
        {
            var drinkModel = Mapper.Map<DrinksModel>(drink);
                             
            return drinkModel;
        }

        public static DrinksModel ToDrinksModel(this Drink drink)
        {
            return Mapper.Map<DrinksModel>(drink);
        }

        public static User FromUserModel(this UserModel userModel)
        {
            return Mapper.Map<User>(userModel);
        }

        public static UserModel ToUserModel(this User user)
        {
            return Mapper.Map<UserModel>(user);
        }

        public static VenueModel ToVenue(this Merchant merchant)
        {
            return Mapper.Map<VenueModel>(merchant);
        }

        public static VenueModel ToVenue(this MerchantWithComments merchant)
        {
            return Mapper.Map<VenueModel>(merchant);
        }
    }
}