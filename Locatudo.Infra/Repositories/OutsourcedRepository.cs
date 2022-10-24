using AutoMapper;
using AutoMapper.QueryableExtensions;
using Locatudo.Domain.Entities;
using Locatudo.Domain.Repositories;
using Locatudo.Infra.Data;
using Locatudo.Infra.Queries;
using Microsoft.EntityFrameworkCore;

namespace Locatudo.Infra.Repositories
{
    public class OutsourcedRepository : IOutsourcedRepository
    {
        private readonly LocatudoDataContext _context;
        private readonly IConfigurationProvider _configurationProvider;

        public OutsourcedRepository(
            LocatudoDataContext context,
            IConfigurationProvider configurationProvider)
        {
            _context = context;
            _configurationProvider = configurationProvider;
        }

        public void Update(Outsourced entity)
        {
            _context.Attach(entity).State = EntityState.Modified;
            _context.Attach(entity.Email);
            _context.Attach(entity.EnterpriseName);
            _context.SaveChanges();
        }

        public void Create(Outsourced entity)
        {
            _context.Outsourceds.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var outsourced = _context.Outsourceds
                .FilterById(id)
                .FirstOrDefault();
            if (outsourced != null)
                _context.Outsourceds.Remove(outsourced);
        }

        public Outsourced? GetById(Guid id)
        {
            return _context.Outsourceds
                .AsNoTracking()
                .FilterById(id)
                .FirstOrDefault();
        }

        public IEnumerable<Outsourced> List()
        {
            return _context.Outsourceds
                .AsNoTracking()
                .ToList();
        }

        public IEnumerable<U> List<U>()
        {
            return _context.Outsourceds
                .AsNoTracking()
                .ProjectTo<U>(_configurationProvider)
                .ToList();
        }

        public U? GetById<U>(Guid id)
        {
            return _context.Outsourceds
                .AsNoTracking()
                .FilterById(id)
                .ProjectTo<U>(_configurationProvider)
                .FirstOrDefault();
        }
    }
}
