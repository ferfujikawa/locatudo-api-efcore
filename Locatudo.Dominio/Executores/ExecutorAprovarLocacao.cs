using Locatudo.Compartilhado.Executores;
using Locatudo.Compartilhado.Executores.Comandos.Saidas;
using Locatudo.Dominio.Executores.Comandos.Entradas;
using Locatudo.Dominio.Executores.Comandos.Saidas;
using Locatudo.Dominio.Repositorios;

namespace Locatudo.Dominio.Executores
{
    public class ExecutorAprovarLocacao : IExecutor<ComandoAprovarLocacao, DadoRespostaComandoAprovarLocacao>
    {
        private readonly IRepositorioLocacao _repositorioLocacao;
        private readonly IRepositorioFuncionario _repositorioFuncionario;

        public ExecutorAprovarLocacao(
            IRepositorioLocacao repositorioLocacao,
            IRepositorioFuncionario repositorioFuncionario)
        {
            _repositorioLocacao = repositorioLocacao;
            _repositorioFuncionario = repositorioFuncionario;
        }

        public IRespostaComandoExecutor<DadoRespostaComandoAprovarLocacao> Executar(ComandoAprovarLocacao comando)
        {
            if (!comando.Validar())
                return new RespostaGenericaComandoExecutor<DadoRespostaComandoAprovarLocacao>(false, null, comando.Notifications);
            
            var aprovador = _repositorioFuncionario.ObterPorId(comando.IdAprovador);
            if (aprovador == null)
                return new RespostaGenericaComandoExecutor<DadoRespostaComandoAprovarLocacao>(false, null, "IdAprovador", "Funcionário não encontrado");

            var locacao = _repositorioLocacao.ObterPorId(comando.IdLocacao);
            if (locacao == null)
                return new RespostaGenericaComandoExecutor<DadoRespostaComandoAprovarLocacao>(false, null, "IdLocacao", "Locação não encontrada.");
            
            if (locacao.PodeSerAprovadaReprovadaPor(aprovador) == false)
                return new RespostaGenericaComandoExecutor<DadoRespostaComandoAprovarLocacao>(false, null, "IdAprovador", "Aprovador não está lotado no departamento gerenciador do equipamento.");

            if (locacao.Aprovar(aprovador) == false)
                return new RespostaGenericaComandoExecutor<DadoRespostaComandoAprovarLocacao>(false, null, "Situacao", "A situação atual da locação não permite aprovação.");

            return new RespostaGenericaComandoExecutor<DadoRespostaComandoAprovarLocacao>(
                true,
                new DadoRespostaComandoAprovarLocacao(locacao.Id, aprovador.Id, aprovador.Nome.ToString(), locacao.Situacao.Valor.ToString()),
                "Sucesso",
                "Locação aprovada");
        }
    }
}
