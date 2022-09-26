using Flunt.Notifications;
using Locatudo.Domain.Executores.Comandos.Contratos;
using Locatudo.Shared.Handlers.Commands.Input;

namespace Locatudo.Domain.Executores.Comandos.Entradas
{
    public class ComandoAprovarLocacao : Notifiable<Notification>, IHandlerCommand
    {
        public Guid IdLocacao { get; set; }
        public Guid IdAprovador { get; set; }

        public ComandoAprovarLocacao()
        {
        }

        public ComandoAprovarLocacao(Guid idLocacao, Guid idAprovador)
        {
            IdLocacao = idLocacao;
            IdAprovador = idAprovador;
        }

        public bool Validate()
        {
            AddNotifications(new ContratoComandoAprovarLocacao(this));

            return IsValid;
        }
    }
}
