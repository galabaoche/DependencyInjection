using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Linq;

namespace DependencyInjection
{
   internal class ConstructorCallSite : IServiceCallSite
  {
      public ConstructorInfo ConstructorInfo { get; private set; }
      public IServiceCallSite[] Parameters { get; private set; }
   
      public ConstructorCallSite(ConstructorInfo constructorInfo, IServiceCallSite[] parameters)
      {
          this.ConstructorInfo = constructorInfo;
          this.Parameters = parameters;
      }
   
      public Expression Build(Expression provider)
      {
          ParameterInfo[] parameters = this.ConstructorInfo.GetParameters();
          return Expression.New(this.ConstructorInfo, this.Parameters.Select((p, index) => Expression.Convert(p.Build(provider), 
              parameters [index].ParameterType)).ToArray());
      }
   
      public object Invoke(ServiceProvider provider)
      {
          return this.ConstructorInfo.Invoke(this.Parameters.Select(p => p.Invoke(provider)).ToArray());
      }
  }
}
