using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjection
{
    internal interface IService
    {
        IService Next { get; set; }
        ServiceLifetime Lifetime { get; }
        IServiceCallSite CreateCallSite(ServiceProvider provider, ISet<Type> callSiteChain);
    }
   
}
