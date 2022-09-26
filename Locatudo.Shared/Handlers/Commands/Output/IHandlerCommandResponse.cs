namespace Locatudo.Shared.Handlers.Commands.Output
{
    public interface IHandlerCommandResponse<T> where T : IHandlerCommandData
    {
        bool Success { get; }
        T? Data { get; }
        IReadOnlyCollection<string> Messages { get; }
    }
}
