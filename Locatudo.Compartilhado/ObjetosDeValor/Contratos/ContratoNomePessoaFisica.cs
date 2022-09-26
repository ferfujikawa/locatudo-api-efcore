using Flunt.Validations;

namespace Locatudo.Compartilhado.ObjetosDeValor.Contratos
{
    public class ContratoNomePessoaFisica : Contract<NomePessoaFisica>
    {
        public ContratoNomePessoaFisica(NomePessoaFisica nomePessoaFisica)
        {
            Requires()
                .IsNotNullOrEmpty(nomePessoaFisica.PrimeiroNome, "PrimeiroNome", "Nome não deve ser nulo ou vazio")
                .IsGreaterOrEqualsThan(nomePessoaFisica.PrimeiroNome.Length, 3, "PrimeiroNome", "Nome deve possuir 3 ou mais caracteres")
                .IsNotNullOrEmpty(nomePessoaFisica.Sobrenome, "Sobrenome", "Sobrenome não deve ser nulo ou vazio")
                .IsGreaterOrEqualsThan(nomePessoaFisica.Sobrenome.Length, 3, "Sobrenome", "Sobrenome deve possuir 3 ou mais caracteres");
        }
    }
}
