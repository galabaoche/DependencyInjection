using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace DependencyInjection
{
    internal class ServiceScopeFactoryService : IService, IServiceCallSite
    {
        public ServiceLifetime Lifetime => ServiceLifetime.Scoped;
        public IService Next { get; set; }

        public IServiceCallSite CreateCallSite(ServiceProvider provider, ISet<Type> callSiteChain)
        {
            return this;
        }

        public Expression Build(Expression provider)
        {
            return Expression.New(typeof(ServiceScopeFactory).GetConstructors().Single(), provider);
        }

        public object Invoke(ServiceProvider provider)
        {
            return new ServiceScopeFactory(provider);
        }
    }

}
