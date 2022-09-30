using Flunt.Validations;
using Locatudo.Domain.Handlers.Commands.Inputs;

namespace Locatudo.Domain.Handlers.Commands.Contracts
{
    public class CancelRentalCommandContract : Contract<CancelRentalCommand>
    {
        public CancelRentalCommandContract(CancelRentalCommand command)
        {
            Requires()
                .AreNotEquals(command.RentalId, default, "RentalId", "É necessário informar o Id da locação que se pretende cancelar");
        }
    }
}
