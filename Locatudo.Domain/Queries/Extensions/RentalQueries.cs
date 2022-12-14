using Locatudo.Domain.Entities;

namespace Locatudo.Domain.Queries.Extensions
{
    public static class RentalQueries
    {
        public static IQueryable<Rental> FilterByEquipmentIdAndTime(this IQueryable<Rental> query, Guid equipmentId, DateTime time)
        {
            return query.Where(x => x.Equipment.Id == equipmentId && x.Time.Start == time);
        }
    }
}
