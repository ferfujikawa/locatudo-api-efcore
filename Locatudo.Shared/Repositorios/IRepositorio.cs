using Locatudo.Shared.Entidades;

namespace Locatudo.Shared.Repositorios
{
    public interface IRepositorio<T> where T : EntidadeBase
    {
        void Criar(T entidade);
        IEnumerable<T> Listar();
        T? ObterPorId(Guid id);
        void Alterar(T entidade);
        void Excluir(Guid id);
    }
}
