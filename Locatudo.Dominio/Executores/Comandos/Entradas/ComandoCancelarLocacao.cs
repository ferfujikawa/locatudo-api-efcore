using Flunt.Notifications;
using Locatudo.Compartilhado.Executores.Comandos.Entradas;
using Locatudo.Dominio.Executores.Comandos.Contratos;

namespace Locatudo.Dominio.Executores.Comandos.Entradas
{
    public class ComandoCancelarLocacao : Notifiable<Notification>, IComandoExecutor
    {
        public Guid IdLocacao { get; set; }

        public ComandoCancelarLocacao()
        {
        }

        public ComandoCancelarLocacao(Guid idLocacao)
        {
            IdLocacao = idLocacao;
        }

        public bool Validar()
        {
            AddNotifications(new ContratoComandoCancelarLocacao(this));

            return IsValid;
        }
    }
}
