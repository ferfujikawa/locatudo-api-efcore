using Flunt.Validations;
using Locatudo.Domain.Commands.Requests;

namespace Locatudo.Domain.Commands.Contracts
{
    public class CreateRentalContract : Contract<CreateRentalRequest>
    {
        public CreateRentalContract(CreateRentalRequest request)
        {
            var inicio = new DateTime(request.Start.Year, request.Start.Month, request.Start.Day, request.Start.Hour, 0, 0);

            Requires()
                .AreNotEquals(request.EquipmentId, default, "EquipmentId", "É necessário informar o Id do equipamento que está sendo locado")
                .AreNotEquals(request.TenantId, default, "TenantId", "É necessário informar o Id da usuário que está locando o equipamento")
                .AreNotEquals(request.Start, new DateTime(), "Start", "É necessário informar o horário de início da locação")
                .IsGreaterThan(inicio, DateTime.Now, "Start", "Início da locação não pode ser no passado");
        }
    }
}
