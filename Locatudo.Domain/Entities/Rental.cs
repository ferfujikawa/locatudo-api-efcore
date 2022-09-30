using Locatudo.Shared.Entities;
using Locatudo.Shared.ValueObjects;

namespace Locatudo.Domain.Entities
{
    public class Rental : BaseEntity
    {
        public Rental(Equipment equipament, User tenant, RentalTime time)
        {
            Equipament = equipament;
            Tenant = tenant;
            Status = new RentalStatus();
            Time = time;
        }

        public Equipment Equipament { get; private set; }
        public User Tenant { get; private set; }
        public Employee? Appraiser { get; private set; }
        public RentalStatus Status { get; private set; }
        public RentalTime Time { get; private set; }

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
            return employee.Departament.Id == Equipament.Manager?.Id;
        }
    }
}
