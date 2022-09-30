using Flunt.Validations;
using Locatudo.Domain.Handlers.Commands.Inputs;

namespace Locatudo.Domain.Handlers.Commands.Contracts
{
    public class DisapproveRentalCommandContract : Contract<DisapproveRentalCommand>
    {
        public DisapproveRentalCommandContract(DisapproveRentalCommand command)
        {
            Requires()
                .AreNotEquals(command.RentalId, default, "RentalId", "É necessário informar o Id da locação que se pretende reprovar")
                .AreNotEquals(command.AppraiserId, default, "AppraiserId", "É necessário informar o Id do funcionário que está reprovando a locacação");
        }
    }
}
