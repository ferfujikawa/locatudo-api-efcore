using FluentValidation;
using Locatudo.Shared.Queries.Requests;

namespace Locatudo.Shared.Queries
{
    public interface IQueryValidator<T> : IValidator<T> where T : IQueryRequest
    {
    }
}
