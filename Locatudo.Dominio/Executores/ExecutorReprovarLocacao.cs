using Locatudo.Compartilhado.Executores;
using Locatudo.Compartilhado.Executores.Comandos.Saidas;
using Locatudo.Dominio.Executores.Comandos.Entradas;
using Locatudo.Dominio.Executores.Comandos.Saidas;
using Locatudo.Dominio.Repositorios;

namespace Locatudo.Dominio.Executores
{
    public class ExecutorReprovarLocacao : IExecutor<ComandoReprovarLocacao, DadoRespostaComandoReprovarLocacao>
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

        public IRespostaComandoExecutor<DadoRespostaComandoReprovarLocacao> Executar(ComandoReprovarLocacao comando)
        {
            if (!comando.Validar())
                return new RespostaGenericaComandoExecutor<DadoRespostaComandoReprovarLocacao>(false, null, comando.Notifications);

            var aprovador = _repositorioFuncionario.ObterPorId(comando.IdAprovador);
            if (aprovador == null)
                return new RespostaGenericaComandoExecutor<DadoRespostaComandoReprovarLocacao>(false, null, "IdAprovador", "Funcionário não encontrado.");

            var locacao = _repositorioLocacao.ObterPorId(comando.IdLocacao);
            if (locacao == null)
                return new RespostaGenericaComandoExecutor<DadoRespostaComandoReprovarLocacao>(false, null, "IdLocacao", "Locação não encontrada.");

            if (!locacao.PodeSerAprovadaReprovadaPor(aprovador))
                return new RespostaGenericaComandoExecutor<DadoRespostaComandoReprovarLocacao>(false, null, "IdAprovador", "Aprovador não está lotado no departamento gerenciador do equipamento.");

            if (!locacao.Reprovar(aprovador))
                return new RespostaGenericaComandoExecutor<DadoRespostaComandoReprovarLocacao>(false, null, "Situacao", "A situação atual da locação não permite reprovação.");

            return new RespostaGenericaComandoExecutor<DadoRespostaComandoReprovarLocacao>(
                true,
                new DadoRespostaComandoReprovarLocacao(locacao.Id, aprovador.Id, aprovador.Nome.ToString(), locacao.Situacao.Valor.ToString()),
                "Sucesso",
                "Locação reprovada.");
        }
    }
}
