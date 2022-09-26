using Flunt.Validations;

namespace Locatudo.Shared.ValueObjects.Contracts
{
    public class EmailContract : Contract<Email>
    {
        public EmailContract(Email email)
        {
            Requires()
                .IsEmail(email.Address, "Email", "E-mail inválido");
        }
    }
}
