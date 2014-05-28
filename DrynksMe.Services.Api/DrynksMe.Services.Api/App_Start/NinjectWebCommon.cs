using System.Linq;
using System.Web.Http;
using DrynksMe.DataAccess.Infrastructure;
using DrynksMe.Services.Api.Security;
using DrynksMe.Services.Contracts;
using DrynksMe.Services.Logging;
using NLog;
using Ninject.Activation;
using Ninject.Web.Mvc.Filter;

[assembly: WebActivator.PreApplicationStartMethod(typeof(DrynksMe.Services.Api.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(DrynksMe.Services.Api.NinjectWebCommon), "Stop")]

namespace DrynksMe.Services.Api
{
    using System;
    using System.Configuration;
    using System.Web;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            GlobalConfiguration.Configuration.DependencyResolver = new NinjectResolver(kernel);
            RegisterServices(kernel);


            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<ILogger>().To<NLogger>();
          
            var databaseContext = new DatabaseContext(ConfigurationManager.ConnectionStrings["DrynksMeDBEntities"].ConnectionString);
            kernel.Bind<IDrinksServices>()
                  .To<DrinksServices>()
                  .WithConstructorArgument("databaseContext", databaseContext);

            kernel.Bind<IMerchantService>()
                  .To<MerchantService>()
                  .WithConstructorArgument("databaseContext", databaseContext);

            kernel.Bind<IMembershipService>()
                  .To<MembershipService>()
                  .WithConstructorArgument("databaseContext", databaseContext);

            kernel.Bind<ISecurityService>()
                  .To<SecurityService>();

         
        }

       
    }

}
