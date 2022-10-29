namespace Locatudo.Shared.Queries.Responses;

public interface IQueryResponse<T> where T : IQueryData
{
    bool Success { get; }
    T? Data { get; }
    IReadOnlyCollection<string> Messages { get; }
}
