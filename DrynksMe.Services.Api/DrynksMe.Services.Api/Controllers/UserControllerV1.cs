using System.Net;
using System.Net.Http;
using System.Web.Http;
using DrynksMe.Services.Api.Models;
using DrynksMe.Services.Contracts;
using DrynksMe.Services.Logging;

namespace DrynksMe.Services.Api.Controllers
{
    public class UserControllerV1 : BaseApiController
    {
        //
        // GET: /UserControllerV1/


        public UserControllerV1(IMembershipService membershipService, IDrinksServices drinksServices, ILogger logger, IMerchantService merchantService) : base(membershipService, drinksServices, logger, merchantService)
        {
        }

        [HttpPost]
        public HttpResponseMessage Register([FromBody] UserModel userModel)
        {
            if (ModelState.IsValid && userModel != null)
            {   
               var user = MembershipService.CreateUserWithUserProfile(userModel.FromUserModel());
               return Request.CreateResponse(HttpStatusCode.Created, user.ToUserModel());
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        // For userName password.. we need a different method that creates membership
        [HttpPut]
        public HttpResponseMessage Update([FromBody] UserModel userModel)
        {
            if (ModelState.IsValid && userModel != null)
            {
                var user = userModel.FromUserModel();
                MembershipService.UpdateUser(user);
                return Request.CreateResponse(HttpStatusCode.Accepted);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
        [HttpPut]
        public HttpResponseMessage Update([FromUri] bool share)
        {
            CurrentUser.Share = share;
            MembershipService.UpdateUser(CurrentUser);
            return Request.CreateResponse(HttpStatusCode.Accepted);
        }

        public UserModel Get()
        {
            return CurrentUser.ToUserModel();
        }

        [HttpPost]
        public HttpResponseMessage AddVenueComment(VenueComment venueComment)
        {
            MerchantService.AddVenueComment(CurrentUser, venueComment.VenueId, venueComment.Comment);
            return Request.CreateResponse(HttpStatusCode.Created,venueComment);
        }
       
    }
}
