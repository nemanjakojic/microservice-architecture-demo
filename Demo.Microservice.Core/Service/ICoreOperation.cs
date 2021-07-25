using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.Microservice.Core
{
    /// <summary>
    /// Represents an asynchronous operation that can be executed.
    /// The operation receives a request (input) and produces a response (output).
    /// </summary>
    /// <typeparam name="TRequest">A request type: inherits from ServiceRequest</typeparam>
    /// <typeparam name="TResponse">A response type: inherits from ServiceResponse</typeparam>
    public interface ICoreOperation<TRequest, TResponse>
        where TRequest : ServiceRequest
        where TResponse : ServiceResponse
    {
        /// <summary>
        /// Executes the operation.
        /// </summary>
        /// <param name="request">An intance of the type TRequest</param>
        /// <returns>An instance of the type TResponse</returns>
        Task<TResponse> Execute(TRequest request);
    }
}
