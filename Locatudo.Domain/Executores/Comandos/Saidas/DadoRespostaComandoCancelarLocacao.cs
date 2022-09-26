using Locatudo.Shared.Executores.Comandos.Saidas;

namespace Locatudo.Domain.Executores.Comandos.Saidas
{
    public class DadoRespostaComandoCancelarLocacao : IDadoRespostaComandoExecutor
    {
        public Guid IdLocacao { get; set; }
        public string Situacao { get; set; }

        public DadoRespostaComandoCancelarLocacao(Guid idLocacao, string situacao)
        {
            IdLocacao = idLocacao;
            Situacao = situacao;
        }
    }
}
