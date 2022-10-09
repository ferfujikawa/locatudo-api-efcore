using Locatudo.Domain.Entities;
using Locatudo.Domain.Repositories;
using Locatudo.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Locatudo.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly LocatudoDataContext _context;

        public UserRepository(LocatudoDataContext context)
        {
            _context = context;
        }

        public void Update(User entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Entry(entity.Email).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Create(User entity)
        {
            //Não pode criar um User sem especialização (Employee/Outsourced)
        }

        public void Delete(Guid id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            if (user != null)
                _context.Users.Remove(user);
        }

        public User? GetById(Guid id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<User> List()
        {
            return _context.Users.AsNoTracking().ToList();
        }
    }
}
