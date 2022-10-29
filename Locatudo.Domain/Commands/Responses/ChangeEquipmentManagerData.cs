using Locatudo.Shared.Commands.Responses;

namespace Locatudo.Domain.Commands.Responses
{
    public class ChangeEquipmentManagerData : ICommandData
    {
        public Guid EquipmentId { get; set; }
        public string EquipmentName { get; set; }
        public Guid DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        public ChangeEquipmentManagerData(Guid equipmentId, string equipmentName, Guid departmentId, string departmentName)
        {
            EquipmentId = equipmentId;
            EquipmentName = equipmentName;
            DepartmentId = departmentId;
            DepartmentName = departmentName;
        }
    }
}
