﻿using Locatudo.Shared.Handlers.Commands.Output;

namespace Locatudo.Domain.Executores.Comandos.Saidas
{
    public class DadoRespostaComandoAlterarGerenciadorEquipamento : IHandlerCommandData
    {
        public Guid IdEquipamento { get; set; }
        public string Nome { get; set; }
        public Guid IdDepartamento { get; set; }
        public string NomeDepartamento { get; set; }

        public DadoRespostaComandoAlterarGerenciadorEquipamento(Guid idEquipamento, string nome, Guid idDepartamento, string nomeDepartamento)
        {
            IdEquipamento = idEquipamento;
            Nome = nome;
            IdDepartamento = idDepartamento;
            NomeDepartamento = nomeDepartamento;
        }
    }
}
