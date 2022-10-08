using Locatudo.Shared.ValueObjects;

namespace Locatudo.Domain.Entities
{
    public class Employee : User
    {
        public Department Departament { get; private set; }

        protected Employee() { }
        public Employee(PersonName name, Email email, Department departament) : base(name, email)
        {
            Departament = departament;
        }

        public void ChangeDepartament(Department departament)
        {
            Departament = departament;
        }
    }
}
