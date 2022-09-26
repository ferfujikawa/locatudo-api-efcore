using Locatudo.Compartilhado.ObjetosDeValor;

namespace Locatudo.Dominio.Entidades
{
    public class Funcionario : Usuario
    {
        public Funcionario(NomePessoaFisica nome, Email email, Departamento lotacao) : base(nome, email)
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
