using System.Linq.Expressions;

namespace UnitTestProject.UI.Data
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();

        List<T> GetAll(Expression<Func<T, bool>> expression);

        T GetByid(int id);

        T GetOne(Expression<Func<T, bool>> expression);

        void Add(T entity);

        void Delete(T entity);

        void Update(T entity);


    }
}
