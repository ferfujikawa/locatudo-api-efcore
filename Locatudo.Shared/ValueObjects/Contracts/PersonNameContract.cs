using Flunt.Validations;

namespace Locatudo.Shared.ValueObjects.Contracts
{
    public class PersonNameContract : Contract<PersonName>
    {
        public PersonNameContract(PersonName personName)
        {
            Requires()
                .IsNotNullOrEmpty(personName.FirstName, "FirstName", "Nome não deve ser nulo ou vazio")
                .IsGreaterOrEqualsThan(personName.FirstName.Length, 3, "FirstName", "Nome deve possuir 3 ou mais caracteres")
                .IsNotNullOrEmpty(personName.LastName, "LastName", "Sobrenome não deve ser nulo ou vazio")
                .IsGreaterOrEqualsThan(personName.LastName.Length, 3, "LastName", "Sobrenome deve possuir 3 ou mais caracteres");
        }
    }
}
