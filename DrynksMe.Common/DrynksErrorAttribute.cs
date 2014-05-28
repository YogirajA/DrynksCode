using System.Web.Mvc;
using DrynksMe.Services.Logging;

namespace DrynksMe.Common
{   
    public class DrynksErrorAttribute : HandleErrorAttribute
    {
        private readonly ILogger _logger;
        public DrynksErrorAttribute(ILogger logger)
        {
            _logger = logger;
        }
        public override void OnException(ExceptionContext filterContext)
        {
            _logger.LogError(filterContext.Exception);
            base.OnException(filterContext);

        }
    }
}
