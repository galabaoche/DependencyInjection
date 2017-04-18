using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Concurrent;


namespace DependencyInjection
{
    internal class ServiceProvider : IServiceProvider, IDisposable
    {
        public ServiceProvider Root { get; private set; }
        public ServiceTable ServiceTable { get; private set; }
        public ConcurrentDictionary<Type, Func<ServiceProvider, object>> RealizedServices { get; private set; } = new ConcurrentDictionary<Type, Func<ServiceProvider, object>>();
        public IList<IDisposable> TransientDisposableServices { get; private set; } = new List<IDisposable>();
        public ConcurrentDictionary<IService, object> ResolvedServices { get; private set; } = new ConcurrentDictionary<IService, object>();
        internal ServiceProvider(ServiceProvider parent)
        {
            Root = parent.Root;
            this.ServiceTable = parent.ServiceTable;
        }
        public ServiceProvider(IServiceCollection services)
        {
            this.Root = this;
            this.ServiceTable = new ServiceTable(services);
        }

        public object GetService(Type serviceType)
        {
            Func<ServiceProvider, object> serviceAccessor;
            if (this.RealizedServices.TryGetValue(serviceType, out serviceAccessor))
            {
                return serviceAccessor(this);
            }

            IServiceCallSite serviceCallSite = this.GetServiceCallSite(serviceType, new HashSet<Type>());
            if (null != serviceCallSite)
            {
                var providerExpression = Expression.Parameter(typeof(ServiceProvider), "provider");
                this.RealizedServices[serviceType] = Expression.Lambda<Func<ServiceProvider, object>>(serviceCallSite.Build(providerExpression), providerExpression).Compile();
                return serviceCallSite.Invoke(this);
            }

            this.RealizedServices[serviceType] = _ => null;
            return null;
        }

        public IServiceCallSite GetServiceCallSite(Type serviceType, ISet<Type> callSiteChain)
        {
            try
            {
                if (callSiteChain.Contains(serviceType))
                {
                    throw new InvalidOperationException(string.Format("A circular dependency was detected for the service of type '{0}'", serviceType.FullName));
                }
                callSiteChain.Add(serviceType);

                ServiceEntry serviceEntry;
                if (this.ServiceTable.ServieEntries.TryGetValue(serviceType,
                    out serviceEntry))
                {
                    return serviceEntry.Last.CreateCallSite(this, callSiteChain);
                }

                if (serviceType.IsConstructedGenericType && serviceType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                                {
                                    Type elementType = serviceType.GenericTypeArguments[0];
                                    IServiceCallSite[] serviceCallSites = this.ServiceTable.ServieEntries.TryGetValue(elementType, out serviceEntry)
                                        ? serviceEntry.All.Select(it => it.CreateCallSite(this, callSiteChain)).ToArray()
                                        : new IServiceCallSite[0];
                                    return new EnumerableCallSite(elementType, serviceCallSites);
                                }

                return null;
            }
            finally
            {
                callSiteChain.Remove(serviceType);
            }
        }

        public void Dispose()
        {
            this.TransientDisposableServices.Foreach(t => t.Dispose());
            this.ResolvedServices.Values.Foreach(r => (r as IDisposable)?.Dispose());
            //Array.ForEach(this.TransientDisposableServices.ToArray(), _ => _.Dispose());
            //Array.ForEach(this.ResolvedServices.Values.ToArray(), _ => (_ as IDisposable)?.Dispose());
            this.TransientDisposableServices.Clear();
            this.ResolvedServices.Clear();
        }
        public object CaptureDisposable(object instance)
        {
            IDisposable disposable = instance as IDisposable;
            if (null != disposable)
            {
                this.TransientDisposableServices.Add(disposable);
            }
            return instance;
        }
    }

   
}
