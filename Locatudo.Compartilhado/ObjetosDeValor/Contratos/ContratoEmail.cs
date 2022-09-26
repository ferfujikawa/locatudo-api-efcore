using Flunt.Validations;

namespace Locatudo.Compartilhado.ObjetosDeValor.Contratos
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
