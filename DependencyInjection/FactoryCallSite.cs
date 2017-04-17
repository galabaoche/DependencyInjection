using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DependencyInjection
{
    internal class FactoryCallSite : IServiceCallSite
    {
        public Func<IServiceProvider, object> Factory { get; private set; }
        public FactoryCallSite(Func<IServiceProvider, object> factory)
        {
            this.Factory = factory;
        }
        public Expression Build(Expression provider)
        {
            Expression<Func<IServiceProvider, object>> factory = p => this.Factory(p);
            return Expression.Invoke(factory, provider);
        }
        public object Invoke(ServiceProvider provider)
        {
            return this.Factory(provider);
        }
    }
}
