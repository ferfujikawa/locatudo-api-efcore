﻿using Flunt.Validations;
using Locatudo.Dominio.Executores.Comandos.Entradas;
using Locatudo.Shared.Extensoes;

namespace Locatudo.Dominio.Executores.Comandos.Contratos
{
    public class ContratoComandoCadastrarEquipamento : Contract<ComandoCadastrarEquipamento>
    {
        public ContratoComandoCadastrarEquipamento(ComandoCadastrarEquipamento comando)
        {
            Requires()
                .HasMinLength(comando.Nome, 3, "Nome", "O nome do equipamento precisa conter no mínimo 3 caracteres");
        }
    }
}
