using System;


namespace DependencyInjection
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IServiceProvider serviceProvider = new ServiceCollection()
             .AddTransient<IFoobar, Foobar>()
             .BuildServiceProvider();

            IFoobar foobar = serviceProvider.GetService<IFoobar>();
            foobar.Dispose();
            GC.Collect();

            Console.WriteLine("----------------");
            using (IServiceScope serviceScope = serviceProvider.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<IFoobar>();
            }
            GC.Collect();

            Console.Read();
        }
    }

    public interface IFoobar : IDisposable
    { }

    public class Foobar : IFoobar
    {
        ~Foobar()
        {
            Console.WriteLine("Foobar.Finalize()");
        }

        public void Dispose()
        {
            Console.WriteLine("Foobar.Dispose()");
        }
    }
}
