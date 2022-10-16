using Locatudo.Shared.Entities;
using Locatudo.Shared.ValueObjects;

namespace Locatudo.Domain.Entities
{
    public class Rental : BaseEntity
    {
        public Guid EquipmentId { get; private set; }
        private Equipment _equipment;
        public Equipment Equipment {
            get => _equipment;
            private set
            {
                _equipment = value;
                EquipmentId = value.Id;
            }
        }
        public Guid TenantId { get; private set; }
        private User _tenant;
        public User Tenant {
            get => _tenant;
            private set
            {
                _tenant = value;
                TenantId = value.Id;
            }
        }
        public Guid? AppraiserId { get; private set; }
        private Employee? _appraiser;
        public Employee? Appraiser {
            get => _appraiser;
            private set
            {
                _appraiser = value;
                AppraiserId = value?.Id;
            }
        }
        public RentalStatus Status { get; private set; }
        public RentalTime Time { get; private set; }

        protected Rental() { }
        public Rental(Equipment equipament, User tenant, RentalTime time)
        {
            Equipment = equipament;
            Tenant = tenant;
            Status = new RentalStatus();
            Time = time;
        }

        public bool Approve(Employee appraiser)
        {
            if (Status.Approve())
            {
                Appraiser = appraiser;
                return true;
            }
            return false;
        }

        public bool Disapprove(Employee appraiser)
        {
            if (Status.Disapprove())
            {
                Appraiser = appraiser;
                return true;
            }
            return false;
        }

        public bool Cancel()
        {
            return Status.Cancel();
        }

        public bool CanBeEvaluatedBy(Employee employee)
        {
            return employee.DepartmentId == Equipment.ManagerId;
        }
    }
}
