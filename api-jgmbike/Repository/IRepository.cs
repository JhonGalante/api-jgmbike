using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace api_jgmbike.Repository
{
    public interface IRepository<T>
    {
        IEnumerable<T> Get();
        Task<T> GetById(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        bool EntityExists(int id);

    }
}
