using Locatudo.Domain.Entidades;
using Locatudo.Shared.Repositorios;
using Locatudo.Shared.ValueObjects;

namespace Locatudo.Domain.Repositorios
{
    public interface IRepositorioLocacao : IRepositorio<Locacao>
    {
        public bool VerificarDisponibilidade(Guid idEquipamento, RentalTime horarioLocacao);
    }
}
