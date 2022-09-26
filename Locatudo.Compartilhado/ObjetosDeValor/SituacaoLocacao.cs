using Locatudo.Compartilhado.Enumeradores;

namespace Locatudo.Compartilhado.ObjetosDeValor
{
    public class SituacaoLocacao
    {
        public ESituacaoLocacao Valor { get; private set; }
        public SituacaoLocacao()
        {
            Valor = ESituacaoLocacao.Solicitado;
        }

        public bool AlterarParaAprovado()
        {
            if (Valor == ESituacaoLocacao.Solicitado)
            {
                Valor = ESituacaoLocacao.Aprovado;
                return true;
            }
            return false;
        }
        public bool AlterarParaReprovado()
        {
            if (Valor == ESituacaoLocacao.Solicitado)
            {
                Valor = ESituacaoLocacao.Reprovado;
                return true;
            }
            return false;
        }

        public bool AlterarParaCancelado()
        {
            ESituacaoLocacao[] situacoesPossiveis = { ESituacaoLocacao.Solicitado, ESituacaoLocacao.Aprovado };
            if (situacoesPossiveis.Contains(Valor))
            {
                Valor = ESituacaoLocacao.Cancelado;
                return true;
            }
            return false;
        }
    }
}
