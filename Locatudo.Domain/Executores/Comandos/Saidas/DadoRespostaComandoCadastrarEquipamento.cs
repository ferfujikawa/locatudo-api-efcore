using Locatudo.Shared.Executores.Comandos.Saidas;

namespace Locatudo.Domain.Executores.Comandos.Saidas
{
    public class DadoRespostaComandoCadastrarEquipamento : IDadoRespostaComandoExecutor
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
