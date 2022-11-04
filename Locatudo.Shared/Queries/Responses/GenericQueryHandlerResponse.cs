using FluentValidation.Results;

namespace Locatudo.Shared.Queries.Responses
{
    public class GenericQueryHandlerResponse<T> : IQueryResponse<T> where T : IQueryData
    {
        public bool Success { get; private set; }
        public T? Data { get; private set; }
        public IReadOnlyCollection<string> Messages => _messages;

        private readonly IReadOnlyCollection<string> _messages;

        public GenericQueryHandlerResponse(IReadOnlyCollection<ValidationFailure> messages)
        {
            Success = false;
            _messages = messages.Select(x => x.ErrorMessage).ToList();
        }

        public GenericQueryHandlerResponse(string message)
        {
            Success = false;
            _messages = new List<string>() { message };
        }

        public GenericQueryHandlerResponse(T data, string message)
        {
            Success = true;
            Data = data;
            _messages = new List<string>() { message };
        }
    }
}
