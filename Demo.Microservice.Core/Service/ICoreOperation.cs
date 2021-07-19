using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.Microservice.Core
{
    public interface ICoreOperation<RequestType, ResponseType>
        where RequestType: ServiceRequest
        where ResponseType: ServiceResponse
    {
        Task<ResponseType> Execute(RequestType request, CancellationToken cancellationToken);
        Task<ResponseType> Execute(RequestType request);
    }
}
