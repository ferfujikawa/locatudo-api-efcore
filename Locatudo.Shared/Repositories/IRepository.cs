using Locatudo.Shared.Entities;

namespace Locatudo.Shared.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        void Create(T entity);
        IEnumerable<T> List();
        T? GetById(Guid id);
        void Change(T entity);
        void Delete(Guid id);
    }
}
