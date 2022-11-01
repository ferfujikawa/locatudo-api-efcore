using Locatudo.Shared.Commands.Requests;

namespace Locatudo.Domain.Commands.Requests
{
    public class CreateEquipmentRequest : ICommandRequest
    {
        public string Name { get; set; }

        public CreateEquipmentRequest()
        {
        }

        public CreateEquipmentRequest(string name)
        {
            Name = name;
        }
    }
}
