using Flunt.Validations;
using Locatudo.Domain.Commands.Requests;

namespace Locatudo.Domain.Commands.Contracts
{
    public class DeleteOutsourcedContract : Contract<DeleteOutsourcedRequest>
    {
        public DeleteOutsourcedContract(DeleteOutsourcedRequest command)
        {
            Requires()
                .AreNotEquals(command.Id, default, "Id", "É necessário informar o Id do terceirizado que se pretende excluir");
        }
    }
}
