using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjection
{
    public class ServiceDescriptor
    {
        public ServiceDescriptor(Type serviceType, object instance)
        {
            this.ServiceType = serviceType;
            this.ImplementationInstance = instance;
        }
        public ServiceDescriptor(Type serviceType, Func<IServiceProvider, object> factory, ServiceLifetime lifetime)
        {
            this.ServiceType = serviceType;
            this.ImplementationFactory = factory;
            this.Lifetime = lifetime;
        }
        public ServiceDescriptor(Type serviceType, Type implementationType, ServiceLifetime lifetime)
        {
            this.ServiceType = serviceType;
            this.ImplementationType = implementationType;
            this.Lifetime = lifetime;
        }

        public Type ServiceType { get; }
        public ServiceLifetime Lifetime { get; }

        public Type ImplementationType { get; }
        public object ImplementationInstance { get; }
        public Func<IServiceProvider, object> ImplementationFactory { get; }
    }
}
