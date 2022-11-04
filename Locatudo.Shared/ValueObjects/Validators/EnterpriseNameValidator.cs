using FluentValidation;

namespace Locatudo.Shared.ValueObjects.Validators
{
    public class EnterpriseNameContract : AbstractValidator<EnterpriseName>
    {
        public EnterpriseNameContract()
        {
            RuleFor(x => x.CompanyName)
                .NotEmpty().WithMessage("Razão social não deve ser nulo ou vazio")
                .MinimumLength(3).WithMessage("Razão Social deve possuir 3 ou mais caracteres");
        }
    }
}
