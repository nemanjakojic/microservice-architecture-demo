using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Microservice.Core
{
    public enum ValidationType
    {
        Information = 0,
        Warning = 1,
        Error = 2,
        Critical = 3
    }

    public class ValidationMessage
    {
        public ValidationType Type { get; set; }
        public string Message { get; set; }
    }


    public class ValidationResult
    {
        private readonly List<ValidationMessage> _messages;

        internal ValidationResult()
        {
            _messages = new List<ValidationMessage>();
        }

        public bool Passed { get; private set; }

        public IEnumerable<ValidationMessage> Messages { get => _messages; }

        public static ValidationResult Success()
        {
            return new ValidationResult { Passed = true };
        }

        public static ValidationResult Failure()
        {
            return new ValidationResult { Passed = false };
        }

        public ValidationResult WithMessages(IEnumerable<ValidationMessage> messages)
        {
            _messages.AddRange(messages);
            return this;
        }

        public ValidationResult WithInfo(string message)
        {
            return AddValidationMessage(message, ValidationType.Information);
        }

        public ValidationResult WithError(string message)
        {
            return AddValidationMessage(message, ValidationType.Error);
        }

        public ValidationResult WithCriticalError(string message)
        {
            return AddValidationMessage(message, ValidationType.Critical);
        }

        public ValidationResult WithWarning(string message)
        {
            return AddValidationMessage(message, ValidationType.Warning);
        }

        private ValidationResult AddValidationMessage(string message, ValidationType validationType)
        {
            _messages.Add(new ValidationMessage { Message = message, Type = validationType });
            return this;
        }

        public Task<ValidationResult> ToTask()
        {
            return Task.FromResult(this);
        }

        public static ValidationResult And(params ValidationResult[] validationResults)
        {
            bool passed = validationResults.All(v => v.Passed);
            var messages = validationResults.SelectMany(v => v.Messages);
            return new ValidationResult { Passed = passed }.WithMessages(messages);
        }

        public static ValidationResult Or(params ValidationResult[] validationResults)
        {
            bool passed = validationResults.Any(v => v.Passed);
            var messages = validationResults.SelectMany(v => v.Messages);
            return new ValidationResult { Passed = passed }.WithMessages(messages);
        }
    }
}
