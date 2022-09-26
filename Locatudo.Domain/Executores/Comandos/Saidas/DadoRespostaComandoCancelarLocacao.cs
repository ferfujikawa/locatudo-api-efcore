using Locatudo.Shared.Handlers.Commands.Output;

namespace Locatudo.Domain.Executores.Comandos.Saidas
{
    public class DadoRespostaComandoCancelarLocacao : IHandlerCommandData
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
