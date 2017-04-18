using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjection
{
    internal class ServiceTable
    {
        public IDictionary<Type, ServiceEntry> ServieEntries { get; private set; } = new Dictionary<Type, ServiceEntry>();

        public ServiceTable(IServiceCollection services)
        {
            this.ServieEntries[typeof(IServiceProvider)] = new ServiceEntry(new ServiceProviderService());
            this.ServieEntries[typeof(IServiceScopeFactory)] = new ServiceEntry(new ServiceScopeFactoryService());
            foreach (var group in services.GroupBy(it => it.ServiceType))
            {
                ServiceDescriptor[] descriptors = group.ToArray();
                ServiceEntry entry = new ServiceEntry(new Service(descriptors[0]));
                for (int index = 1; index < descriptors.Length; index++)
                {
                    entry.Add(new Service(descriptors[index]));
                }
                this.ServieEntries[group.Key] = entry;
            }
            //省略其他代码
        }
    }
}
