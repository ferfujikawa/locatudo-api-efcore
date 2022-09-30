using Flunt.Validations;
using Locatudo.Domain.Handlers.Commands.Inputs;
using Locatudo.Shared.Extensions;

namespace Locatudo.Domain.Handlers.Commands.Contracts
{
    public class CreateEquipmentCommandContract : Contract<CreateEquipmentCommand>
    {
        public CreateEquipmentCommandContract(CreateEquipmentCommand command)
        {
            Requires()
                .HasMinLength(command.Name, 3, "Name", "O nome do equipamento precisa conter no mínimo 3 caracteres");
        }
    }
}
