using Flunt.Validations;
using Locatudo.Domain.Commands.Requests;
using Locatudo.Shared.Extensions;

namespace Locatudo.Domain.Commands.Contracts
{
    public class CreateEquipmentContract : Contract<CreateEquipmentRequest>
    {
        public CreateEquipmentContract(CreateEquipmentRequest command)
        {
            Requires()
                .HasMinLength(command.Name, 3, "Name", "O nome do equipamento precisa conter no mínimo 3 caracteres");
        }
    }
}
