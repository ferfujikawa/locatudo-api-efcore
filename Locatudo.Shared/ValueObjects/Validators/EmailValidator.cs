using FluentValidation;

namespace Locatudo.Shared.ValueObjects.Validators
{
    public class EmailContract : AbstractValidator<Email>
    {
        public EmailContract()
        {
            RuleFor(x => x.Address).EmailAddress().WithMessage("E-mail inválido");
        }
    }
}
