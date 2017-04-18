using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DependencyInjection
{
   internal class EnumerableCallSite : IServiceCallSite
   {
       public Type ElementType { get; private set; }
       public IServiceCallSite[] ServiceCallSites { get; private set; }
    
       public EnumerableCallSite(Type elementType, IServiceCallSite[] serviceCallSites)
       {
           this.ElementType = elementType;
           this.ServiceCallSites = serviceCallSites;
       }
    
       public Expression Build(Expression provider)
       {
           return Expression.NewArrayInit(this.ElementType, this.ServiceCallSites.Select(
               it => Expression.Convert(it.Build(provider), this.ElementType)));
       }
    
       public object Invoke(ServiceProvider provider)
       {
           var array = Array.CreateInstance(this.ElementType, this.ServiceCallSites.Length);
           for (var index = 0; index< this.ServiceCallSites.Length; index++)
           {
               array.SetValue(this.ServiceCallSites[index].Invoke(provider), index);
           }
           return array;
       }
   }
}
