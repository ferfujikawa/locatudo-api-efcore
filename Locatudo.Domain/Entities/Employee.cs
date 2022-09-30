using Locatudo.Shared.ValueObjects;

namespace Locatudo.Domain.Entities
{
    public class Employee : User
    {
        public Employee(PersonName name, Email email, Department departament) : base(name, email)
        {
            Departament = departament;
        }

        public Department Departament { get; private set; }

        public void ChangeDepartament(Department departament)
        {
            Departament = departament;
        }
    }
}
