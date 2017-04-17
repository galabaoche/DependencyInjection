using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjection
{
   internal class ServiceScopeFactory : IServiceScopeFactory
   {
       private readonly ServiceProvider _provider;
       public ServiceScopeFactory(ServiceProvider provider)
       {
           _provider = provider;
       }
    
       public IServiceScope CreateScope()
       {
           return new ServiceScope(new ServiceProvider(_provider));
       }
   }
}
