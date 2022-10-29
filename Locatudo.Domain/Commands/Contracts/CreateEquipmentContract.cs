using Flunt.Validations;
using Locatudo.Domain.Commands.Requests;
using Locatudo.Shared.Extensions;

namespace Locatudo.Domain.Commands.Contracts
{
    public class CreateEquipmentContract : Contract<CreateEquipmentRequest>
    {
        public CreateEquipmentContract(CreateEquipmentRequest request)
        {
            Requires()
                .HasMinLength(request.Name, 3, "Name", "O nome do equipamento precisa conter no mínimo 3 caracteres");
        }
    }
}
