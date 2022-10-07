using Locatudo.Domain.Entities;
using Locatudo.Domain.Repositories;
using Locatudo.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Locatudo.Infra.Repositories
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private readonly LocatudoDataContext _context;

        public EquipmentRepository(LocatudoDataContext context)
        {
            _context = context;
        }

        public void Update(Equipment entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Create(Equipment entity)
        {
            _context.Equipments.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var department = _context.Equipments.FirstOrDefault(x => x.Id == id);
            if (department != null)
                _context.Equipments.Remove(department);
        }

        public Equipment? GetById(Guid id)
        {
            return _context.Equipments.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Equipment> List()
        {
            return _context.Equipments.AsNoTracking().ToList();
        }
    }
}
