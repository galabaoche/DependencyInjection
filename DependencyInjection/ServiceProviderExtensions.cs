using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjection
{
    public static class ServiceProviderExtensions
    {

        public static T GetService<T>(this IServiceProvider provider) where T:class
        {
            return provider.GetService(typeof(T)) as T;
        }
        //public static object GetRequiredService(this IServiceProvider provider, Type serviceType)

        //public static T GetRequiredService<T>(this IServiceProvider provider);
    }
}
