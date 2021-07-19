using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Demo.Microservice.Core.Extensions;

namespace Demo.Microservice.Core
{
    /// <summary>
    /// A base class for all application core operations.
    /// </summary>
    /// <typeparam name="RequestType">Operation input</typeparam>
    /// <typeparam name="ResponseType">Operation output</typeparam>
    public abstract class CoreOperationBase<RequestType, ResponseType> : ICoreOperation<RequestType, ResponseType>
        where RequestType: ServiceRequest
        where ResponseType: ServiceResponse, new()
    {
        protected readonly ILogger<CoreOperationBase<RequestType, ResponseType>> _logger;

        protected CoreOperationBase(ILogger<CoreOperationBase<RequestType, ResponseType>> logger)
        {
            _logger = logger;
        }

        protected virtual Task<ValidationResult> ValidateRequest(RequestType request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return ValidationResult.Failure().WithMessage("Request is null.").ToTask();
            }
            return ValidationResult.Success().ToTask();
        }

        /// <summary>
        /// Application service body.
        /// </summary>
        /// <param name="request">A service request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>A service response</returns>
        protected abstract Task<ResponseType> ExecuteRequest(RequestType request, CancellationToken cancellationToken);

        public Task<ResponseType> Execute(RequestType request)
        {
            return Execute(request, CancellationToken.None);
        }

        public async Task<ResponseType> Execute(RequestType request, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = await ValidateRequest(request, cancellationToken);
                if (!validationResult.Valid)
                {
                    _logger.LogWarning($"Refused invalid request { typeof(RequestType).Name }: { JsonConvert.SerializeObject(request) }.");
                    return new ResponseType().Failure().WithMessages(validationResult.Messages);
                }

                return await ExecuteRequest(request, cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Failed to execute { typeof(RequestType).Name }: { JsonConvert.SerializeObject(request) }.");
                return new ResponseType().Failure().WithMessages($"Failed to execute request { typeof(RequestType).Name }: { e.Message }.");
            }
        }
    }
}
