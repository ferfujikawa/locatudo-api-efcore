using Locatudo.Shared.Entities;

namespace Locatudo.Domain.Queries.Extensions
{
    public static class BaseEntityQueries
    {
        public static IQueryable<T> FilterById<T>(this IQueryable<T> query, Guid id) where T : BaseEntity
        {
            return query.Where(x => x.Id == id);
        }
    }
}
