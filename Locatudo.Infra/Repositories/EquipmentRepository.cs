using AutoMapper;
using AutoMapper.QueryableExtensions;
using Locatudo.Domain.Entities;
using Locatudo.Domain.Queries;
using Locatudo.Domain.Repositories;
using Locatudo.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Locatudo.Infra.Repositories
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private readonly LocatudoDataContext _context;
        private readonly IConfigurationProvider _configurationProvider;

        public EquipmentRepository(
            LocatudoDataContext context,
            IConfigurationProvider configurationProvider)
        {
            _context = context;
            _configurationProvider = configurationProvider;
        }

        public void Update(Equipment entity)
        {
            _context.Attach(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Create(Equipment entity)
        {
            _context.Equipments.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Equipment entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public Equipment? GetById(Guid id)
        {
            return _context.Equipments
                .AsNoTracking()
                .FilterById(id)
                .FirstOrDefault();
        }

        public IEnumerable<Equipment> List()
        {
            return _context.Equipments
                .AsNoTracking()
                .ToList();
        }

        public IEnumerable<U> List<U>()
        {
            return _context.Equipments
                .AsNoTracking()
                .ProjectTo<U>(_configurationProvider)
                .ToList();
        }

        public U? GetById<U>(Guid id)
        {
            return _context.Equipments
                .AsNoTracking()
                .FilterById(id)
                .ProjectTo<U>(_configurationProvider)
                .FirstOrDefault();
        }
    }
}
