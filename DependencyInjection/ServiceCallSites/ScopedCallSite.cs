using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Collections.Concurrent;
using System.Reflection;

namespace DependencyInjection
{
    internal class ScopedCallSite : IServiceCallSite
    {
        public IService Service { get; private set; }
        public IServiceCallSite ServiceCallSite { get; private set; }

        public ScopedCallSite(IService service, IServiceCallSite serviceCallSite)
        {
            this.Service = service;
            this.ServiceCallSite = serviceCallSite;
        }

        public virtual Expression Build(Expression provider)
        {
            var service = Expression.Constant(this.Service);
            var instance = Expression.Variable(typeof(object), "instance");
            var resolvedServices = Expression.Property(provider, "ResolvedServices");
            var tryGetValue = Expression.Call(resolvedServices, "TryGetValue", null, service, instance);
            var index = Expression.MakeIndex(resolvedServices, typeof(ConcurrentDictionary<IService, object>).GetProperty("Item"), new Expression[] { service });
            var assign = Expression.Assign(index, this.ServiceCallSite.Build(provider));

            return Expression.Block(typeof(object), new[] { instance}, Expression.IfThen(Expression.Not(tryGetValue), assign), index);
        }

        public virtual object Invoke(ServiceProvider provider)
        {
            object instance;
            return provider.ResolvedServices.TryGetValue(this.Service, out instance)
                ? instance
                : provider.ResolvedServices[this.Service] = this.ServiceCallSite.Invoke(provider);
        }
    }
}
