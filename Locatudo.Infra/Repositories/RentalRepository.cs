﻿using Locatudo.Domain.Entities;
using Locatudo.Domain.Repositories;
using Locatudo.Infra.Data;
using Locatudo.Shared.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Locatudo.Infra.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        private readonly LocatudoDataContext _context;

        public RentalRepository(LocatudoDataContext context)
        {
            _context = context;
        }

        public void Update(Rental entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Entry(entity.Status).State = EntityState.Modified;
            _context.Entry(entity.Time).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Create(Rental entity)
        {
            _context.Rentals.Add(entity);
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
            return _context.Rentals.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Rental> List()
        {
            return _context.Rentals.AsNoTracking().ToList();
        }

        public bool CheckAvailability(Guid equipmentId, RentalTime start)
        {
            return !_context.Rentals.Any(x => x.Equipment.Id == equipmentId && x.Time.Start == start.Start);
        }
    }
}
