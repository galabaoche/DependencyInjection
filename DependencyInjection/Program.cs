using System;
using System.Diagnostics;
using System.Reflection;

namespace DependencyInjection
{
    public class Program
    {
        public static void Main(string[] args)
        {
            #region instants
            IServiceProvider serviceProvider = new ServiceCollection()
                .AddSingleton<IFoo, Foo>()
                .AddSingleton<IBar>(new Bar())
                .AddSingleton<IBaz>(_ => new Baz())
                .AddSingleton<IGux, Gux>()
                .BuildServiceProvider();

            Console.WriteLine("serviceProvider.GetService<IFoo>(): {0}", serviceProvider.GetService<IFoo>());
            Console.WriteLine("serviceProvider.GetService<IBar>(): {0}", serviceProvider.GetService<IBar>());
            Console.WriteLine("serviceProvider.GetService<IBaz>(): {0}", serviceProvider.GetService<IBaz>());
            Console.WriteLine("serviceProvider.GetService<IGux>(): {0}", serviceProvider.GetService<IGux>());

            #endregion

            #region collection
            //IServiceCollection serviceCollection = new ServiceCollection()
            //       .AddSingleton<IFoobar, Foobar1>()
            //       .AddSingleton<IFoobar, Foobar2>();

            //IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            //Console.WriteLine("serviceProvider.GetService<IFoobar>(): {0}", serviceProvider.GetService<IFoobar>());

            //IEnumerable<IFoobar> services = serviceProvider.GetServices<IFoobar>();
            //int index = 1;
            //Console.WriteLine("serviceProvider.GetServices<IFoobar>():"); 

            //foreach (IFoobar foobar in services)
            //{
            //    Console.WriteLine("{0}: {1}", index++, foobar);
            //}
            #endregion

            #region IServiceProvider self
            //IServiceProvider serviceProvider = new ServiceCollection().BuildServiceProvider();
            //Debug.Assert(object.ReferenceEquals(serviceProvider, serviceProvider.GetService<IServiceProvider>()));
            //Debug.Assert(object.ReferenceEquals(serviceProvider, serviceProvider.GetServices<IServiceProvider>().Single())); 
            #endregion

            #region Generic

            //IServiceProvider serviceProvider = new ServiceCollection()
            //    .AddScoped<IFoo, Foo>()
            //    .AddScoped<IBar, Bar>()
            //    .AddScoped<IBaz, Baz>()
            //    .AddScoped<IGux, Gux>()
            //    .AddScoped(typeof(IBazGux<,>), typeof(BazGux<,>))
            //    .BuildServiceProvider();

            //Console.WriteLine($"serviceProvider.GetService<IBazGux<IBaz, IGux>>().BazT={ serviceProvider.GetService<IBazGux<IBaz, IGux>>().BazT}");
            //Console.WriteLine($"serviceProvider.GetService<IBazGux<IBaz, IGux>>().GuxT={ serviceProvider.GetService<IBazGux<IBaz, IGux>>().GuxT}");

            #endregion

            #region constructor choose1
            //new ServiceCollection()
            //    .AddTransient<IFoo, Foo>()
            //    .AddTransient<IBar, Bar>()
            //    .AddTransient<IGux, Gux>()
            //    .BuildServiceProvider()
            //    .GetService<IGux>(); 
            #endregion

            #region constructor choose2
            //new ServiceCollection()
            //      .AddTransient<IFoo, Foo>()
            //      .AddTransient<IBar, Bar>()
            //      .AddTransient<IBaz, Baz>()
            //      .AddTransient<IGux, Gux>()
            //      .BuildServiceProvider()
            //      .GetService<IGux>(); 
            #endregion

            #region parent sun
            //IServiceProvider serviceProvider1 = new ServiceCollection().BuildServiceProvider();
            //IServiceProvider serviceProvider2 = serviceProvider1.GetService<IServiceScopeFactory>().CreateScope().ServiceProvider;

            //object root = serviceProvider2.GetType().GetField("_root", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(serviceProvider2);
            //Debug.Assert(object.ReferenceEquals(serviceProvider1, root)); 
            #endregion

            #region Life Cycle
            //IServiceProvider root = new ServiceCollection()
            //  .AddTransient<IFoo, Foo>()
            //  .AddScoped<IBar, Bar>()
            //  .AddSingleton<IBaz, Baz>()
            //  .BuildServiceProvider();
            //IServiceProvider child1 = root.GetService<IServiceScopeFactory>().CreateScope().ServiceProvider;
            //IServiceProvider child2 = root.GetService<IServiceScopeFactory>().CreateScope().ServiceProvider;

            //Console.WriteLine("ReferenceEquals(root.GetService<IFoo>(), root.GetService<IFoo>() = {0}", ReferenceEquals(root.GetService<IFoo>(), root.GetService<IFoo>()));
            //Console.WriteLine("ReferenceEquals(child1.GetService<IBar>(), child1.GetService<IBar>() = {0}", ReferenceEquals(child1.GetService<IBar>(), child1.GetService<IBar>()));
            //Console.WriteLine("ReferenceEquals(child1.GetService<IBar>(), child2.GetService<IBar>() = {0}", ReferenceEquals(child1.GetService<IBar>(), child2.GetService<IBar>()));
            //Console.WriteLine("ReferenceEquals(child1.GetService<IBaz>(), child2.GetService<IBaz>() = {0}", ReferenceEquals(child1.GetService<IBaz>(), child2.GetService<IBaz>())); 
            #endregion

            #region life cycle2
            //IServiceProvider root = new ServiceCollection()
            //  .AddTransient<IFooDispose, FooDispose>()
            //  .AddScoped<IBarDispose, BarDispose>()
            //  .AddSingleton<IBazDispose, BazDispose>()
            //  .BuildServiceProvider();
            //IServiceProvider child1 = root.GetService<IServiceScopeFactory>().CreateScope().ServiceProvider;
            //IServiceProvider child2 = root.GetService<IServiceScopeFactory>().CreateScope().ServiceProvider;

            //child1.GetService<IFooDispose>();
            //child1.GetService<IFooDispose>();
            //child2.GetService<IBarDispose>();
            //child2.GetService<IBazDispose>();

            //Console.WriteLine("child1.Dispose()");
            //((IDisposable)child1).Dispose();

            //Console.WriteLine("child2.Dispose()");
            //((IDisposable)child2).Dispose();

            //Console.WriteLine("root.Dispose()");
            //((IDisposable)root).Dispose(); 
            #endregion

            #region life cycle3
            //IServiceProvider serviceProvider = new ServiceCollection()
            //          .AddTransient<IFoobarbaz, Foobarbaz>()
            //          .BuildServiceProvider();

            //IFoobarbaz foobar = serviceProvider.GetService<IFoobarbaz>();
            //foobar.Dispose();
            //GC.Collect();

            //Console.WriteLine("----------------");
            //using (IServiceScope serviceScope = serviceProvider.GetService<IServiceScopeFactory>().CreateScope())
            //{
            //    serviceScope.ServiceProvider.GetService<IFoobarbaz>();
            //}
            //GC.Collect(); 
            #endregion

            Console.Read();
        }
    }


}
