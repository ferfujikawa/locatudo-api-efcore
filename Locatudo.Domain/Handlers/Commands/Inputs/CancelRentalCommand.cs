using Flunt.Notifications;
using Locatudo.Domain.Handlers.Commands.Contracts;
using Locatudo.Shared.Handlers.Commands.Input;

namespace Locatudo.Domain.Handlers.Commands.Inputs
{
    public class CancelRentalCommand : Notifiable<Notification>, ICommand
    {
        public Guid RentalId { get; set; }

        public CancelRentalCommand()
        {
        }

        public CancelRentalCommand(Guid rentalId)
        {
            RentalId = rentalId;
        }

        public bool Validate()
        {
            AddNotifications(new CancelRentalCommandContract(this));

            return IsValid;
        }
    }
}
