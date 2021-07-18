using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Microservice.Core.Extensions
{
    public static class ServiceResponseExtentions
    {
        public static ResponseType Success<ResponseType>(this ResponseType response)
            where ResponseType: ServiceResponse
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

        public static ResponseType WithMessages<ResponseType>(this ResponseType response, IEnumerable<string> messages)
            where ResponseType : ServiceResponse
        {
            response.AddMessages(messages);
            return response;
        }

        public static ResponseType WithMessages<ResponseType>(this ResponseType response, params string[] messages)
            where ResponseType : ServiceResponse
        {
            response.AddMessages(messages);
            return response;
        }
    }
}
