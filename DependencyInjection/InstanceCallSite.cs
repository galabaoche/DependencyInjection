using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DependencyInjection
{
    internal class InstanceCallSite : IServiceCallSite
    {
        public object Instance { get; private set; }

        public InstanceCallSite(object instance)
        {
            this.Instance = instance;
        }
        public Expression Build(Expression provider)
        {
            return Expression.Constant(this.Instance);
        }
        public object Invoke(ServiceProvider provider)
        {
            return Instance;
        }
    }
}
