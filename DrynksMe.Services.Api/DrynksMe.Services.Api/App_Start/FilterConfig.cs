using System.Web.Mvc;
using DrynksMe.Common;
using DrynksMe.Services.Logging;

namespace DrynksMe.Services.Api
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
           // filters.Add(new HandleErrorAttribute());
            filters.Add(new DrynksErrorAttribute(new NLogger()));
        }
    }
}