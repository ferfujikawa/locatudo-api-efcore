using Flunt.Validations;
using Locatudo.Shared.ObjetosDeValor;

namespace Locatudo.Shared.ObjetosDeValor.Contratos
{
    public class ContratoNomePessoaJuridica : Contract<NomePessoaJuridica>
    {
        public ContratoNomePessoaJuridica(NomePessoaJuridica nomePessoaJuridica)
        {
            Requires()
                .IsNotNullOrEmpty(nomePessoaJuridica.RazaoSocial, "RazaoSocial", "Nome não deve ser nulo ou vazio")
                .IsGreaterOrEqualsThan(nomePessoaJuridica.RazaoSocial.Length, 3, "RazaoSocial", "Nome deve possuir 3 ou mais caracteres");
        }
    }
}
