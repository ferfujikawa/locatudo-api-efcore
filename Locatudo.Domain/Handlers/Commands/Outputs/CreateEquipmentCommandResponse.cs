using Locatudo.Shared.Handlers.Commands.Output;

namespace Locatudo.Domain.Handlers.Commands.Outputs
{
    public class CreateEquipmentCommandResponse : ICommandData
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public CreateEquipmentCommandResponse(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
