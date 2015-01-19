using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Raven.Client;

namespace rampsnamp.Core
{
    public class RavenDBInstaller : IWindsorInstaller
    {
        private readonly IDocumentStore _store;

        public RavenDBInstaller(IDocumentStore store)
        {
            _store = store;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IDocumentStore>().Instance(_store).LifestyleSingleton());
            container.Register(Component.For<IDocumentSession>().LifestylePerWebRequest().UsingFactoryMethod(k => _store.OpenSession()));
        }
    }
}