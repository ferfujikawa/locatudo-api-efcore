using Locatudo.Compartilhado.Entidades;
using Locatudo.Compartilhado.ObjetosDeValor;

namespace Locatudo.Dominio.Entidades
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
