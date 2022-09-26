using Locatudo.Domain.Executores.Comandos.Entradas;
using Locatudo.Domain.Executores.Comandos.Saidas;
using Locatudo.Domain.Repositorios;
using Locatudo.Shared.Executores.Comandos.Saidas;
using Locatudo.Shared.Handlers;
using Locatudo.Shared.Handlers.Commands.Output;

namespace Locatudo.Domain.Executores
{
    public class ExecutorCancelarLocacao : IHandler<ComandoCancelarLocacao, DadoRespostaComandoCancelarLocacao>
    {
        private readonly IRepositorioLocacao _repositorioLocacao;

        public ExecutorCancelarLocacao(IRepositorioLocacao repositorioLocacao)
        {
            _repositorioLocacao = repositorioLocacao;
        }

        public IHandlerCommandResponse<DadoRespostaComandoCancelarLocacao> Handle(ComandoCancelarLocacao comando)
        {
            if (!comando.Validate())
                return new GenericHandlerCommandResponse<DadoRespostaComandoCancelarLocacao>(false, null, comando.Notifications);

            var locacao = _repositorioLocacao.ObterPorId(comando.IdLocacao);
            if (locacao == null)
                return new GenericHandlerCommandResponse<DadoRespostaComandoCancelarLocacao>(false, null, "IdLocacao", "Locação não encontrada.");

            if (!locacao.Cancelar())
                return new GenericHandlerCommandResponse<DadoRespostaComandoCancelarLocacao>(false, null, "Situacao", "A situação atual da locação não permite cancelamento.");

            return new GenericHandlerCommandResponse<DadoRespostaComandoCancelarLocacao>(
                true,
                new DadoRespostaComandoCancelarLocacao(locacao.Id, locacao.Situacao.Value.ToString()),
                "Sucesso",
                "Locação cancelada.");
        }
    }
}
