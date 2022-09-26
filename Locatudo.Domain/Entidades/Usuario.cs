using Locatudo.Shared.Entidades;
using Locatudo.Shared.ObjetosDeValor;

namespace Locatudo.Domain.Entidades
{
    public abstract class Usuario : EntidadeBase
    {
        public Usuario(NomePessoaFisica nome, Email email) : base()
        {
            Nome = nome;
            Email = email;
        }

        public NomePessoaFisica Nome { get; private set; }
        public Email Email { get; private set; }
    }
}
