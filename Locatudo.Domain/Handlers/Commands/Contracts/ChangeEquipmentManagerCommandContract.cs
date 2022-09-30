using Flunt.Validations;
using Locatudo.Domain.Handlers.Commands.Inputs;

namespace Locatudo.Domain.Handlers.Commands.Contracts;

public class ChangeEquipmentManagerCommandContract : Contract<ChangeEquipmentManagerCommand>
{
    public ChangeEquipmentManagerCommandContract(ChangeEquipmentManagerCommand command)
    {
        Requires()
            .AreNotEquals(command.EquipmentId, default, "EquipmentId", "É necessário informar o Id do equipamento para alterar seu gerenciador")
            .AreNotEquals(command.DepartmentId, default, "DepartmentId", "É necessário informar o Id do novo gerenciador do equipamento");
    }
}
