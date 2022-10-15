namespace Locatudo.Domain.Entities.Dtos
{
    public class RentalDto
    {
        public Guid Id { get; set; }
        public Guid EquipmentId { get; set; }
        public string EquipmentName { get; set; }
        public string Status { get; set; }
        public DateTime Time { get; set; }
    }
}
