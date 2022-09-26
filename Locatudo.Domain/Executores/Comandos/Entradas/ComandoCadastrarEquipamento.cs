using Flunt.Notifications;
using Locatudo.Domain.Executores.Comandos.Contratos;
using Locatudo.Shared.Handlers.Commands.Input;

namespace Locatudo.Domain.Executores.Comandos.Entradas
{
    public class ComandoCadastrarEquipamento : Notifiable<Notification>, IHandlerCommand
    {
        public string Nome { get; set; }

        public ComandoCadastrarEquipamento()
        {
        }

        public ComandoCadastrarEquipamento(string nome)
        {
            Nome = nome;
        }

        public bool Validate()
        {
            AddNotifications(new ContratoComandoCadastrarEquipamento(this));

            return IsValid;
        }
    }
}
