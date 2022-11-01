using FluentValidation;
using Locatudo.Domain.Commands.Requests;

namespace Locatudo.Domain.Commands.Validators
{
    public class DisapproveRentalValidator : AbstractValidator<DisapproveRentalRequest>, ICommandValidator<DisapproveRentalRequest>
    {
        public DisapproveRentalValidator()
        {
            RuleFor(x => x.RentalId).NotEmpty().WithMessage("É necessário informar o Id da locação que se pretende reprovar");
            RuleFor(x => x.AppraiserId).NotEmpty().WithMessage("É necessário informar o Id do funcionário que está reprovando a locacação");
        }
    }
}
