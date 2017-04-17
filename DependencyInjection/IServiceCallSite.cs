using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace DependencyInjection
{
    internal interface IServiceCallSite
    {
        object Invoke(ServiceProvider provider);
        Expression Build(Expression provider);
    }
}
