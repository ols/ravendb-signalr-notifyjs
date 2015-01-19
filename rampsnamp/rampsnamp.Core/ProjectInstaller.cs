using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace rampsnamp.Core
{
    public class ProjectInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IUserQuery>().ImplementedBy<UserQuery>().LifestyleTransient());
            container.Register(Component.For<IUserService>().ImplementedBy<UserService>().LifestyleTransient());
        }
    }
}