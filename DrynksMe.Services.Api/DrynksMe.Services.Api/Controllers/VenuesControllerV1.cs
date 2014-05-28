using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using DrynksMe.Services.Api.Models;
using DrynksMe.Services.Contracts;
using DrynksMe.Services.Logging;

namespace DrynksMe.Services.Api.Controllers
{
    public class VenuesControllerV1 : BaseApiController
    {
        //
        // GET: /Venues/


        public VenuesControllerV1(IMembershipService membershipService, IDrinksServices drinksServices, ILogger logger, IMerchantService merchantService) : base(membershipService, drinksServices, logger, merchantService)
        {
        }

        public IEnumerable<VenueModel> GetFeatured()
        {
            return MerchantService.GetFeaturedMerchants().Select(x => x.ToVenue()).ToList();
        }
        [HttpGet]
        public IEnumerable<VenueModel> Nearby(double longitude, double latitude,int radius)
        {
            return
                MerchantService.GetMerchantsByCoordinates(longitude,latitude,radius).Select(x => x.ToVenue()).ToList();
        }

        [HttpGet]
        public IEnumerable<VenueModel> Search([FromUri] IEnumerable<string> tags)
        {
            return MerchantService.SearchByTags(tags).Select(x => x.ToVenue()).ToList();
        }

        public VenueModel Get(int id)
        {
            var merchantWithComments = MerchantService.GetVenueWithComments(id);
            var venuModel = merchantWithComments.Merchant.ToVenue();
            venuModel.Comments = merchantWithComments.Comments;
            return venuModel;
        }

        [HttpGet]
        public IEnumerable<DrinksModel> Drinks(int id)
        {
            return MerchantService.GetDrinks(id).Select(x => x.ToDrinksModel()).ToList();
        }

        //social stuff
        //User comments associated to the venue
        //Like or not
    }
}
