using Flunt.Validations;
using Locatudo.Domain.Queries.Commands.Inputs;

namespace Locatudo.Domain.Queries.Commands.Contracts
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
