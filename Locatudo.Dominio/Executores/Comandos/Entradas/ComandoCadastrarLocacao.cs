using Flunt.Notifications;
using Locatudo.Dominio.Executores.Comandos.Contratos;
using Locatudo.Shared.Executores.Comandos.Entradas;

namespace Locatudo.Dominio.Executores.Comandos.Entradas
{
    public class ComandoCadastrarLocacao : Notifiable<Notification>, IComandoExecutor
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

        public bool Validar()
        {
            AddNotifications(new ContratoComandoCadastrarLocacao(this));

            return IsValid;
        }
    }
}
