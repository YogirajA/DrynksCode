using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Claims;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading;
using System.Web;
using DrynksMe.DataAccess.Infrastructure;
using DrynksMe.Services.Api.Models;
using DrynksMe.Services.Contracts;
using Newtonsoft.Json;
using Claim = System.Security.Claims.Claim;
using ClaimTypes = System.Security.Claims.ClaimTypes;

namespace DrynksMe.Services.Api.Security
{
    public class SecurityService : ISecurityService
    {
       

        public bool Authenticated(DrynksApiHeader header)
        {
           
            //if (header.UserId > 0)
            //{
            //    _membershipService.GetUserByDeviceId()
            //}
            return header.ApiKey == "Blah";
            //ValidateUserNamePW if required

            //return true;
        }

        public bool Authorized(DrynksApiHeader header)
        {
            return true;
        }

        public void SetCurrentPrincipal(DrynksApiHeader header)
        {
           
            var claims = new List<Claim>
                {
                    //new Claim(ClaimTypes.Name, "ApiUser"),
                    //new Claim(ClaimTypes.Role, "User"),
                    new Claim(Constants.HttpCustomclaimAnonymousid,header.AnonymousSystemId,Rights.PossessProperty),
                    new Claim(Constants.HttpCustomclaimUserId,header.UserId.ToString(CultureInfo.InvariantCulture),Rights.PossessProperty),
                    new Claim(Constants.HttpCustomclaimTwitterId,string.IsNullOrEmpty(header.TwitterId)?"":header.TwitterId,Rights.PossessProperty)

                };

        
              
         
            var claimsIdentity = new ClaimsIdentity(claims, "DrynksAPI", ClaimTypes.Name, ClaimTypes.Role);

            var principal = new ClaimsPrincipal(claimsIdentity);

            Thread.CurrentPrincipal = principal;
            
        }
    }
}