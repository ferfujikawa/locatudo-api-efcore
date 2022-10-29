using Flunt.Validations;
using Locatudo.Domain.Commands.Requests;

namespace Locatudo.Domain.Commands.Contracts
{
    public class DeleteOutsourcedContract : Contract<DeleteOutsourcedRequest>
    {
        public DeleteOutsourcedContract(DeleteOutsourcedRequest request)
        {
            Requires()
                .AreNotEquals(request.Id, default, "Id", "É necessário informar o Id do terceirizado que se pretende excluir");
        }
    }
}
