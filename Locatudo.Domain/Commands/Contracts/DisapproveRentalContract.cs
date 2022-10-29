using Flunt.Validations;
using Locatudo.Domain.Commands.Requests;

namespace Locatudo.Domain.Commands.Contracts
{
    public class DisapproveRentalContract : Contract<DisapproveRentalRequest>
    {
        public DisapproveRentalContract(DisapproveRentalRequest request)
        {
            Requires()
                .AreNotEquals(request.RentalId, default, "RentalId", "É necessário informar o Id da locação que se pretende reprovar")
                .AreNotEquals(request.AppraiserId, default, "AppraiserId", "É necessário informar o Id do funcionário que está reprovando a locacação");
        }
    }
}
