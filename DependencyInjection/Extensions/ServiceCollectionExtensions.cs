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

        public static IServiceCollection AddTransient(
            this IServiceCollection services,
            Type serviceType,
            Type implementationType)
        {
            return Add(services, serviceType, implementationType, ServiceLifetime.Transient);
        }
        public static IServiceCollection AddTransient(
            this IServiceCollection services,
            Type serviceType,
            Func<IServiceProvider, object> implementationFactory)
        {
            return Add(services, serviceType, implementationFactory, ServiceLifetime.Transient);
        }

        public static IServiceCollection AddTransient<TService, TImplementation>(this IServiceCollection services)
            where TService : class
            where TImplementation : class, TService
        {
            return services.AddTransient(typeof(TService), typeof(TImplementation));
        }

        public static IServiceCollection AddTransient(
            this IServiceCollection services,
            Type serviceType)
        {
            return services.AddTransient(serviceType, serviceType);
        }

        public static IServiceCollection AddTransient<TService>(this IServiceCollection services)
            where TService : class
        {
            return services.AddTransient(typeof(TService));
        }

        public static IServiceCollection AddTransient<TService>(
            this IServiceCollection services,
            Func<IServiceProvider, TService> implementationFactory)
            where TService : class
        {
            return services.AddTransient(typeof(TService), implementationFactory);
        }

        public static IServiceCollection AddTransient<TService, TImplementation>(
            this IServiceCollection services,
            Func<IServiceProvider, TImplementation> implementationFactory)
            where TService : class
            where TImplementation : class, TService
        {
            return services.AddTransient(typeof(TService), implementationFactory);
        }


        public static IServiceCollection AddScoped(
            this IServiceCollection services,
            Type serviceType,
            Type implementationType)
        {
            return Add(services, serviceType, implementationType, ServiceLifetime.Scoped);
        }
        public static IServiceCollection AddScoped(
            this IServiceCollection services,
            Type serviceType,
            Func<IServiceProvider, object> implementationFactory)
        {
      
            return Add(services, serviceType, implementationFactory, ServiceLifetime.Scoped);
        }

        public static IServiceCollection AddScoped<TService, TImplementation>(this IServiceCollection services)
            where TService : class
            where TImplementation : class, TService
        {
            return services.AddScoped(typeof(TService), typeof(TImplementation));
        }

        public static IServiceCollection AddScoped(
            this IServiceCollection services,
            Type serviceType)
        { 

            return services.AddScoped(serviceType, serviceType);
        }
        public static IServiceCollection AddScoped<TService>(this IServiceCollection services)
            where TService : class
        {
            return services.AddScoped(typeof(TService));
        }

        public static IServiceCollection AddScoped<TService>(
            this IServiceCollection services,
            Func<IServiceProvider, TService> implementationFactory)
            where TService : class
        {
            return services.AddScoped(typeof(TService), implementationFactory);
        }

        public static IServiceCollection AddScoped<TService, TImplementation>(
            this IServiceCollection services,
            Func<IServiceProvider, TImplementation> implementationFactory)
            where TService : class
            where TImplementation : class, TService
        {
            return services.AddScoped(typeof(TService), implementationFactory);
        }

     
        public static IServiceCollection AddSingleton(
            this IServiceCollection services,
            Type serviceType,
            Type implementationType)
        {

            return Add(services, serviceType, implementationType, ServiceLifetime.Singleton);
        }

        public static IServiceCollection AddSingleton(
            this IServiceCollection services,
            Type serviceType,
            Func<IServiceProvider, object> implementationFactory)
        {

            return Add(services, serviceType, implementationFactory, ServiceLifetime.Singleton);
        }

        public static IServiceCollection AddSingleton<TService, TImplementation>(this IServiceCollection services)
            where TService : class
            where TImplementation : class, TService
        {         
            return services.AddSingleton(typeof(TService), typeof(TImplementation));
        }

        public static IServiceCollection AddSingleton(
            this IServiceCollection services,
            Type serviceType)
        {
            return services.AddSingleton(serviceType, serviceType);
        }

        public static IServiceCollection AddSingleton<TService>(this IServiceCollection services)
            where TService : class
        {

            return services.AddSingleton(typeof(TService));
        }

        public static IServiceCollection AddSingleton<TService>(
            this IServiceCollection services,
            Func<IServiceProvider, TService> implementationFactory)
            where TService : class
        {
            return services.AddSingleton(typeof(TService), implementationFactory);
        }

        public static IServiceCollection AddSingleton<TService, TImplementation>(
            this IServiceCollection services,
            Func<IServiceProvider, TImplementation> implementationFactory)
            where TService : class
            where TImplementation : class, TService
        {
            return services.AddSingleton(typeof(TService), implementationFactory);
        }

        public static IServiceCollection AddSingleton(
            this IServiceCollection services,
            Type serviceType,
            object implementationInstance)
        {
            var serviceDescriptor = new ServiceDescriptor(serviceType, implementationInstance);
            services.Add(serviceDescriptor);
            return services;
        }

        public static IServiceCollection AddSingleton<TService>(
            this IServiceCollection services,
            TService implementationInstance)
            where TService : class
        {
            return services.AddSingleton(typeof(TService), implementationInstance);
        }

        private static IServiceCollection Add(
            IServiceCollection collection,
            Type serviceType,
            Type implementationType,
            ServiceLifetime lifetime)
        {
            var descriptor = new ServiceDescriptor(serviceType, implementationType, lifetime);
            collection.Add(descriptor);
            return collection;
        }

        private static IServiceCollection Add(
            IServiceCollection collection,
            Type serviceType,
            Func<IServiceProvider, object> implementationFactory,
            ServiceLifetime lifetime)
        {
            var descriptor = new ServiceDescriptor(serviceType, implementationFactory, lifetime);
            collection.Add(descriptor);
            return collection;
        }
    }
}
