using Locatudo.Shared.Commands.Requests;

namespace Locatudo.Domain.Commands.Requests
{
    public class CancelRentalRequest : ICommandRequest
    {
        public Guid RentalId { get; set; }

        public CancelRentalRequest()
        {
        }

        public CancelRentalRequest(Guid rentalId)
        {
            RentalId = rentalId;
        }

        public bool Validate()
        {
            return true;
        }
    }
}
