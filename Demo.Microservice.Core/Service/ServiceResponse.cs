using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Microservice.Core
{
    public class ServiceResponse
    {
        private readonly List<string> _messages;

        public ServiceResponse()
        {
            _messages = new List<string>();
        }

        public IEnumerable<string> Messages { get => _messages; }
        public bool Completed { get; internal set; }

        internal void AddMessage(string message)
        {
            _messages.Add(message);
        }

        internal void AddMessages(IEnumerable<string> msgList)
        {
            _messages.AddRange(msgList);
        }
    }
}
