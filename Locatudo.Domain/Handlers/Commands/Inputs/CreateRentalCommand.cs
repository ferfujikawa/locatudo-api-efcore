using Flunt.Notifications;
using Locatudo.Domain.Handlers.Commands.Contracts;
using Locatudo.Shared.Handlers.Commands.Input;

namespace Locatudo.Domain.Handlers.Commands.Inputs
{
    public class CreateRentalCommand : Notifiable<Notification>, ICommand
    {
        public Guid EquipmentId { get; set; }
        public Guid TenantId { get; set; }
        public DateTime Start { get; set; }

        public CreateRentalCommand()
        {
        }

        public CreateRentalCommand(Guid equipmentId, Guid tenantId, DateTime start)
        {
            EquipmentId = equipmentId;
            TenantId = tenantId;
            Start = start;
        }

        public bool Validate()
        {
            AddNotifications(new CreateRentalCommandContract(this));

            return IsValid;
        }
    }
}
