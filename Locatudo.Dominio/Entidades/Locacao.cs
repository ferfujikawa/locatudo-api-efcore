using Locatudo.Compartilhado.Entidades;
using Locatudo.Compartilhado.ObjetosDeValor;

namespace Locatudo.Dominio.Entidades
{
    public class Locacao : EntidadeBase
    {
        public Locacao(Equipamento equipamento, Usuario locatario, HorarioLocacao horario)
        {
            Equipamento = equipamento;
            Locatario = locatario;
            Situacao = new SituacaoLocacao();
            Horario = horario;
        }

        public Equipamento Equipamento { get; private set; }
        public Usuario Locatario { get; private set; }
        public Funcionario? Aprovador { get; private set; }
        public SituacaoLocacao Situacao { get; private set; }
        public HorarioLocacao Horario { get; private set; }

        public bool Aprovar(Funcionario aprovador)
        {
            if (Situacao.AlterarParaAprovado())
            {
                Aprovador = aprovador;
                return true;
            }
            return false;
        }
        public bool Reprovar(Funcionario aprovador)
        {
            if (Situacao.AlterarParaReprovado())
            {
                Aprovador = aprovador;
                return true;
            }
            return false;
        }

        public bool Cancelar()
        {
            return Situacao.AlterarParaCancelado();
        }

        public bool PodeSerAprovadaReprovadaPor(Funcionario funcionario)
        {
            return funcionario.Lotacao.Id == Equipamento.Gerenciador?.Id;
        }
    }
}
