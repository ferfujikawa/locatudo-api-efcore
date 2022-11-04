using FluentValidation;

namespace Locatudo.Shared.ValueObjects.Validators
{
    public class PersonNameValidator : AbstractValidator<PersonName>
    {
        public PersonNameValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Nome não deve ser nulo ou vazio")
                .MinimumLength(3).WithMessage("Nome deve possuir 3 ou mais caracteres");
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Sobrenome não deve ser nulo ou vazio")
                .MinimumLength(3).WithMessage("Sobrenome deve possuir 3 ou mais caracteres");
        }
    }
}
