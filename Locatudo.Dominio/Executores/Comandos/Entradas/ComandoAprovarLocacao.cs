using Flunt.Notifications;
using Locatudo.Dominio.Executores.Comandos.Contratos;
using Locatudo.Shared.Executores.Comandos.Entradas;

namespace Locatudo.Dominio.Executores.Comandos.Entradas
{
    public class ComandoAprovarLocacao : Notifiable<Notification>, IComandoExecutor
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

        public bool Validar()
        {
            AddNotifications(new ContratoComandoAprovarLocacao(this));

            return IsValid;
        }
    }
}
