using Locatudo.Shared.Commands.Requests;

namespace Locatudo.Domain.Commands.Requests
{
    public class DisapproveRentalRequest : ICommandRequest
    {
        public Guid RentalId { get; set; }
        public Guid AppraiserId { get; set; }

        public DisapproveRentalRequest()
        {
        }

        public DisapproveRentalRequest(Guid rentalId, Guid appraiserId)
        {
            RentalId = rentalId;
            AppraiserId = appraiserId;
        }
    }
}
