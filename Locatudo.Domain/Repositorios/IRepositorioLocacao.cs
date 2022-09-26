using Locatudo.Domain.Entidades;
using Locatudo.Shared.ObjetosDeValor;
using Locatudo.Shared.Repositorios;

namespace Locatudo.Domain.Repositorios
{
    public interface IRepositorioLocacao : IRepositorio<Locacao>
    {
        public bool VerificarDisponibilidade(Guid idEquipamento, HorarioLocacao horarioLocacao);
    }
}
