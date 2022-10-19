namespace Locatudo.Domain.Entities.Dtos
{
    public class EquipmentDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? ManagerId { get; set; }
        public string? ManagerName { get; set; }
    }
}
