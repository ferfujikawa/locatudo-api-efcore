using AutoMapper;
using AutoMapper.QueryableExtensions;
using Locatudo.Domain.Entities;
using Locatudo.Domain.Queries.Extensions;
using Locatudo.Domain.Repositories;
using Locatudo.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Locatudo.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly LocatudoDataContext _context;
        private readonly IConfigurationProvider _configurationProvider;

        public UserRepository(
            LocatudoDataContext context,
            IConfigurationProvider configurationProvider)
        {
            _context = context;
            _configurationProvider = configurationProvider;
        }

        public void Update(User entity)
        {
            _context.Attach(entity).State = EntityState.Modified;
            _context.Attach(entity.Email);
            _context.SaveChanges();
        }

        public void Create(User entity)
        {
            //Não pode criar um User sem especialização (Employee/Outsourced)
        }

        public void Delete(User entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public User? GetById(Guid id)
        {
            return _context.Users
                .AsNoTracking()
                .FilterById(id)
                .FirstOrDefault();
        }

        public IEnumerable<User> List()
        {
            return _context.Users
                .AsNoTracking()
                .ToList();
        }

        public IEnumerable<U> List<U>()
        {
            return _context.Users
                .AsNoTracking()
                .ProjectTo<U>(_configurationProvider)
                .ToList();
        }

        public U? GetById<U>(Guid id)
        {
            return _context.Users
                .AsNoTracking()
                .FilterById(id)
                .ProjectTo<U>(_configurationProvider)
                .FirstOrDefault();
        }
    }
}
