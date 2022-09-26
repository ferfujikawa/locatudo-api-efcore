using Locatudo.Shared.Handlers.Commands.Output;

namespace Locatudo.Domain.Executores.Comandos.Saidas
{
    public class DadoRespostaComandoCadastrarEquipamento : IHandlerCommandData
    {
        public Guid IdEquipamento { get; set; }
        public string Nome { get; set; }

        public DadoRespostaComandoCadastrarEquipamento(Guid idEquipamento, string nome)
        {
            IdEquipamento = idEquipamento;
            Nome = nome;
        }
    }
}
