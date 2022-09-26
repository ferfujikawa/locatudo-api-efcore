using Locatudo.Domain.Executores.Comandos.Entradas;
using Locatudo.Domain.Executores.Comandos.Saidas;
using Locatudo.Domain.Repositorios;
using Locatudo.Domain.Entidades;
using Locatudo.Shared.Executores;
using Locatudo.Shared.Executores.Comandos.Saidas;

namespace Locatudo.Domain.Executores
{
    public class ExecutorCadastrarEquipamento : IExecutor<ComandoCadastrarEquipamento, DadoRespostaComandoCadastrarEquipamento>
    {
        private readonly IRepositorioEquipamento _repositorioEquipamento;

        public ExecutorCadastrarEquipamento(IRepositorioEquipamento repositorioEquipamento)
        {
            _repositorioEquipamento = repositorioEquipamento;
        }

        public IRespostaComandoExecutor<DadoRespostaComandoCadastrarEquipamento> Executar(ComandoCadastrarEquipamento comando)
        {
            if (!comando.Validar())
                return new RespostaGenericaComandoExecutor<DadoRespostaComandoCadastrarEquipamento>(false, null, comando.Notifications);

            var equipamento = new Equipamento(comando.Nome);
            _repositorioEquipamento.Criar(equipamento);

            return new RespostaGenericaComandoExecutor<DadoRespostaComandoCadastrarEquipamento>(
                true,
                new DadoRespostaComandoCadastrarEquipamento(equipamento.Id, equipamento.Nome),
                "Sucesso",
                "Equipamento cadastrado");
        }
    }
}
