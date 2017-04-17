using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DependencyInjection
{
    internal class Service : IService
    {
        public ServiceDescriptor ServiceDescriptor { get; private set; }
        public ServiceLifetime Lifetime => this.ServiceDescriptor.Lifetime;
        public IService Next { get; set; }

        public Service(ServiceDescriptor serviceDescriptor)
        {
            this.ServiceDescriptor = serviceDescriptor;
        }

        public IServiceCallSite CreateCallSite(ServiceProvider provider, ISet<Type> callSiteChain)
        {
            IServiceCallSite serviceCallSite =
           (null != this.ServiceDescriptor.ImplementationInstance)
           ? new InstanceCallSite(this.ServiceDescriptor.ImplementationInstance)
           : null;
     
       serviceCallSite = serviceCallSite ??
           ((null != this.ServiceDescriptor.ImplementationFactory)
           ? new FactoryCallSite(this.ServiceDescriptor.ImplementationFactory)
           : null);
      
       serviceCallSite = serviceCallSite ?? this.CreateConstructorCallSite(provider, callSiteChain);
      
       switch (this.Lifetime)
       {
                   case ServiceLifetime.Transient: return new TransientCallSite(serviceCallSite);
                          case ServiceLifetime.Scoped: return new ScopedCallSite(this, serviceCallSite);
                          default: return new SingletonCallSite(this, serviceCallSite);
                      }
        }

        private ConstructorCallSite CreateConstructorCallSite(ServiceProvider provider, ISet<Type> callSiteChain)
        {
            ConstructorInfo constructor = this.GetConstructor(provider, callSiteChain);
            if (null == constructor)
            {
                throw new InvalidOperationException("No avaliable constructor");
            }
            return new ConstructorCallSite(constructor, constructor.GetParameters().Select(p => provider.GetServiceCallSite(p.ParameterType, callSiteChain)).ToArray());
        }

        private ConstructorInfo GetConstructor(ServiceProvider provider, ISet<Type> callSiteChain)
        {
            ConstructorInfo[] constructors = this.ServiceDescriptor.ImplementationType.GetConstructors()
                .Where(c => (null != this.GetParameterCallSites(c, provider, callSiteChain))).ToArray();

            Type[] allParameterTypes = constructors.SelectMany(
                c => c.GetParameters().Select(p => p.ParameterType)).Distinct().ToArray();

            return constructors.FirstOrDefault(
                 c => new HashSet<Type>(c.GetParameters().Select(p => p.ParameterType)).IsSupersetOf(allParameterTypes));
        }

        private IServiceCallSite[] GetParameterCallSites(ConstructorInfo constructor, ServiceProvider provider, ISet<Type> callSiteChain)
        {
            ParameterInfo[] parameters = constructor.GetParameters();
            IServiceCallSite[] serviceCallSites = new IServiceCallSite[parameters.Length];

            for (int index = 0; index < serviceCallSites.Length; index++)
            {
                ParameterInfo parameter = parameters[index];
                IServiceCallSite serviceCallSite = provider.GetServiceCallSite(
                    parameter.ParameterType, callSiteChain);
                if (null == serviceCallSite && parameter.HasDefaultValue)
                {
                    serviceCallSite = new InstanceCallSite(parameter.DefaultValue);
                }
                if (null == serviceCallSite)
                {
                    return null;
                }
                serviceCallSites[index] = serviceCallSite;
            }
            return serviceCallSites;
        }
    }
}
