using api_jgmbike.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace api_jgmbike.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<T> Get()
        {
            return _context.Set<T>().ToList();
        }

        public Task<T> GetById(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().SingleOrDefaultAsync(predicate);
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }
        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public bool EntityExists(int id)
        {
            return _context.Produtos.Any(prod => prod.Id == id);
        }


    }
}
