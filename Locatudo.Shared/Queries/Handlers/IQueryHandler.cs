using Locatudo.Shared.Queries.Responses;
using Locatudo.Shared.Queries.Requests;

namespace Locatudo.Shared.Queries.Handlers;

public interface IQueryHandler<T, U> where T : IQueryRequest where U : IQueryData
{
    IQueryResponse<U> Handle(T request);
}
