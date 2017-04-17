using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjection
{
    public enum ServiceLifetime
    {

        Singleton,

        Scoped,

        Transient
    }
}
