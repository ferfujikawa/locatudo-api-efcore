using FluentValidation;
using Locatudo.Domain.Commands.Requests;

namespace Locatudo.Domain.Commands.Validators
{
    public class CreateEquipmentValidator : AbstractValidator<CreateEquipmentRequest>, ICommandValidator<CreateEquipmentRequest>
    {
        public CreateEquipmentValidator()
        {
            RuleFor(x => x.Name).MinimumLength(3).WithMessage("O nome do equipamento precisa conter no mínimo 3 caracteres");
        }
    }
}
