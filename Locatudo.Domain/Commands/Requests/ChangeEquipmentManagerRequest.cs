using Flunt.Notifications;
using Locatudo.Domain.Commands.Contracts;
using Locatudo.Shared.Commands.Requests;

namespace Locatudo.Domain.Commands.Requests
{
    public class ChangeEquipmentManagerRequest : Notifiable<Notification>, ICommandRequest
    {
        public Guid EquipmentId { get; set; }
        public Guid DepartmentId { get; set; }

        public ChangeEquipmentManagerRequest()
        {
        }

        public ChangeEquipmentManagerRequest(Guid id, Guid departmentId)
        {
            EquipmentId = id;
            DepartmentId = departmentId;
        }

        public bool Validate()
        {
            AddNotifications(new ChangeEquipmentManagerContract(this));

            return IsValid;
        }
    }
}
