using FluentValidation;
using Locatudo.Domain.Commands.Requests;

namespace Locatudo.Domain.Commands.Validators
{
    public class ChangeEquipmentManagerValidator : AbstractValidator<ChangeEquipmentManagerRequest>, ICommandValidator<ChangeEquipmentManagerRequest>
    {
        public ChangeEquipmentManagerValidator()
        {
            RuleFor(x => x.EquipmentId).NotEmpty().WithMessage("É necessário informar o Id do equipamento para alterar seu gerenciador");
            RuleFor(x => x.DepartmentId).NotEmpty().WithMessage("É necessário informar o Id do novo gerenciador do equipamento");
        }
    }
}
