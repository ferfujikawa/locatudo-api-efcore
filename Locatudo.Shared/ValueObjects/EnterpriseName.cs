namespace Locatudo.Shared.ValueObjects
{
    public class EnterpriseName
    {
        public EnterpriseName(string companyName)
        {
            CompanyName = companyName;
        }

        public string CompanyName { get; private set; }
    }
}
