using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DependencyInjection
{
    internal class ServiceProviderService : IService, IServiceCallSite
    {
        public ServiceLifetime Lifetime => ServiceLifetime.Scoped;
        public IService Next { get; set; }

        public Expression Build(Expression provider)
        {
            return provider;
        }

        public IServiceCallSite CreateCallSite(ServiceProvider provider, ISet<Type> callSiteChain)
        {
            return this;
        }

        public object Invoke(ServiceProvider provider)
        {
            return provider;
        }
    }

}
