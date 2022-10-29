using Locatudo.Shared.Commands.Responses;

namespace Locatudo.Domain.Commands.Responses
{
    public class CreateRentalData : ICommandData
    {
        public Guid Id { get; set; }
        public Guid EquipmentId { get; set; }
        public string EquipmentName { get; set; }
        public Guid TenantId { get; set; }
        public string TenantName { get; set; }
        public DateTime Start { get; set; }
        public string Status { get; set; }

        public CreateRentalData(Guid rentalId, Guid equipmentId, string equipmentName, Guid tenantId, string tenantName, DateTime start, string status)
        {
            Id = rentalId;
            EquipmentId = equipmentId;
            EquipmentName = equipmentName;
            TenantId = tenantId;
            TenantName = tenantName;
            Start = start;
            Status = status;
        }
    }
}
