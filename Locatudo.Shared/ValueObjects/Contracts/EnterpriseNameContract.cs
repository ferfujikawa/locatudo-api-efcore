using Flunt.Validations;

namespace Locatudo.Shared.ValueObjects.Contracts
{
    public class EnterpriseNameContract : Contract<EnterpriseName>
    {
        public EnterpriseNameContract(EnterpriseName enterpriseName)
        {
            Requires()
                .IsNotNullOrEmpty(enterpriseName.CompanyName, "CompanyName", "Razão social não deve ser nulo ou vazio")
                .IsGreaterOrEqualsThan(enterpriseName.CompanyName.Length, 3, "CompanyName", "Razão Social deve possuir 3 ou mais caracteres");
        }
    }
}
