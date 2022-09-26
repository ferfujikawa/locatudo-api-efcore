using Locatudo.Shared.ObjetosDeValor;

namespace Locatudo.Domain.Entidades
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
