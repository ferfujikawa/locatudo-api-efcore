using Flunt.Validations;
using Locatudo.Domain.Commands.Requests;

namespace Locatudo.Domain.Commands.Contracts
{
    public class CancelRentalContract : Contract<CancelRentalRequest>
    {
        public CancelRentalContract(CancelRentalRequest command)
        {
            Requires()
                .AreNotEquals(command.RentalId, default, "RentalId", "É necessário informar o Id da locação que se pretende cancelar");
        }
    }
}
