using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjection
{
   internal class ServiceEntry
   {
       public IService First { get; private set; }
       public IService Last { get; private set; }
       public IList<IService> All { get; private set; } = new List<IService>();
    
       public ServiceEntry(IService service)
       {
           this.First = service;
           this.Last = service;
           this.All.Add(service);
       }
    
       public void Add(IService service)
       {
           this.Last.Next = service;
           this.Last = service;
           this.Add(service);
       }
   }
}
