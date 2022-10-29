namespace Locatudo.Shared.Commands.Responses
{
    public interface ICommandResponse<T> where T : ICommandData
    {
        bool Success { get; }
        T? Data { get; }
        IReadOnlyCollection<string> Messages { get; }
    }
}
