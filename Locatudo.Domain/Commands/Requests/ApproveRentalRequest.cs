using Locatudo.Shared.Commands.Requests;

namespace Locatudo.Domain.Commands.Requests
{
    public class ApproveRentalRequest : ICommandRequest
    {
        public Guid RentalId { get; set; }
        public Guid AppraiserId { get; set; }

        public ApproveRentalRequest()
        {
        }

        public ApproveRentalRequest(Guid rentalId, Guid appraiserId)
        {
            RentalId = rentalId;
            AppraiserId = appraiserId;
        }

        public bool Validate()
        {
            return true;
        }
    }
}
