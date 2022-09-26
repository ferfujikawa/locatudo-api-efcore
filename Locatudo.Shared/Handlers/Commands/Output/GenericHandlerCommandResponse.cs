using Flunt.Notifications;

namespace Locatudo.Shared.Handlers.Commands.Output
{
    public class GenericHandlerCommandResponse<T> : IHandlerCommandResponse<T> where T : IHandlerCommandData
    {
        public bool Success { get; private set; }
        public T? Data { get; private set; }
        public IReadOnlyCollection<string> Messages => _messages?.Select(x => x.Message).ToList() ?? new List<string>();

        private readonly IEnumerable<Notification>? _messages;

        public GenericHandlerCommandResponse(bool success, T? data, IEnumerable<Notification>? messages)
        {
            Success = success;
            Data = data;
            _messages = messages;
        }

        public GenericHandlerCommandResponse(bool success, T? data, string? messageKey, string? messages)
        {
            Success = success;
            Data = data;
            _messages = new List<Notification>() { new Notification(messageKey, messages) };
        }
    }
}
