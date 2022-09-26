using Locatudo.Compartilhado.Executores;
using Locatudo.Compartilhado.Executores.Comandos.Saidas;
using Locatudo.Dominio.Executores.Comandos.Entradas;
using Locatudo.Dominio.Executores.Comandos.Saidas;
using Locatudo.Dominio.Repositorios;

namespace Locatudo.Dominio.Executores
{
    public class ExecutorCancelarLocacao : IExecutor<ComandoCancelarLocacao, DadoRespostaComandoCancelarLocacao>
    {
        private readonly IRepositorioLocacao _repositorioLocacao;

        public ExecutorCancelarLocacao(IRepositorioLocacao repositorioLocacao)
        {
            _repositorioLocacao = repositorioLocacao;
        }

        public IRespostaComandoExecutor<DadoRespostaComandoCancelarLocacao> Executar(ComandoCancelarLocacao comando)
        {
            if (!comando.Validar())
                return new RespostaGenericaComandoExecutor<DadoRespostaComandoCancelarLocacao>(false, null, comando.Notifications);

            var locacao = _repositorioLocacao.ObterPorId(comando.IdLocacao);
            if (locacao == null)
                return new RespostaGenericaComandoExecutor<DadoRespostaComandoCancelarLocacao>(false, null, "IdLocacao", "Locação não encontrada.");

            if (!locacao.Cancelar())
                return new RespostaGenericaComandoExecutor<DadoRespostaComandoCancelarLocacao>(false, null, "Situacao", "A situação atual da locação não permite cancelamento.");

            return new RespostaGenericaComandoExecutor<DadoRespostaComandoCancelarLocacao>(
                true,
                new DadoRespostaComandoCancelarLocacao(locacao.Id, locacao.Situacao.Valor.ToString()),
                "Sucesso",
                "Locação cancelada.");
        }
    }
}
