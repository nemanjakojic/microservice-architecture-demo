using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Microservice.Core
{
    public class ServiceResponse
    {
        private readonly List<ValidationMessage> _messages;

        public ServiceResponse()
        {
            _messages = new List<ValidationMessage>();
        }

        public IEnumerable<ValidationMessage> Messages { get => _messages; }
        public bool Completed { get; internal set; }

        internal void AddMessage(ValidationMessage message)
        {
            _messages.Add(message);
        }

        internal void AddMessages(IEnumerable<ValidationMessage> messages)
        {
            _messages.AddRange(messages);
        }
    }
}
