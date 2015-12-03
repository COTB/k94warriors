using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using K94Warriors.Data.Contracts;

namespace K94Warriors.Data
{
    public class EFRepository<T> : IRepository<T>
        where T : class
    {
        public EFRepository(DbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException(nameof(dbContext));

            DbContext = dbContext;
            DbSet = dbContext.Set<T>();
        }

        protected DbContext DbContext { get; }

        protected DbSet<T> DbSet { get; }

        public IQueryable<T> GetAll() => DbSet;

        public T GetById(int id) => DbSet.Find(id);

        public void Update(T entity)
        {
            DbSet.Attach(entity);
            DbContext.Entry(entity).State = EntityState.Modified;
            DbContext.SaveChanges();
        }

        public void Update(IEnumerable<T> entities)
        {
            foreach (var dbObj in entities.Select(entity => DbContext.Entry(entity)))
            {
                dbObj.State = EntityState.Modified;
            }
            DbContext.SaveChanges();
        }

        public void Insert(T entity)
        {
            DbSet.Add(entity);
            DbContext.SaveChanges();
        }

        public void Insert(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
                DbSet.Add(entity);
            DbContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            var dbObj = DbContext.Entry(entity);
            dbObj.State = EntityState.Deleted;
            DbContext.SaveChanges();
        }

        public void Delete(IEnumerable<T> entities)
        {
            foreach (var dbObj in entities.Select(entity => DbContext.Entry(entity)))
            {
                dbObj.State = EntityState.Deleted;
            }
            DbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = DbSet.Find(id);
            if (entity == null) return;

            Delete(entity);
        }

        public void Delete(IEnumerable<int> ids)
        {
            var entities = ids.Select(id => DbSet.Find(id)).ToList();
            Delete(entities);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate) => DbSet.Where(predicate);
    }
}