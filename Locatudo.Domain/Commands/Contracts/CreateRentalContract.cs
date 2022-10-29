using Flunt.Validations;
using Locatudo.Domain.Commands.Requests;

namespace Locatudo.Domain.Commands.Contracts
{
    public class CreateRentalContract : Contract<CreateRentalRequest>
    {
        public CreateRentalContract(CreateRentalRequest command)
        {
            var inicio = new DateTime(command.Start.Year, command.Start.Month, command.Start.Day, command.Start.Hour, 0, 0);

            Requires()
                .AreNotEquals(command.EquipmentId, default, "EquipmentId", "É necessário informar o Id do equipamento que está sendo locado")
                .AreNotEquals(command.TenantId, default, "TenantId", "É necessário informar o Id da usuário que está locando o equipamento")
                .AreNotEquals(command.Start, new DateTime(), "Start", "É necessário informar o horário de início da locação")
                .IsGreaterThan(inicio, DateTime.Now, "Start", "Início da locação não pode ser no passado");
        }
    }
}
