using Locatudo.Dominio.Entidades;
using Locatudo.Shared.ObjetosDeValor;
using Locatudo.Shared.Repositorios;

namespace Locatudo.Dominio.Repositorios
{
    public interface IRepositorioLocacao : IRepositorio<Locacao>
    {
        public bool VerificarDisponibilidade(Guid idEquipamento, HorarioLocacao horarioLocacao);
    }
}
