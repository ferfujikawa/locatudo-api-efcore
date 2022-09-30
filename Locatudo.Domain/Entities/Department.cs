using Locatudo.Shared.Entities;
using Locatudo.Shared.ValueObjects;

namespace Locatudo.Domain.Entities
{
    public class Department : BaseEntity
    {
        public Department(string name, Email email) : base()
        {
            Name = name;
            Email = email;
        }

        public string Name { get; private set; }
        public Email Email { get; private set; }
    }
}
