using Flunt.Notifications;
using Locatudo.Compartilhado.Executores.Comandos.Entradas;
using Locatudo.Dominio.Executores.Comandos.Contratos;

namespace Locatudo.Dominio.Executores.Comandos.Entradas
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
