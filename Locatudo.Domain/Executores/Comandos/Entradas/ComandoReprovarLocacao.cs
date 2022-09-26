using Flunt.Notifications;
using Locatudo.Domain.Executores.Comandos.Contratos;
using Locatudo.Shared.Handlers.Commands.Input;

namespace Locatudo.Domain.Executores.Comandos.Entradas
{
    public class ComandoReprovarLocacao : Notifiable<Notification>, IHandlerCommand
    {
        public Guid IdLocacao { get; set; }
        public Guid IdAprovador { get; set; }

        public ComandoReprovarLocacao()
        {
        }

        public ComandoReprovarLocacao(Guid idLocacao, Guid idAprovador)
        {
            IdLocacao = idLocacao;
            IdAprovador = idAprovador;
        }

        public bool Validate()
        {
            AddNotifications(new ContratoComandoReprovarLocacao(this));

            return IsValid;
        }
    }
}
