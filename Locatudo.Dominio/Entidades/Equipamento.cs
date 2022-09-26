using Locatudo.Compartilhado.Entidades;

namespace Locatudo.Dominio.Entidades
{
    public class Equipamento : EntidadeBase
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
