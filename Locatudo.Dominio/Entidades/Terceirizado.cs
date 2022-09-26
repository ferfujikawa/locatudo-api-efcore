using Locatudo.Compartilhado.ObjetosDeValor;

namespace Locatudo.Dominio.Entidades
{
    public class Terceirizado : Usuario
    {
        public Terceirizado(NomePessoaFisica nome, Email email, NomePessoaJuridica empresa) : base(nome, email)
        {
            Empresa = empresa;
        }

        public NomePessoaJuridica Empresa { get; private set; }
    }
}
