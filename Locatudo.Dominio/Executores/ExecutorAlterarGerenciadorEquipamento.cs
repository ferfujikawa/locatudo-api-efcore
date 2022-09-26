using Locatudo.Compartilhado.Executores;
using Locatudo.Compartilhado.Executores.Comandos.Saidas;
using Locatudo.Dominio.Executores.Comandos.Entradas;
using Locatudo.Dominio.Executores.Comandos.Saidas;
using Locatudo.Dominio.Repositorios;

namespace Locatudo.Dominio.Executores
{
    public class ExecutorAlterarGerenciadorEquipamento : IExecutor<ComandoAlterarGerenciadorEquipamento, DadoRespostaComandoAlterarGerenciadorEquipamento>
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

        public IRespostaComandoExecutor<DadoRespostaComandoAlterarGerenciadorEquipamento> Executar(ComandoAlterarGerenciadorEquipamento comando)
        {
            if (!comando.Validar())
                return new RespostaGenericaComandoExecutor<DadoRespostaComandoAlterarGerenciadorEquipamento>(false, null, comando.Notifications);

            var equipamento = _repositorioEquipamento.ObterPorId(comando.Id);
            if (equipamento == null)
                return new RespostaGenericaComandoExecutor<DadoRespostaComandoAlterarGerenciadorEquipamento>(false, null, "IdEquipamento", "Equipamento não encontrado");

            var departamento = _repositorioDepartamento.ObterPorId(comando.IdDepartamento);
            if (departamento == null)
                return new RespostaGenericaComandoExecutor<DadoRespostaComandoAlterarGerenciadorEquipamento>(false, null, "IdDepartamento", "Departamento não encontrado");

            equipamento.AlterarGerenciador(departamento);
            _repositorioEquipamento.Alterar(equipamento);

            return new RespostaGenericaComandoExecutor<DadoRespostaComandoAlterarGerenciadorEquipamento>(
                true,
                new DadoRespostaComandoAlterarGerenciadorEquipamento(equipamento.Id, equipamento.Nome, departamento.Id, departamento.Nome),
                "Sucesso",
                "Gerenciador do equipamento alterado");
        }
    }
}
