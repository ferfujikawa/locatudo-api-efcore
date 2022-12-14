using AutoMapper;
using AutoMapper.QueryableExtensions;
using Locatudo.Domain.Entities;
using Locatudo.Domain.Queries.Extensions;
using Locatudo.Domain.Repositories;
using Locatudo.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Locatudo.Infra.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly LocatudoDataContext _context;
        private readonly IConfigurationProvider _configurationProvider;

        public EmployeeRepository(
            LocatudoDataContext context,
            IConfigurationProvider configurationProvider)
        {
            _context = context;
            _configurationProvider = configurationProvider;
        }

        public void Update(Employee entity)
        {
            _context.Attach(entity).State = EntityState.Modified;
            _context.Attach(entity.Email);
            _context.Attach(entity.Department);
            _context.SaveChanges();
        }

        public void Create(Employee entity)
        {
            _context.Employees.Add(entity);
            _context.Attach(entity.Department);
            _context.SaveChanges();
        }

        public void Delete(Employee entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public Employee? GetById(Guid id)
        {
            return _context.Employees
                .AsNoTracking()
                .FilterById(id)
                .FirstOrDefault();
        }

        public IEnumerable<Employee> List()
        {
            return _context.Employees
                .AsNoTracking()
                .ToList();
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
                .FilterById(id)
                .ProjectTo<U>(_configurationProvider)
                .FirstOrDefault();
        }
    }
}
