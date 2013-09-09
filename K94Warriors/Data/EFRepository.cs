using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace K94Warriors.Data
{
    public class EFRepository<T> : IRepository<T>
        where T : class
    {
        private readonly DbContext _dbContext;

        private readonly DbSet<T> _dbSet;

        public EFRepository(DbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("dbContext");

            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        protected DbContext DbContext
        {
            get { return _dbContext; }
        }

        protected DbSet<T> DbSet
        {
            get { return _dbSet; }
        }

        public IQueryable<T> GetAll()
        {
            return DbSet;
        }

        public T GetById(int id)
        {
            return DbSet.Find(id);
        }

        public void Update(T entity)
        {
            DbEntityEntry<T> dbObj = DbContext.Entry(entity);
            dbObj.State = EntityState.Modified;
            DbContext.SaveChanges();
        }

        public void Insert(T entity)
        {
            DbSet.Add(entity);
            DbContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            DbEntityEntry<T> dbObj = DbContext.Entry(entity);
            dbObj.State = EntityState.Deleted;
            DbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            T entity = DbSet.Find(id);
            if (entity == null) return;

            DbEntityEntry<T> dbOjb = DbContext.Entry(entity);
            dbOjb.State = EntityState.Deleted;
            DbContext.SaveChanges();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }
    }
}