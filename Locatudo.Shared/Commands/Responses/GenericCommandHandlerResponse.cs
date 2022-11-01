using FluentValidation.Results;
using Flunt.Notifications;

namespace Locatudo.Shared.Commands.Responses
{
    public class GenericCommandHandlerResponse<T> : ICommandResponse<T> where T : ICommandData
    {
        public bool Success { get; private set; }
        public T? Data { get; private set; }
        public IReadOnlyCollection<string> Messages => _messages?.Select(x => x.Message).ToList() ?? new List<string>();

        private readonly IEnumerable<Notification>? _messages;

        public GenericCommandHandlerResponse(bool success, T? data, IEnumerable<Notification>? messages)
        {
            Success = success;
            Data = data;
            _messages = messages;
        }

        public GenericCommandHandlerResponse(IEnumerable<ValidationFailure> validationFailures)
        {
            Success = false;
            _messages = validationFailures.Select(x => new Notification(x.PropertyName, x.ErrorMessage));
        }

        public GenericCommandHandlerResponse(string? errorKey, string? errorMessage)
        {
            Success = false;
            _messages = new List<Notification>() { new Notification(errorKey, errorMessage) };
        }

        public GenericCommandHandlerResponse(T? data, string? messageKey, string? message)
        {
            Success = true;
            Data = data;
            _messages = new List<Notification>() { new Notification(messageKey, message) };
        }
    }
}
