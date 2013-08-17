using System.Linq;

namespace K94Warriors
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
    }
}