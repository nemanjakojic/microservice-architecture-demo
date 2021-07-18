using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Microservice.Core
{
    public interface ICoreOperationProvider
    {
        TCoreOperation GetCoreOperation<TCoreOperation>();
    }
}
