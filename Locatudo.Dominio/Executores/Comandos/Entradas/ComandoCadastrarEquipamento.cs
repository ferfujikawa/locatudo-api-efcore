using Flunt.Notifications;
using Locatudo.Dominio.Executores.Comandos.Contratos;
using Locatudo.Shared.Executores.Comandos.Entradas;

namespace Locatudo.Dominio.Executores.Comandos.Entradas
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
