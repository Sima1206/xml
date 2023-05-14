using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightService.Core
{
    interface IUnitOfWork : IDisposable
    {
        void Dispose();
    }
}
