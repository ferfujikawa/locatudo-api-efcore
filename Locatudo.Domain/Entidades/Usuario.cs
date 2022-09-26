using Locatudo.Shared.Entities;
using Locatudo.Shared.ValueObjects;

namespace Locatudo.Domain.Entidades
{
    public abstract class Usuario : BaseEntity
    {
        public Usuario(PersonName nome, Email email) : base()
        {
            Nome = nome;
            Email = email;
        }

        public PersonName Nome { get; private set; }
        public Email Email { get; private set; }
    }
}
