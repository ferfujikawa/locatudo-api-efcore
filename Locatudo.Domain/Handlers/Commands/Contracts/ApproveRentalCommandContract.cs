using Flunt.Validations;
using Locatudo.Domain.Handlers.Commands.Inputs;

namespace Locatudo.Domain.Handlers.Commands.Contracts
{
    public class ApproveRentalCommandContract : Contract<ApproveRentalCommand>
    {
        public ApproveRentalCommandContract(ApproveRentalCommand command)
        {
            Requires()
                .AreNotEquals(command.RentalId, default, "RentalId", "É necessário informar o Id da locação que se pretende aprovar")
                .AreNotEquals(command.AppraiserId, default, "AppraiserId", "É necessário informar o Id do funcionário que está aprovando a locacação");
        }
    }
}
