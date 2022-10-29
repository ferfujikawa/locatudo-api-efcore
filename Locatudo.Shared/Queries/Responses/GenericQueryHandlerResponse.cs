using Flunt.Notifications;

namespace Locatudo.Shared.Queries.Responses
{
    public class GenericQueryHandlerResponse<T> : IQueryResponse<T> where T : IQueryData
    {
        public bool Success { get; private set; }
        public T? Data { get; private set; }
        public IReadOnlyCollection<string> Messages => _messages?.Select(x => x.Message).ToList() ?? new List<string>();

        private readonly IEnumerable<Notification>? _messages;

        public GenericQueryHandlerResponse(bool success, T? data, IEnumerable<Notification>? messages)
        {
            Success = success;
            Data = data;
            _messages = messages;
        }

        public GenericQueryHandlerResponse(bool success, T? data, string? messageKey, string? message)
        {
            Success = success;
            Data = data;
            _messages = new List<Notification>() { new Notification(messageKey, message) };
        }
    }
}
