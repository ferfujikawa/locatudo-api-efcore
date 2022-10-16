using Locatudo.Shared.Entities;

namespace Locatudo.Shared.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        void Create(T entity);
        IEnumerable<T> List();
        IEnumerable<U> List<U>();
        T? GetById(Guid id);
        U? GetById<U>(Guid id);
        void Update(T entity);
        void Delete(Guid id);
    }
}
