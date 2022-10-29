using Locatudo.Shared.Commands.Responses;

namespace Locatudo.Domain.Commands.Responses
{
    public class CreateEquipment : ICommandData
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public CreateEquipment(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
