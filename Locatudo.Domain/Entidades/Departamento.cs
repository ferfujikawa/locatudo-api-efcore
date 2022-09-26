using Locatudo.Shared.Entities;
using Locatudo.Shared.ValueObjects;

namespace Locatudo.Domain.Entidades
{
    public class Departamento : BaseEntity
    {
        public Departamento(string nome, Email email) : base()
        {
            Nome = nome;
            Email = email;
        }

        public string Nome { get; private set; }
        public Email Email { get; private set; }
    }
}
