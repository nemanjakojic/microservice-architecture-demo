using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Microservice.Core
{
    public class ValidationResult
    {
        private readonly List<string> _messages;

        public ValidationResult()
        {
            _messages = new List<string>();
        }

        public bool Valid { get; private set; }

        public IEnumerable<string> Messages { get => _messages; }

        public static ValidationResult Success()
        {
            return new ValidationResult { Valid = true };
        }

        public static ValidationResult Failure()
        {
            return new ValidationResult { Valid = false };
        }

        public ValidationResult WithMessages(IEnumerable<string> messages)
        {
            _messages.AddRange(messages);
            return this;
        }

        public ValidationResult WithMessage(string message)
        {
            _messages.Add(message);
            return this;
        }

        public Task<ValidationResult> ToTask()
        {
            return Task.FromResult(this);
        }
    }
}
