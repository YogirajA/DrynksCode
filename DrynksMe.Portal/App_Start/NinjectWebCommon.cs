using System.Configuration;
using DrynksMe.DataAccess.Infrastructure;
using DrynksMe.Services;
using DrynksMe.Services.Contracts;

[assembly: WebActivator.PreApplicationStartMethod(typeof(DrynksMe.Portal.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(DrynksMe.Portal.NinjectWebCommon), "Stop")]

namespace DrynksMe.Portal
{
    using System;
    using System.Web;
    using Services.Logging;
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
            var kernel = new StandardKernel(new NinjectSettings
                {
                    InjectNonPublic = true
                    //,LoadExtensions = true
                });
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
           
            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            

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

            kernel.Bind<ILogger>()
                  .To<NLogger>();

          

        }        
    }
}
