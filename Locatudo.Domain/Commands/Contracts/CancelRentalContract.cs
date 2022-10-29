using Flunt.Validations;
using Locatudo.Domain.Commands.Requests;

namespace Locatudo.Domain.Commands.Contracts
{
    public class CancelRentalContract : Contract<CancelRentalRequest>
    {
        public CancelRentalContract(CancelRentalRequest request)
        {
            Requires()
                .AreNotEquals(request.RentalId, default, "RentalId", "É necessário informar o Id da locação que se pretende cancelar");
        }
    }
}
