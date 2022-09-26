using Locatudo.Shared.ValueObjects;

namespace Locatudo.Domain.Entidades
{
    public class Terceirizado : Usuario
    {
        public Terceirizado(PersonName nome, Email email, EnterpriseName empresa) : base(nome, email)
        {
            Empresa = empresa;
        }

        public EnterpriseName Empresa { get; private set; }
    }
}
