using Flunt.Notifications;
using Locatudo.Domain.Handlers.Commands.Contracts;
using Locatudo.Shared.Handlers.Commands.Input;

namespace Locatudo.Domain.Handlers.Commands.Inputs
{
    public class ChangeEquipmentManagerCommand : Notifiable<Notification>, ICommand
    {
        public Guid EquipmentId { get; set; }
        public Guid DepartmentId { get; set; }

        public ChangeEquipmentManagerCommand()
        {
        }

        public ChangeEquipmentManagerCommand(Guid id, Guid departmentId)
        {
            EquipmentId = id;
            DepartmentId = departmentId;
        }

        public bool Validate()
        {
            AddNotifications(new ChangeEquipmentManagerCommandContract(this));

            return IsValid;
        }
    }
}
