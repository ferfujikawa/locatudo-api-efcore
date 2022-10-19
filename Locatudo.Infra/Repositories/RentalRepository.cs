using AutoMapper;
using AutoMapper.QueryableExtensions;
using Locatudo.Domain.Entities;
using Locatudo.Domain.Repositories;
using Locatudo.Infra.Data;
using Locatudo.Shared.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Locatudo.Infra.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        private readonly LocatudoDataContext _context;
        private readonly IConfigurationProvider _configurationProvider;

        public RentalRepository(LocatudoDataContext context, IConfigurationProvider configurationProvider)
        {
            _context = context;
            _configurationProvider = configurationProvider;
        }

        public void Update(Rental entity)
        {
            _context.Attach(entity).State = EntityState.Modified;
            _context.Attach(entity.Status);
            _context.Attach(entity.Time);
            _context.SaveChanges();
        }

        public void Create(Rental entity)
        {
            _context.Rentals.Add(entity);
            _context.Attach(entity.Equipment);
            _context.Attach(entity.Tenant);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var rental = _context.Rentals.FirstOrDefault(x => x.Id == id);
            if (rental != null)
                _context.Rentals.Remove(rental);
        }

        public Rental? GetById(Guid id)
        {
            return _context.Rentals
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Rental> List()
        {
            return _context.Rentals
                .AsNoTracking()
                .ToList();
        }

        public bool CheckAvailability(Guid equipmentId, RentalTime start)
        {
            return !_context.Rentals.Any(x => x.Equipment.Id == equipmentId && x.Time.Start == start.Start);
        }

        public IEnumerable<U> List<U>()
        {
            return _context.Rentals
                .AsNoTracking()
                .ProjectTo<U>(_configurationProvider)
                .ToList();
        }

        public U? GetById<U>(Guid id)
        {
            return _context.Rentals
                .AsNoTracking()
                .Where(x => x.Id == id)
                .ProjectTo<U>(_configurationProvider)
                .FirstOrDefault();
        }

        public Rental? GetByIdIncludingEquipment(Guid id)
        {
            return _context.Rentals
                .AsNoTracking()
                .Include(x => x.Equipment)
                .FirstOrDefault(x => x.Id == id);
        }
    }
}
