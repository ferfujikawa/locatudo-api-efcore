using Locatudo.Compartilhado.Executores.Comandos.Saidas;

namespace Locatudo.Dominio.Executores.Comandos.Saidas
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
