using Flunt.Validations;
using Locatudo.Shared.ObjetosDeValor;

namespace Locatudo.Shared.ObjetosDeValor.Contratos
{
    public class ContratoEmail : Contract<Email>
    {
        public ContratoEmail(Email email)
        {
            Requires()
                .IsEmail(email.Endereco, "Email", "E-mail inválido");
        }
    }
}
