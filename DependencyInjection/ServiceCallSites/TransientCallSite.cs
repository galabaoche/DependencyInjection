using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DependencyInjection
{
    internal class TransientCallSite : IServiceCallSite
    {
        public IServiceCallSite ServiceCallSite { get; private set; }
        public TransientCallSite(IServiceCallSite serviceCallSite)
        {
            this.ServiceCallSite = serviceCallSite;
        }

        public Expression Build(Expression provider)
        {
            return Expression.Call(provider, "CaptureDisposable", null, this.ServiceCallSite.Build(provider));
        }

        public object Invoke(ServiceProvider provider)
        {
            return provider.CaptureDisposable(this.ServiceCallSite.Invoke(provider));
        }
    }

}
