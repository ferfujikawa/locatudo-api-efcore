using AutoMapper;
using AutoMapper.QueryableExtensions;
using Locatudo.Domain.Entities;
using Locatudo.Domain.Repositories;
using Locatudo.Infra.Data;
using Locatudo.Infra.Queries;
using Microsoft.EntityFrameworkCore;

namespace Locatudo.Infra.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly LocatudoDataContext _context;
        private readonly IConfigurationProvider _configurationProvider;

        public DepartmentRepository(
            LocatudoDataContext context,
            IConfigurationProvider configurationProvider)
        {
            _context = context;
            _configurationProvider = configurationProvider;
        }

        public void Update(Department entity)
        {
            _context.Attach(entity).State = EntityState.Modified;
            _context.Attach(entity.Email);
            _context.SaveChanges();
        }

        public void Create(Department entity)
        {
            _context.Departments.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var department = _context.Departments
                .FilterById(id)
                .FirstOrDefault();
            if (department != null)
            {
                _context.Remove(department);
                _context.SaveChanges();
            }
        }

        public Department? GetById(Guid id)
        {
            return _context.Departments
                .AsNoTracking()
                .FilterById(id)
                .FirstOrDefault();
        }

        public IEnumerable<Department> List()
        {
            return _context.Departments
                .AsNoTracking()
                .ToList();
        }

        public IEnumerable<U> List<U>()
        {
            return _context.Departments
                .AsNoTracking()
                .ProjectTo<U>(_configurationProvider)
                .ToList();
        }

        public U? GetById<U>(Guid id)
        {
            return _context.Departments
                .AsNoTracking()
                .FilterById(id)
                .ProjectTo<U>(_configurationProvider)
                .FirstOrDefault();
        }
    }
}
