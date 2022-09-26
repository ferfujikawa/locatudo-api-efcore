﻿using Flunt.Validations;
using Locatudo.Domain.Executores.Comandos.Entradas;

namespace Locatudo.Domain.Executores.Comandos.Contratos
{
    public class ContratoComandoCadastrarLocacao : Contract<ComandoCadastrarLocacao>
    {
        public ContratoComandoCadastrarLocacao(ComandoCadastrarLocacao comando)
        {
            var inicio = new DateTime(comando.Inicio.Year, comando.Inicio.Month, comando.Inicio.Day, comando.Inicio.Hour, 0, 0);

            Requires()
                .AreNotEquals(comando.IdEquipamento, default, "IdEquipamento", "É necessário informar o IdEquipamento do equipamento que está sendo locado")
                .AreNotEquals(comando.IdLocatario, default, "IdLocatario", "É necessário informar o IdEquipamento da usuário que está locando o equipamento")
                .AreNotEquals(comando.Inicio, new DateTime(), "Start", "É necessário informar o horário de início da locação")
                .IsGreaterThan(inicio, DateTime.Now, "Start", "Início da locação não pode ser no passado");
        }
    }
}
