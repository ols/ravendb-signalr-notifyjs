using System.Web.Mvc;
using System.Web.Routing;
using Castle.Windsor;
using NLog;
using rampsnamp.Core;
using rampsnamp.Web.RavenDBObservers;
using rampsnamp.Web.WindsorSetup;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Extensions;
using Raven.Client.UniqueConstraints;

namespace rampsnamp.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        public static IWindsorContainer WindsorContainer;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            AutoMapperConfigurator.InitializeAutoMapper();

            var store = new DocumentStore { ConnectionStringName = "RavenDb" };
            store.RegisterListener(new UniqueConstraintsStoreListener());
            store.Initialize();
            store.DatabaseCommands.GlobalAdmin.EnsureDatabaseExists("Rampsnamp");

            WindsorContainer = new WindsorContainer();

            WindsorContainer.Install(
                new RavenDBInstaller(store),
                new ProjectInstaller(),
                new ControllersInstaller());

            CheckRavenForUserChanges(store);

            WindsorServiceLocator.Initialize(WindsorContainer);
            var controllerFactory = new WindsorControllerFactory(WindsorContainer.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);

            _logger.Debug("Starting rampsnamp!");
        }

        private static void CheckRavenForUserChanges(IDocumentStore store)
        {
            store
                .Changes()
                .ForDocumentsStartingWith("Users/")
            .Subscribe(new UsersObserver());
        }
    }
}
