using Flunt.Notifications;
using Locatudo.Domain.Commands.Contracts;
using Locatudo.Shared.Commands.Requests;

namespace Locatudo.Domain.Commands.Requests
{
    public class DisapproveRentalRequest : Notifiable<Notification>, ICommandRequest
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

        public bool Validate()
        {
            AddNotifications(new DisapproveRentalContract(this));

            return IsValid;
        }
    }
}
