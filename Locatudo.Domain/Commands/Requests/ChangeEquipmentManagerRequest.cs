using Locatudo.Shared.Commands.Requests;

namespace Locatudo.Domain.Commands.Requests
{
    public class ChangeEquipmentManagerRequest : ICommandRequest
    {
        public Guid EquipmentId { get; set; }
        public Guid DepartmentId { get; set; }

        public ChangeEquipmentManagerRequest()
        {
        }

        public ChangeEquipmentManagerRequest(Guid id, Guid departmentId)
        {
            EquipmentId = id;
            DepartmentId = departmentId;
        }
    }
}
