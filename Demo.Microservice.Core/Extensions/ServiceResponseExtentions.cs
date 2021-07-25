using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Microservice.Core.Extensions
{
    public static class ServiceResponseExtensions
    {
        public static ResponseType Success<ResponseType>(this ResponseType response)
            where ResponseType : ServiceResponse
        {
            response.Completed = true;
            return response;
        }

        public static ResponseType Failure<ResponseType>(this ResponseType response)
            where ResponseType : ServiceResponse
        {
            response.Completed = false;
            return response;
        }

        public static ResponseType WithMessages<ResponseType>(this ResponseType response, IEnumerable<ValidationMessage> messages)
            where ResponseType : ServiceResponse
        {
            response.AddMessages(messages);
            return response;
        }

        public static ResponseType WithValidation<ResponseType>(this ResponseType response, ValidationResult validation)
            where ResponseType : ServiceResponse
        {
            response.AddMessages(validation.Messages);
            return response;
        }
    }
}
