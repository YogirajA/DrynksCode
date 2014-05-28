using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using DrynksMe.Common;
using DrynksMe.Services.Api.Models;
using DrynksMe.Services.Api.Security;
using Newtonsoft.Json;

namespace DrynksMe.Services.Api
{
    public class ApiAuthorize : AuthorizeAttribute
    {
       // private readonly ISecurityService _securityService;
       
        public ISecurityService SecurityService { get; set; }

        //figure out ninject issuees
        public ApiAuthorize()
            : this(new SecurityService())
        {

        }
        public ApiAuthorize(ISecurityService securityService)
        {
            SecurityService = securityService;
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {

            var apiHeader = GetDrynksApiHeaderUnSecured(actionContext);
            if (apiHeader == null)
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest);

            if (SecurityService.Authenticated(apiHeader))
            {
                if (SecurityService.Authorized(apiHeader))
                    SecurityService.SetCurrentPrincipal(apiHeader);
               else
                   actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                   
            }
            else
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
            
        }

        private static DrynksApiHeader GetDrynksApiHeaderUnSecured(HttpActionContext actionContext)
        {
            
            var httpRequestHeaders = actionContext.Request.Headers;
            
            var userId = httpRequestHeaders.Any(x => x.Key == "UserId") ? httpRequestHeaders.GetValues("UserId").First() : null;
            var systemId = httpRequestHeaders.Any(x => x.Key == "AnonymousSystemId") ? httpRequestHeaders.GetValues("AnonymousSystemId").First() : null;
            var twitterId = httpRequestHeaders.Any(x => x.Key == "TwitterId") ? httpRequestHeaders.GetValues("TwitterId").First() : null;
            
            if(string.IsNullOrEmpty(userId) && string.IsNullOrEmpty(systemId) && string.IsNullOrEmpty(twitterId))
                throw new Exception("At least one id is required");

            int id;
            int.TryParse(userId, out id);
            
            return new DrynksApiHeader
                {
                    AnonymousSystemId = systemId,
                    UserId = id,
                    TwitterId = twitterId,
                    ApiKey = "Blah",
                    DeviceId = "1111"
                };
        }

        private static DrynksApiHeader GetDrynksApiHeader(HttpActionContext actionContext)
        {
            var authorizationToken = actionContext.Request.Headers.GetValues("Authorization-Token").ToList();

            if (!authorizationToken.Any())
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest);

            var header = RSAClass.Decrypt(authorizationToken.First());
            var apiHeader = JsonConvert.DeserializeObject<DrynksApiHeader>(header);
            return apiHeader;
        }
    }
}