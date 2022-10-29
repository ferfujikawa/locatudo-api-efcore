using Flunt.Validations;
using Locatudo.Domain.Commands.Requests;

namespace Locatudo.Domain.Commands.Contracts
{
    public class DisapproveRentalContract : Contract<DisapproveRentalRequest>
    {
        public DisapproveRentalContract(DisapproveRentalRequest command)
        {
            Requires()
                .AreNotEquals(command.RentalId, default, "RentalId", "É necessário informar o Id da locação que se pretende reprovar")
                .AreNotEquals(command.AppraiserId, default, "AppraiserId", "É necessário informar o Id do funcionário que está reprovando a locacação");
        }
    }
}
