using System;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace K94Warriors
{
    public class EFRepository<T> : IRepository<T>
        where T : class
    {
        private readonly DbContext _dbContext;
        protected DbContext DbContext { get { return _dbContext; } }

        private readonly DbSet<T> _dbSet;
        protected DbSet<T> DbSet { get { return _dbSet; } }

        public EFRepository(DbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("dbContext");

            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
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
            var dbObj = DbContext.Entry(entity);
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
            var dbObj = DbContext.Entry(entity);
            dbObj.State = EntityState.Deleted;
            DbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = DbSet.Find(id);
            if (entity == null) return;

            var dbOjb = DbContext.Entry(entity);
            dbOjb.State = EntityState.Deleted;
            DbContext.SaveChanges();
        }
    }
}