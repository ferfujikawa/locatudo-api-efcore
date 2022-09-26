using Locatudo.Compartilhado.Executores.Comandos.Saidas;

namespace Locatudo.Dominio.Executores.Comandos.Saidas
{
    public class DadoRespostaComandoAprovarLocacao : IDadoRespostaComandoExecutor
    {
        public Guid IdLocacao { get; set; }
        public Guid IdAprovador { get; set; }
        public string NomeAprovador { get; set; }
        public string Situacao { get; set; }

        public DadoRespostaComandoAprovarLocacao(Guid idLocacao, Guid idAprovador, string nomeAprovador, string situacao)
        {
            IdLocacao = idLocacao;
            IdAprovador = idAprovador;
            NomeAprovador = nomeAprovador;
            Situacao = situacao;
        }
    }
}
