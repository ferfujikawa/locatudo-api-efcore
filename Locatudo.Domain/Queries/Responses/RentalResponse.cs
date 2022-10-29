using Locatudo.Shared.Queries.Responses;

namespace Locatudo.Domain.Queries.Responses
{
    public class RentalResponse : IQueryData
    {
        public Guid Id { get; set; }
        public Guid EquipmentId { get; set; }
        public string EquipmentName { get; set; }
        public string Status { get; set; }
        public DateTime Time { get; set; }
    }
}
