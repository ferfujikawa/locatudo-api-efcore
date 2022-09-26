using Flunt.Notifications;
using Locatudo.Domain.Executores.Comandos.Contratos;
using Locatudo.Shared.Handlers.Commands.Input;

namespace Locatudo.Domain.Executores.Comandos.Entradas
{
    public class ComandoCancelarLocacao : Notifiable<Notification>, IHandlerCommand
    {
        public Guid IdLocacao { get; set; }

        public ComandoCancelarLocacao()
        {
        }

        public ComandoCancelarLocacao(Guid idLocacao)
        {
            IdLocacao = idLocacao;
        }

        public bool Validate()
        {
            AddNotifications(new ContratoComandoCancelarLocacao(this));

            return IsValid;
        }
    }
}
