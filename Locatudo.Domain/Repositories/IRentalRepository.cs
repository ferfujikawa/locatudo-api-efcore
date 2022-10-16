using Locatudo.Domain.Entities;
using Locatudo.Shared.Repositories;
using Locatudo.Shared.ValueObjects;

namespace Locatudo.Domain.Repositories
{
    public interface IRentalRepository : IRepository<Rental>
    {
        bool CheckAvailability(Guid equipmentId, RentalTime start);
        Rental? GetByIdIncludingEquipment(Guid id);
    }
}
