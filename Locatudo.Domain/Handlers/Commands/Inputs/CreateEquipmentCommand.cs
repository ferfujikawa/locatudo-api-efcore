using Flunt.Notifications;
using Locatudo.Domain.Handlers.Commands.Contracts;
using Locatudo.Shared.Handlers.Commands.Input;

namespace Locatudo.Domain.Handlers.Commands.Inputs
{
    public class CreateEquipmentCommand : Notifiable<Notification>, ICommand
    {
        public string Name { get; set; }

        public CreateEquipmentCommand()
        {
        }

        public CreateEquipmentCommand(string name)
        {
            Name = name;
        }

        public bool Validate()
        {
            AddNotifications(new CreateEquipmentCommandContract(this));

            return IsValid;
        }
    }
}
