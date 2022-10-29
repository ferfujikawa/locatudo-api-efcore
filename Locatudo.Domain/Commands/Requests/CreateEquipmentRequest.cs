using Flunt.Notifications;
using Locatudo.Domain.Commands.Contracts;
using Locatudo.Shared.Commands.Requests;

namespace Locatudo.Domain.Commands.Requests
{
    public class CreateEquipmentRequest : Notifiable<Notification>, ICommandRequest
    {
        public string Name { get; set; }

        public CreateEquipmentRequest()
        {
        }

        public CreateEquipmentRequest(string name)
        {
            Name = name;
        }

        public bool Validate()
        {
            AddNotifications(new CreateEquipmentContract(this));

            return IsValid;
        }
    }
}
