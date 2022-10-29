using Flunt.Validations;
using Locatudo.Domain.Commands.Requests;

namespace Locatudo.Domain.Commands.Contracts
{
    public class ApproveRentalContract : Contract<ApproveRentalRequest>
    {
        public ApproveRentalContract(ApproveRentalRequest command)
        {
            Requires()
                .AreNotEquals(command.RentalId, default, "RentalId", "É necessário informar o Id da locação que se pretende aprovar")
                .AreNotEquals(command.AppraiserId, default, "AppraiserId", "É necessário informar o Id do funcionário que está aprovando a locacação");
        }
    }
}
