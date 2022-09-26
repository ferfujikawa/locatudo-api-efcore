using Locatudo.Compartilhado.ObjetosDeValor;
using Locatudo.Compartilhado.Repositorios;
using Locatudo.Dominio.Entidades;

namespace Locatudo.Dominio.Repositorios
{
    public interface IRepositorioLocacao : IRepositorio<Locacao>
    {
        public bool VerificarDisponibilidade(Guid idEquipamento, HorarioLocacao horarioLocacao);
    }
}
