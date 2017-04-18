using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjection
{
    public static class CollectionExtend
    {
        public static void Foreach<T>(this IEnumerable<T> sources, Action<T> action)
        {
            foreach (var e in sources)
            {
                action(e);
            }
        }
    }
}
