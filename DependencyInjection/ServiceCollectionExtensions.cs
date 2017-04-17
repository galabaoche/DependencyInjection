using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceProvider BuildServiceProvider(this IServiceCollection services)
        {
            return new ServiceProvider(services);
        }

        public static IServiceCollection AddSingleton<TService, TImplementation>(this IServiceCollection services)
        where TService : class
        where TImplementation : class, TService
        {
            return services.AddSingleton(typeof(TService), typeof(TImplementation));
        }

        public static IServiceCollection AddSingleton(this IServiceCollection services, Type serviceType, Type implementationType)
        {
            return Add(services, serviceType, implementationType, ServiceLifetime.Singleton);
        }
        public static IServiceCollection AddScoped<TService, TImplementation>(this IServiceCollection services)
          where TService : class
          where TImplementation : class, TService
        {
            return services.AddScoped(typeof(TService), typeof(TImplementation));
        }

        public static IServiceCollection AddScoped(this IServiceCollection services, Type serviceType, Type implementationType)
        {
            return Add(services, serviceType, implementationType, ServiceLifetime.Scoped);
        }

        public static IServiceCollection AddTransient<TService, TImplementation>(this IServiceCollection services)
           where TService : class
           where TImplementation : class, TService
        {
            return services.AddTransient(typeof(TService), typeof(TImplementation));
        }

        public static IServiceCollection AddTransient( this IServiceCollection services, Type serviceType,Type implementationType)
        {
            return Add(services, serviceType, implementationType, ServiceLifetime.Transient);
        }


        private static IServiceCollection Add(IServiceCollection collection,Type serviceType,Type implementationType,ServiceLifetime lifetime)
        {
            var descriptor = new ServiceDescriptor(serviceType, implementationType, lifetime);
            collection.Add(descriptor);
            return collection;
        }
    }
}
