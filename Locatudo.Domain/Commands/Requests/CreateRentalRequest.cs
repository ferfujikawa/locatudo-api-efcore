using Flunt.Notifications;
using Locatudo.Domain.Commands.Contracts;
using Locatudo.Shared.Commands.Requests;

namespace Locatudo.Domain.Commands.Requests
{
    public class CreateRentalRequest : Notifiable<Notification>, ICommandRequest
    {
        public Guid EquipmentId { get; set; }
        public Guid TenantId { get; set; }
        public DateTime Start { get; set; }

        public CreateRentalRequest()
        {
        }

        public CreateRentalRequest(Guid equipmentId, Guid tenantId, DateTime start)
        {
            EquipmentId = equipmentId;
            TenantId = tenantId;
            Start = start;
        }

        public bool Validate()
        {
            AddNotifications(new CreateRentalContract(this));

            return IsValid;
        }
    }
}
