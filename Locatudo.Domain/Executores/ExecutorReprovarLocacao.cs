using Locatudo.Domain.Executores.Comandos.Entradas;
using Locatudo.Domain.Executores.Comandos.Saidas;
using Locatudo.Domain.Repositorios;
using Locatudo.Shared.Executores.Comandos.Saidas;
using Locatudo.Shared.Handlers;
using Locatudo.Shared.Handlers.Commands.Output;

namespace Locatudo.Domain.Executores
{
    public class ExecutorReprovarLocacao : IHandler<ComandoReprovarLocacao, DadoRespostaComandoReprovarLocacao>
    {
        private readonly IRepositorioLocacao _repositorioLocacao;
        private readonly IRepositorioFuncionario _repositorioFuncionario;

        public ExecutorReprovarLocacao(
            IRepositorioLocacao repositorioLocacao,
            IRepositorioFuncionario repositorioFuncionario)
        {
            _repositorioLocacao = repositorioLocacao;
            _repositorioFuncionario = repositorioFuncionario;
        }

        public IHandlerCommandResponse<DadoRespostaComandoReprovarLocacao> Handle(ComandoReprovarLocacao comando)
        {
            if (!comando.Validate())
                return new GenericHandlerCommandResponse<DadoRespostaComandoReprovarLocacao>(false, null, comando.Notifications);

            var aprovador = _repositorioFuncionario.ObterPorId(comando.IdAprovador);
            if (aprovador == null)
                return new GenericHandlerCommandResponse<DadoRespostaComandoReprovarLocacao>(false, null, "IdAprovador", "Funcionário não encontrado.");

            var locacao = _repositorioLocacao.ObterPorId(comando.IdLocacao);
            if (locacao == null)
                return new GenericHandlerCommandResponse<DadoRespostaComandoReprovarLocacao>(false, null, "IdLocacao", "Locação não encontrada.");

            if (!locacao.PodeSerAprovadaReprovadaPor(aprovador))
                return new GenericHandlerCommandResponse<DadoRespostaComandoReprovarLocacao>(false, null, "IdAprovador", "Aprovador não está lotado no departamento gerenciador do equipamento.");

            if (!locacao.Reprovar(aprovador))
                return new GenericHandlerCommandResponse<DadoRespostaComandoReprovarLocacao>(false, null, "Situacao", "A situação atual da locação não permite reprovação.");

            return new GenericHandlerCommandResponse<DadoRespostaComandoReprovarLocacao>(
                true,
                new DadoRespostaComandoReprovarLocacao(locacao.Id, aprovador.Id, aprovador.Nome.ToString(), locacao.Situacao.Value.ToString()),
                "Sucesso",
                "Locação reprovada.");
        }
    }
}
