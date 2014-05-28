using System.Web;
using System.Web.Mvc;
using DrynksMe.Portal.Filters;
using DrynksMe.Portal.Helpers;
using DrynksMe.Services.Logging;

namespace DrynksMe.Portal
{
    public class FilterConfig
    {
       
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            
            //filters.Add(new HandleErrorAttribute());
            filters.Add(new DrynksPortalHandleError(new NLogger()));
            filters.Add(new InitializeSimpleMembershipAttribute());
        }
    }
}