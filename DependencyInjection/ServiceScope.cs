using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjection
{
    internal class ServiceScope : IServiceScope
    {
        private readonly ServiceProvider _scopedProvider;
        public ServiceScope(ServiceProvider scopedProvider)
        {
            this._scopedProvider = scopedProvider;
        }

        public void Dispose()
        {
            (this.ServiceProvider as IDisposable)?.Dispose();
        }

        public IServiceProvider ServiceProvider
        {
            get { return _scopedProvider; }
        }
    }
}
