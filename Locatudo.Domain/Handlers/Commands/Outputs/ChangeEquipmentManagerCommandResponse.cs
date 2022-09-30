using Locatudo.Shared.Handlers.Commands.Output;

namespace Locatudo.Domain.Handlers.Commands.Outputs
{
    public class ChangeEquipmentManagerCommandResponse : ICommandData
    {
        public Guid EquipmentId { get; set; }
        public string EquipmentName { get; set; }
        public Guid DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        public ChangeEquipmentManagerCommandResponse(Guid equipmentId, string equipmentName, Guid departmentId, string departmentName)
        {
            EquipmentId = equipmentId;
            EquipmentName = equipmentName;
            DepartmentId = departmentId;
            DepartmentName = departmentName;
        }
    }
}
