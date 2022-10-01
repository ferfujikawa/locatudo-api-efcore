using Locatudo.Domain.Entities;
using Locatudo.Domain.Repositories;
using Locatudo.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Locatudo.Infra.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly LocatudoDataContext _context;

        public DepartmentRepository(LocatudoDataContext context)
        {
            _context = context;
        }

        public void Update(Department entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Entry(entity.Email).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Create(Department entity)
        {
            _context.Departments.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var department = _context.Departments.FirstOrDefault(x => x.Id == id);
            if (department != null)
                _context.Departments.Remove(department);
        }

        public Department? GetById(Guid id)
        {
            return _context.Departments.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Department> List()
        {
            return _context.Departments.ToList();
        }
    }
}
