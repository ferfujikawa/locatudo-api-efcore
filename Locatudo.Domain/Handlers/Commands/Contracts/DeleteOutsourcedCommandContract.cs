using Flunt.Validations;
using Locatudo.Domain.Handlers.Commands.Inputs;

namespace Locatudo.Domain.Handlers.Commands.Contracts
{
    public class DeleteOutsourcedCommandContract : Contract<DeleteOutsourcedCommand>
    {
        public DeleteOutsourcedCommandContract(DeleteOutsourcedCommand command)
        {
            Requires()
                .AreNotEquals(command.Id, default, "Id", "É necessário informar o Id do terceirizado que se pretende excluir");
        }
    }
}
