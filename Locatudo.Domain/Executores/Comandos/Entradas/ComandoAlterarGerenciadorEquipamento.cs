using Flunt.Notifications;
using Locatudo.Domain.Executores.Comandos.Contratos;
using Locatudo.Shared.Executores.Comandos.Entradas;

namespace Locatudo.Domain.Executores.Comandos.Entradas
{
    public class ComandoAlterarGerenciadorEquipamento : Notifiable<Notification>, IComandoExecutor
    {
        public Guid Id { get; set; }
        public Guid IdDepartamento { get; set; }

        public ComandoAlterarGerenciadorEquipamento()
        {
        }

        public ComandoAlterarGerenciadorEquipamento(Guid id, Guid idDepartamento)
        {
            Id = id;
            IdDepartamento = idDepartamento;
        }

        public bool Validar()
        {
            AddNotifications(new ContratoComandoAlterarGerenciadorEquipamento(this));

            return IsValid;
        }
    }
}
