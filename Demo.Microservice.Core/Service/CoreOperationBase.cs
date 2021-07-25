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
    /// An abstract base class for all application core operations.
    /// Standardizes the request processing flow. 
    /// Defines execution phases of the flow that can be redefined by its subclasses.
    /// </summary>
    /// <typeparam name="RequestType">A request type: subclass of ServiceRequest</typeparam>
    /// <typeparam name="ResponseType">A response type: subclass of ServiceResponse</typeparam>
    public abstract class CoreOperationBase<RequestType, ResponseType> : ICoreOperation<RequestType, ResponseType>
        where RequestType : ServiceRequest
        where ResponseType : ServiceResponse, new()
    {
        protected readonly ILogger<CoreOperationBase<RequestType, ResponseType>> _logger;

        protected CoreOperationBase(ILogger<CoreOperationBase<RequestType, ResponseType>> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Performs default request validation. 
        /// This method can be overriden by subclasses.
        /// </summary>
        /// <param name="request">A request to validate</param>
        /// <returns></returns>
        protected virtual Task<ValidationResult> ValidateRequest(RequestType request)
        {
            return ValidationResult.Success().ToTask();
        }

        /// <summary>
        /// Abstract operation body aimed for implementation in subclasses.
        /// </summary>
        /// <param name="request">A service request to execute</param>
        /// <param name="validation">A result of the request validation made available for the execution phase</param>
        /// <returns>A service response</returns>
        protected abstract Task<ResponseType> ExecuteRequest(RequestType request, ValidationResult validation);

        /// <summary>
        /// Defines a mandatory request processing flow that all concrete operations are supposed to follow (Template Method DP). 
        /// This method cannot be overriden by subclasses.
        /// </summary>
        /// <param name="request">A request to validate and execute.</param>
        /// <returns></returns>
        public async Task<ResponseType> Execute(RequestType request)
        {
            try
            {
                if (request == null)
                {
                    return HandleError($"{ typeof(RequestType).Name } was null.", request);
                }

                var requestValidation = await ValidateRequest(request);
                if (!requestValidation.Passed)
                {
                    return HandleError($"{ typeof(RequestType).Name } failed validation.", request);
                }

                return await ExecuteRequest(request, requestValidation);
            }
            catch (Exception e)
            {
                return HandleError($"Failed to execute { typeof(RequestType).Name } due to an internal server error.", request, e);
            }
        }

        /// <summary>
        /// A private reusable error handler.
        /// Facilitates error handling and keeps it concise.
        /// </summary>
        /// <param name="errorMessage">An error message to log and return to the consumer</param>
        /// <param name="request">The active request to record in the server logs</param>
        /// <param name="e">If available, the exception is logged too.</param>
        /// <returns>A response object with error messages</returns>
        private ResponseType HandleError(string errorMessage, ServiceRequest request, Exception e = null)
        {
            var loggingMessage = $"{errorMessage} Request: { JsonConvert.SerializeObject(request) }.";
            if (e != null)
            {
                _logger.LogError(e, loggingMessage);
            }
            else
            {
                _logger.LogError(loggingMessage);
            }
            
            var validation = ValidationResult.Failure().WithError(errorMessage);
            return new ResponseType().Failure().WithValidation(validation);
        }
    }
}
