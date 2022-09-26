using Locatudo.Shared.Entities;
using Locatudo.Shared.ValueObjects;

namespace Locatudo.Domain.Entidades
{
    public class Locacao : BaseEntity
    {
        public Locacao(Equipamento equipamento, Usuario locatario, RentalTime horario)
        {
            Equipamento = equipamento;
            Locatario = locatario;
            Situacao = new RentalStatus();
            Horario = horario;
        }

        public Equipamento Equipamento { get; private set; }
        public Usuario Locatario { get; private set; }
        public Funcionario? Aprovador { get; private set; }
        public RentalStatus Situacao { get; private set; }
        public RentalTime Horario { get; private set; }

        public bool Aprovar(Funcionario aprovador)
        {
            if (Situacao.Approve())
            {
                Aprovador = aprovador;
                return true;
            }
            return false;
        }
        public bool Reprovar(Funcionario aprovador)
        {
            if (Situacao.Disapprove())
            {
                Aprovador = aprovador;
                return true;
            }
            return false;
        }

        public bool Cancelar()
        {
            return Situacao.Cancel();
        }

        public bool PodeSerAprovadaReprovadaPor(Funcionario funcionario)
        {
            return funcionario.Lotacao.Id == Equipamento.Gerenciador?.Id;
        }
    }
}
