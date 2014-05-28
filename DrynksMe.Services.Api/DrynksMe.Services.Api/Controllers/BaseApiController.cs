using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using DrynksMe.DataAccess.Models;
using DrynksMe.Services.Contracts;
using DrynksMe.Services.Logging;

namespace DrynksMe.Services.Api.Controllers
{
    //[ApiAuthorize]
    public class BaseApiController : ApiController
    {
        protected readonly IMembershipService MembershipService;
        protected IDrinksServices DrinksServices;
        protected readonly ILogger Logger;
        protected readonly IMerchantService MerchantService;

        private User _user;
        public User CurrentUser
        {
            get
            {
                if (_user != null)
                    return _user;

                _user = SetPropertiesFromPrincipal();
                return _user;
            }
           
        }

        public BaseApiController(IMembershipService membershipService, IDrinksServices drinksServices, ILogger logger,IMerchantService merchantService)
        {
            MembershipService = membershipService;
            DrinksServices = drinksServices;
            Logger = logger;
            MerchantService = merchantService;

            //SetPropertiesFromPrincipal();
        }



        private User SetPropertiesFromPrincipal()
        {
            var claims = ClaimsPrincipal.Current.Claims.ToList();

            var anonymousId = claims.First(x => x.Type == Constants.HttpCustomclaimAnonymousid);
           
            int id;
            int.TryParse(claims.Single(x => x.Type == Constants.HttpCustomclaimUserId).Value, out id);

            var twitterId = claims.First(x => x.Type == Constants.HttpCustomclaimTwitterId);
            User user;
            if (id > 0)
            {
                user = MembershipService.GetUserByUserId(id);
            }
            else if (anonymousId != null && !string.IsNullOrEmpty(anonymousId.Value))
            {
                user = MembershipService.GetUserByAnonymousId(anonymousId.Value);
            }
            else
            {
                //let it exception at this point.
                user = MembershipService.GetUserByTwitterId(twitterId.Value);
            }

            return user;


        }
    }
}
