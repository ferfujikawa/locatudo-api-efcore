﻿using Locatudo.Domain.Entities;
using Locatudo.Domain.Repositories;
using Locatudo.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Locatudo.Infra.Repositories
{
    public class OutsourcedRepository : IOutsourcedRepository
    {
        private readonly LocatudoDataContext _context;

        public OutsourcedRepository(LocatudoDataContext context)
        {
            _context = context;
        }

        public void Update(Outsourced entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Entry(entity.Email).State = EntityState.Modified;
            _context.Entry(entity.EnterpriseName).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Create(Outsourced entity)
        {
            _context.Outsourceds.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var outsourced = _context.Outsourceds.FirstOrDefault(x => x.Id == id);
            if (outsourced != null)
                _context.Outsourceds.Remove(outsourced);
        }

        public Outsourced? GetById(Guid id)
        {
            return _context.Outsourceds.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Outsourced> List()
        {
            return _context.Outsourceds.AsNoTracking().ToList();
        }
    }
}
