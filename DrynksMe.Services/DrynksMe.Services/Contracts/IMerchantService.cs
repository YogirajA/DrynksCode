using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using DrynksMe.DataAccess.Models;
using DrynksMe.Services.Models;

namespace DrynksMe.Services.Contracts
{
    public interface IMerchantService
    {
        IEnumerable<Merchant> GetVenues();
        int AddVenue(Merchant merchant, int[] merchantTypeIds);
        int UpdateVenue(Merchant merchant, int[] merchantTypeIds);
        MerchantGroup GetVenue(int id);
        IEnumerable<MerchantSearchResult> Search(MerchantSearchModel merchantSearchModel);
        IEnumerable<MerchantType> GetAllMerchantTypes();
        IEnumerable<Merchant> GetFeaturedMerchants();
        IEnumerable<Merchant> GetMerchantsByCoordinates(double longitude, double latitude,int radius);
        IEnumerable<Merchant> SearchByTags(IEnumerable<string> tags);
        IEnumerable<DrinksResultModel> GetDrinks(int merchantId);
        MerchantWithComments GetVenueWithComments(int id);
        void AddVenueComment(User User,int merchantId, string comment);
        void UpdateUserMerchantCommentForShared(User user, IDbConnection connection);
    }
}