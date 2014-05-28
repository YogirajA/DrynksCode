using System.Web.Mvc;
using DrynksMe.Services.Logging;
using Ninject;

namespace DrynksMe.Portal.Helpers
{
    public class DrynksPortalHandleError : HandleErrorAttribute
    {
        private readonly ILogger _logger;
        public DrynksPortalHandleError(ILogger logger)
        {
            _logger = logger;
        }
        //[Inject]
        //public ILogger Logger { get; set; }

        public override void OnException(ExceptionContext filterContext)
        {   
            _logger.LogError(filterContext.Exception);
            base.OnException(filterContext);
            
        }
    }
}