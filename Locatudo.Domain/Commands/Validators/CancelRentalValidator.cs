using FluentValidation;
using Locatudo.Domain.Commands.Requests;

namespace Locatudo.Domain.Commands.Validators
{
    public class CancelRentalValidator : AbstractValidator<CancelRentalRequest>, ICommandValidator<CancelRentalRequest>
    {
        public CancelRentalValidator()
        {
            RuleFor(x => x.RentalId).NotEmpty().WithMessage("É necessário informar o Id da locação que se pretende cancelar");
        }
    }
}
