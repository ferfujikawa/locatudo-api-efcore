using Locatudo.Shared.Entities;

namespace Locatudo.Domain.Entidades
{
    public class Equipamento : BaseEntity
    {
        public Equipamento(string nome) : base()
        {
            Nome = nome;
        }

        public string Nome { get; private set; }
        public Departamento? Gerenciador { get; private set; }

        public void AlterarGerenciador(Departamento gerenciador)
        {
            Gerenciador = gerenciador;
        }
    }
}
