using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace K94Warriors.Data.Contracts
{
    public interface IRepository<T>
        where T : class
    {
        IQueryable<T> GetAll();
        T GetById(int id);
        void Update(T entity);
        void Update(IEnumerable<T> entities);
        void Insert(T entity);
        void Insert(IEnumerable<T> entities);
        void Delete(T entity);
        void Delete(IEnumerable<T> entities);
        void Delete(int id);
        void Delete(IEnumerable<int> ids);
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
    }
}