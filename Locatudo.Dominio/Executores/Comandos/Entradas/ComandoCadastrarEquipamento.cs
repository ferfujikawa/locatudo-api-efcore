using Flunt.Notifications;
using Locatudo.Compartilhado.Executores.Comandos.Entradas;
using Locatudo.Dominio.Executores.Comandos.Contratos;

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
