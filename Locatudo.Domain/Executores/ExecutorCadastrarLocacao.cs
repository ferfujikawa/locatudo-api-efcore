using Locatudo.Shared.ObjetosDeValor;
using Locatudo.Domain.Entidades;
using Locatudo.Shared.Executores;
using Locatudo.Shared.Executores.Comandos.Saidas;
using Locatudo.Domain.Executores.Comandos.Entradas;
using Locatudo.Domain.Repositorios;
using Locatudo.Domain.Executores.Comandos.Saidas;

namespace Locatudo.Domain.Executores
{
    public class ExecutorCadastrarLocacao : IExecutor<ComandoCadastrarLocacao, DadoRespostaComandoCadastrarLocacao>
    {
        private readonly IRepositorioEquipamento _repositorioEquipamento;
        private readonly IRepositorioUsuario _repositorioUsuario;
        private readonly IRepositorioLocacao _repositorioLocacao;

        public ExecutorCadastrarLocacao(
            IRepositorioEquipamento repositorioEquipamento,
            IRepositorioUsuario repositorioUsuario,
            IRepositorioLocacao repositorioLocacao)
        {
            _repositorioEquipamento = repositorioEquipamento;
            _repositorioUsuario = repositorioUsuario;
            _repositorioLocacao = repositorioLocacao;
        }

        public IRespostaComandoExecutor<DadoRespostaComandoCadastrarLocacao> Executar(ComandoCadastrarLocacao comando)
        {
            if (!comando.Validar())
                return new RespostaGenericaComandoExecutor<DadoRespostaComandoCadastrarLocacao>(false, null, comando.Notifications);

            var equipamento = _repositorioEquipamento.ObterPorId(comando.IdEquipamento);
            var horarioLocacao = new HorarioLocacao(comando.Inicio);
            if (equipamento == null)
                return new RespostaGenericaComandoExecutor<DadoRespostaComandoCadastrarLocacao>(false, null, "IdEquipamento", "Equipamento não encontrado");

            var locatario = _repositorioUsuario.ObterPorId(comando.IdLocatario);
            if (locatario == null)
                return new RespostaGenericaComandoExecutor<DadoRespostaComandoCadastrarLocacao>(false, null, "IdLocatario", "Usuário não encontrado");

            if (!_repositorioLocacao.VerificarDisponibilidade(comando.IdEquipamento, horarioLocacao))
                return new RespostaGenericaComandoExecutor<DadoRespostaComandoCadastrarLocacao>(false, null, "Inicio", "Horário de locação indisponível");

            var locacao = new Locacao(equipamento, locatario, horarioLocacao);
            _repositorioLocacao.Criar(locacao);

            return new RespostaGenericaComandoExecutor<DadoRespostaComandoCadastrarLocacao>(
                true,
                new DadoRespostaComandoCadastrarLocacao(
                    locacao.Id,
                    equipamento.Id,
                    equipamento.Nome,
                    locatario.Id,
                    locatario.Nome.ToString(),
                    locacao.Horario.Inicio,
                    locacao.Situacao.Valor.ToString()),
                "Sucesso",
                "Locação cadastrada");
        }
    }
}
