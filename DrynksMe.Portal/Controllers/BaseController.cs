using System.Web.Mvc;
using DrynksMe.Services.Contracts;

namespace DrynksMe.Portal.Controllers
{
    [Authorize]
    public class BaseController : AsyncController
    {
        protected readonly IMembershipService MembershipService;
        protected readonly IMerchantService MerchantService;
        protected readonly IDrinksServices DrinksServices;

        public BaseController(IMembershipService membershipService,IMerchantService merchantService,IDrinksServices drinksServices)
        {
            MembershipService = membershipService;
            MerchantService = merchantService;
            DrinksServices = drinksServices;
        }
    }
}
