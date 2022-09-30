using Flunt.Notifications;
using Locatudo.Domain.Handlers.Commands.Contracts;
using Locatudo.Shared.Handlers.Commands.Input;

namespace Locatudo.Domain.Handlers.Commands.Inputs
{
    public class ApproveRentalCommand : Notifiable<Notification>, ICommand
    {
        public Guid RentalId { get; set; }
        public Guid AppraiserId { get; set; }

        public ApproveRentalCommand()
        {
        }

        public ApproveRentalCommand(Guid rentalId, Guid appraiserId)
        {
            RentalId = rentalId;
            AppraiserId = appraiserId;
        }

        public bool Validate()
        {
            AddNotifications(new ApproveRentalCommandContract(this));

            return IsValid;
        }
    }
}
