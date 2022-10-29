using Flunt.Notifications;
using Locatudo.Domain.Commands.Contracts;
using Locatudo.Shared.Commands.Requests;

namespace Locatudo.Domain.Commands.Requests
{
    public class ApproveRentalRequest : Notifiable<Notification>, ICommandRequest
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
            AddNotifications(new ApproveRentalContract(this));

            return IsValid;
        }
    }
}
