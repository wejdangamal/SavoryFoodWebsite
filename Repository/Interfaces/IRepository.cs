using System.Linq.Expressions;

namespace Savory_Website.Repository
{
    public interface IRepository<T> :IDisposable where T : class
    {
        Task<T> GetById(int id);
        Task<bool> Add(T entity);
        Task<bool> DeleteById(int id);
        Task<bool> Update(T entity);
        IEnumerable<T> GetAll(string[] includes =null);
        IEnumerable<T> GetAll(Expression<Func<T,bool>> search,string[] includes =null);
        Task<bool> Delete(T row);
    }
}
