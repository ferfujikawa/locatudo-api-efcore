using Locatudo.Compartilhado.Executores.Comandos.Saidas;

namespace Locatudo.Dominio.Executores.Comandos.Saidas
{
    public class DadoRespostaComandoCadastrarLocacao : IDadoRespostaComandoExecutor
    {
        public Guid IdLocacao { get; set; }
        public Guid IdEquipamento { get; set; }
        public string NomeEquipamento { get; set; }
        public Guid IdLocatario { get; set; }
        public string NomeLocatario { get; set; }
        public DateTime Inicio { get; set; }
        public string Situacao { get; set; }

        public DadoRespostaComandoCadastrarLocacao(Guid idLocacao, Guid idEquipamento, string nomeEquipamento, Guid idLocatario, string nomeLocatario, DateTime inicio, string situacao)
        {
            IdLocacao = idLocacao;
            IdEquipamento = idEquipamento;
            NomeEquipamento = nomeEquipamento;
            IdLocatario = idLocatario;
            NomeLocatario = nomeLocatario;
            Inicio = inicio;
            Situacao = situacao;
        }
    }
}
