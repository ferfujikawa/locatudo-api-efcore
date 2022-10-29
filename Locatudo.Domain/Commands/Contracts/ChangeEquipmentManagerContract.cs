﻿using Flunt.Validations;
using Locatudo.Domain.Commands.Requests;

namespace Locatudo.Domain.Commands.Contracts;

public class ChangeEquipmentManagerContract : Contract<ChangeEquipmentManagerRequest>
{
    public ChangeEquipmentManagerContract(ChangeEquipmentManagerRequest command)
    {
        Requires()
            .AreNotEquals(command.EquipmentId, default, "EquipmentId", "É necessário informar o Id do equipamento para alterar seu gerenciador")
            .AreNotEquals(command.DepartmentId, default, "DepartmentId", "É necessário informar o Id do novo gerenciador do equipamento");
    }
}