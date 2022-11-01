using FluentValidation.Results;

namespace Locatudo.Shared.Commands.Responses
{
    public class GenericCommandHandlerResponse<T> : ICommandResponse<T> where T : ICommandData
    {
        public bool Success { get; private set; }
        public T? Data { get; private set; }
        public IReadOnlyCollection<string> Messages => _messages;

        private readonly IReadOnlyCollection<string> _messages = new List<string>();

        public GenericCommandHandlerResponse(IEnumerable<ValidationFailure> validationFailures)
        {
            Success = false;
            _messages = validationFailures.Select(x => x.ErrorMessage).ToList() ;
        }

        public GenericCommandHandlerResponse(string? errorMessage)
        {
            Success = false;
            _messages = new List<string>() { errorMessage };
        }

        public GenericCommandHandlerResponse(T? data, string? message)
        {
            Success = true;
            Data = data;
            _messages = new List<string>() { message };
        }
    }
}
