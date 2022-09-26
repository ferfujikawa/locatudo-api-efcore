using Locatudo.Domain.Executores.Comandos.Entradas;
using Locatudo.Domain.Executores.Comandos.Saidas;
using Locatudo.Domain.Repositorios;
using Locatudo.Shared.Executores.Comandos.Saidas;
using Locatudo.Shared.Handlers;
using Locatudo.Shared.Handlers.Commands.Output;

namespace Locatudo.Domain.Executores
{
    public class ExecutorAlterarGerenciadorEquipamento : IHandler<ComandoAlterarGerenciadorEquipamento, DadoRespostaComandoAlterarGerenciadorEquipamento>
    {
        private readonly IRepositorioEquipamento _repositorioEquipamento;
        private readonly IRepositorioDepartamento _repositorioDepartamento;

        public ExecutorAlterarGerenciadorEquipamento(
            IRepositorioEquipamento repositorioEquipamento,
            IRepositorioDepartamento repositorioDepartamento)
        {
            _repositorioEquipamento = repositorioEquipamento;
            _repositorioDepartamento = repositorioDepartamento;
        }

        public IHandlerCommandResponse<DadoRespostaComandoAlterarGerenciadorEquipamento> Handle(ComandoAlterarGerenciadorEquipamento comando)
        {
            if (!comando.Validate())
                return new GenericHandlerCommandResponse<DadoRespostaComandoAlterarGerenciadorEquipamento>(false, null, comando.Notifications);

            var equipamento = _repositorioEquipamento.ObterPorId(comando.Id);
            if (equipamento == null)
                return new GenericHandlerCommandResponse<DadoRespostaComandoAlterarGerenciadorEquipamento>(false, null, "IdEquipamento", "Equipamento não encontrado");

            var departamento = _repositorioDepartamento.ObterPorId(comando.IdDepartamento);
            if (departamento == null)
                return new GenericHandlerCommandResponse<DadoRespostaComandoAlterarGerenciadorEquipamento>(false, null, "IdDepartamento", "Departamento não encontrado");

            equipamento.AlterarGerenciador(departamento);
            _repositorioEquipamento.Alterar(equipamento);

            return new GenericHandlerCommandResponse<DadoRespostaComandoAlterarGerenciadorEquipamento>(
                true,
                new DadoRespostaComandoAlterarGerenciadorEquipamento(equipamento.Id, equipamento.Nome, departamento.Id, departamento.Nome),
                "Sucesso",
                "Gerenciador do equipamento alterado");
        }
    }
}
