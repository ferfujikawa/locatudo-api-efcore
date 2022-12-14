using Locatudo.Shared.Entities;
using Locatudo.Shared.ValueObjects;

namespace Locatudo.Domain.Entities
{
    public class User : BaseEntity
    {
        protected User() { }
        public User(PersonName name, Email email) : base()
        {
            Name = name;
            Email = email;
        }

        public PersonName Name { get; private set; }
        public Email Email { get; private set; }
    }
}
