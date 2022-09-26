using Flunt.Notifications;
using Locatudo.Domain.Executores.Comandos.Contratos;
using Locatudo.Shared.Handlers.Commands.Input;

namespace Locatudo.Domain.Executores.Comandos.Entradas
{
    public class ComandoCadastrarLocacao : Notifiable<Notification>, IHandlerCommand
    {
        public Guid IdEquipamento { get; set; }
        public Guid IdLocatario { get; set; }
        public DateTime Inicio { get; set; }

        public ComandoCadastrarLocacao()
        {
        }

        public ComandoCadastrarLocacao(Guid idEquipamento, Guid idLocatario, DateTime inicio)
        {
            IdEquipamento = idEquipamento;
            IdLocatario = idLocatario;
            Inicio = inicio;
        }

        public bool Validate()
        {
            AddNotifications(new ContratoComandoCadastrarLocacao(this));

            return IsValid;
        }
    }
}
