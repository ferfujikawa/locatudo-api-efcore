using Locatudo.Shared.ValueObjects;

namespace Locatudo.Domain.Entities
{
    public class Employee : User
    {
        public Guid DepartmentId { get; private set; }
        private Department _department;
        public Department Department
        {
            get => _department;
            private set
            {
                _department = value;
                DepartmentId = value.Id;
            }
        }

        protected Employee() { }
        public Employee(PersonName name, Email email, Department department) : base(name, email)
        {
            Department = department;
        }

        public void ChangeDepartament(Department department)
        {
            Department = department;
        }
    }
}
