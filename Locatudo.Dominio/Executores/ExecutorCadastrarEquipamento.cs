using Locatudo.Compartilhado.Executores;
using Locatudo.Compartilhado.Executores.Comandos.Saidas;
using Locatudo.Dominio.Entidades;
using Locatudo.Dominio.Executores.Comandos.Entradas;
using Locatudo.Dominio.Executores.Comandos.Saidas;
using Locatudo.Dominio.Repositorios;

namespace Locatudo.Dominio.Executores
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
