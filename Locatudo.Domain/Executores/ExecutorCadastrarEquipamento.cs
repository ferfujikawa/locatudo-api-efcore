using Locatudo.Domain.Executores.Comandos.Entradas;
using Locatudo.Domain.Executores.Comandos.Saidas;
using Locatudo.Domain.Repositorios;
using Locatudo.Domain.Entidades;
using Locatudo.Shared.Executores.Comandos.Saidas;
using Locatudo.Shared.Handlers;
using Locatudo.Shared.Handlers.Commands.Output;

namespace Locatudo.Domain.Executores
{
    public class ExecutorCadastrarEquipamento : IHandler<ComandoCadastrarEquipamento, DadoRespostaComandoCadastrarEquipamento>
    {
        private readonly IRepositorioEquipamento _repositorioEquipamento;

        public ExecutorCadastrarEquipamento(IRepositorioEquipamento repositorioEquipamento)
        {
            _repositorioEquipamento = repositorioEquipamento;
        }

        public IHandlerCommandResponse<DadoRespostaComandoCadastrarEquipamento> Handle(ComandoCadastrarEquipamento comando)
        {
            if (!comando.Validate())
                return new GenericHandlerCommandResponse<DadoRespostaComandoCadastrarEquipamento>(false, null, comando.Notifications);

            var equipamento = new Equipamento(comando.Nome);
            _repositorioEquipamento.Criar(equipamento);

            return new GenericHandlerCommandResponse<DadoRespostaComandoCadastrarEquipamento>(
                true,
                new DadoRespostaComandoCadastrarEquipamento(equipamento.Id, equipamento.Nome),
                "Sucesso",
                "Equipamento cadastrado");
        }
    }
}
