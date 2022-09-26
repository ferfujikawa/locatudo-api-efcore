using Flunt.Notifications;
using Locatudo.Domain.Executores.Comandos.Contratos;
using Locatudo.Shared.Executores.Comandos.Entradas;

namespace Locatudo.Domain.Executores.Comandos.Entradas
{
    public class ComandoCadastrarEquipamento : Notifiable<Notification>, IComandoExecutor
    {
        public string Nome { get; set; }

        public ComandoCadastrarEquipamento()
        {
        }

        public ComandoCadastrarEquipamento(string nome)
        {
            Nome = nome;
        }

        public bool Validar()
        {
            AddNotifications(new ContratoComandoCadastrarEquipamento(this));

            return IsValid;
        }
    }
}
