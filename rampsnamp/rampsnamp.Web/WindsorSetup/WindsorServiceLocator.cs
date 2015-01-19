using System;
using System.Collections;
using System.Linq;
using System.Text;
using Castle.MicroKernel;
using Castle.MicroKernel.Handlers;
using Castle.Windsor;
using Castle.Windsor.Diagnostics;

namespace rampsnamp.Web.WindsorSetup
{
    public static class WindsorServiceLocator
    {
        private static IWindsorContainer _container;

        public static void Initialize(IWindsorContainer windsorContainer)
        {
            GlobalContainer = windsorContainer;
        }

        public static void CheckForPotentiallyMisconfiguredComponents(IWindsorContainer windsorContainer, string projectName)
        {
            var host = (IDiagnosticsHost)windsorContainer.Kernel.GetSubSystem(SubSystemConstants.DiagnosticsKey);
            var diagnostics = host.GetDiagnostic<IPotentiallyMisconfiguredComponentsDiagnostic>();

            var handlers = diagnostics.Inspect();

            if (handlers.Any())
            {
                var message = new StringBuilder();
                var inspector = new DependencyInspector(message);

                foreach (var handler1 in handlers)
                {
                    var handler = (IExposeDependencyInfo) handler1;
                    handler.ObtainDependencyDetails(inspector);
                }

                throw new MisconfiguredComponentException(string.Concat("Misconfigured components in: ", projectName, " --- ", message.ToString()));
            }
        }

        public static object Resolve(Type serviceType)
        {
            return Container.Resolve(serviceType);
        }

        public static object Resolve(Type serviceType, string serviceName)
        {
            return Container.Resolve(serviceName, serviceType);
        }

        public static T TryResolve<T>()
        {
            return TryResolve(default(T));
        }

        public static T TryResolve<T>(T defaultValue)
        {
            if (Container.Kernel.HasComponent(typeof(T)) == false)
                return defaultValue;
            return Container.Resolve<T>();
        }

        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }

        public static T Resolve<T>(string name)
        {
            return Container.Resolve<T>(name);
        }

        public static T Resolve<T>(object argumentsAsAnonymousType)
        {
            return Container.Resolve<T>(argumentsAsAnonymousType);
        }


        public static T Resolve<T>(IDictionary parameters)
        {
            return Container.Resolve<T>(parameters);
        }

        public static IWindsorContainer Container
        {
            get
            {
                IWindsorContainer result = GlobalContainer;
                if (result == null)
                    throw new InvalidOperationException("The container has not been initialized! Please call IoC.Initialize(container) before using it.");
                return result;
            }
        }

        public static bool IsInitialized
        {
            get { return GlobalContainer != null; }
        }

        internal static IWindsorContainer GlobalContainer
        {
            get
            {
                return _container;
            }
            set
            {
                _container = value;
            }
        }

        public static void Reset(IWindsorContainer containerToReset)
        {
            if (containerToReset == null)
                return;
            if (ReferenceEquals(GlobalContainer, containerToReset))
            {
                GlobalContainer = null;
            }
        }

        public static void Reset()
        {
            IWindsorContainer windsorContainer = GlobalContainer;
            Reset(windsorContainer);
        }

        public static Array ResolveAll(Type service)
        {
            return Container.ResolveAll(service);
        }

        public static T[] ResolveAll<T>()
        {
            return Container.ResolveAll<T>();
        }
    }

    [Serializable]
    public class MisconfiguredComponentException : Exception
    {
        public MisconfiguredComponentException() { }

        public MisconfiguredComponentException(string message) : base(message) { }

        public MisconfiguredComponentException(string message, Exception inner) : base(message, inner) { }
    }
}