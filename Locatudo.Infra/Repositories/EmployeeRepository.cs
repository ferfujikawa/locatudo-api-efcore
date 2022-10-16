using AutoMapper;
using AutoMapper.QueryableExtensions;
using Locatudo.Domain.Entities;
using Locatudo.Domain.Repositories;
using Locatudo.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Locatudo.Infra.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly LocatudoDataContext _context;
        private readonly IConfigurationProvider _configurationProvider;

        public EmployeeRepository(LocatudoDataContext context, IConfigurationProvider configurationProvider)
        {
            _context = context;
            _configurationProvider = configurationProvider;
        }

        public void Update(Employee entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Entry(entity.Email).State = EntityState.Modified;
            _context.Entry(entity.Department).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Create(Employee entity)
        {
            _context.Employees.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var employee = _context.Employees.FirstOrDefault(x => x.Id == id);
            if (employee != null)
                _context.Employees.Remove(employee);
        }

        public Employee? GetById(Guid id)
        {
            return _context.Employees
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Employee> List()
        {
            return _context.Employees.AsNoTracking().ToList();
        }

        public IEnumerable<U> List<U>()
        {
            return _context.Employees
                .AsNoTracking()
                .ProjectTo<U>(_configurationProvider)
                .ToList();
        }

        public U? GetById<U>(Guid id)
        {
            return _context.Employees
                .AsNoTracking()
                .Where(x => x.Id == id)
                .ProjectTo<U>(_configurationProvider)
                .FirstOrDefault();
        }
    }
}
