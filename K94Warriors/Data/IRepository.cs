using System;
using System.Linq;
using System.Linq.Expressions;

namespace K94Warriors.Data
{
    public interface IRepository<T>
        where T : class
    {
        IQueryable<T> GetAll();
        T GetById(int id);
        void Update(T entity);
        void Insert(T entity);
        void Delete(T entity);
        void Delete(int id);
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
    }
}