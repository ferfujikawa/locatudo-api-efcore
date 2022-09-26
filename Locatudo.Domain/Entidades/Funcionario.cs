using Locatudo.Shared.ValueObjects;

namespace Locatudo.Domain.Entidades
{
    public class Funcionario : Usuario
    {
        public Funcionario(PersonName nome, Email email, Departamento lotacao) : base(nome, email)
        {
            Lotacao = lotacao;
        }

        public Departamento Lotacao { get; private set; }

        public void AlterarLotacao(Departamento departamento)
        {
            Lotacao = departamento;
        }
    }
}
