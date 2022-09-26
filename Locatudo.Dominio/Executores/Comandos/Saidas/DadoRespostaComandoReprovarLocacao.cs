﻿using Locatudo.Shared.Executores.Comandos.Saidas;

namespace Locatudo.Dominio.Executores.Comandos.Saidas
{
    public class DadoRespostaComandoReprovarLocacao : IDadoRespostaComandoExecutor
    {
        public Guid IdLocacao { get; set; }
        public Guid IdAprovador { get; set; }
        public string NomeAprovador { get; set; }
        public string Situacao { get; set; }

        public DadoRespostaComandoReprovarLocacao(Guid idLocacao, Guid idAprovador, string nomeAprovador, string situacao)
        {
            IdLocacao = idLocacao;
            IdAprovador = idAprovador;
            NomeAprovador = nomeAprovador;
            Situacao = situacao;
        }
    }
}
