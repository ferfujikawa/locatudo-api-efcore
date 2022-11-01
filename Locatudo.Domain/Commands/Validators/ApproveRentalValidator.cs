using FluentValidation;
using Locatudo.Domain.Commands.Requests;

namespace Locatudo.Domain.Commands.Validators
{
    public class ApproveRentalValidator : AbstractValidator<ApproveRentalRequest>
    {
        public ApproveRentalValidator()
        {
            RuleFor(x => x.RentalId).NotEmpty().WithMessage("É necessário informar o Id da locação que se pretende aprovar");
            RuleFor(x => x.AppraiserId).NotEmpty().WithMessage("É necessário informar o Id do funcionário que está aprovando a locacação");
        }
    }
}
