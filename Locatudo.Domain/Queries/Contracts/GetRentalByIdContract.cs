using Flunt.Validations;
using Locatudo.Domain.Queries.Requests;

namespace Locatudo.Domain.Queries.Contracts
{
    public class GetRentalByIdContract : Contract<GetRentalByIdRequest>
    {
        public GetRentalByIdContract(GetRentalByIdRequest command)
        {
            Requires()
                .AreNotEquals(command.Id, default, "Id", "É necessário informar o Id da locação que se pretende obter");
        }
    }
}
