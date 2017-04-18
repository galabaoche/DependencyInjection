using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DependencyInjection
{
    internal class SingletonCallSite : ScopedCallSite
    {
        public SingletonCallSite(IService service, IServiceCallSite serviceCallSite) :
        base(service, serviceCallSite)
        { }

        public override Expression Build(Expression provider)
        {
            return base.Build(Expression.Property(provider, "Root"));
        }

        public override object Invoke(ServiceProvider provider)
        {
            return base.Invoke(provider.Root);
        }
    }

}
