﻿using Flunt.Validations;
using Locatudo.Domain.Executores.Comandos.Entradas;
using Locatudo.Shared.Extensions;

namespace Locatudo.Domain.Executores.Comandos.Contratos
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
