using Locatudo.Compartilhado.Entidades;
using Locatudo.Compartilhado.ObjetosDeValor;

namespace Locatudo.Dominio.Entidades
{
    public class Departamento : EntidadeBase
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
