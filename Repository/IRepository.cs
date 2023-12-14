namespace Savory_Website.Repository
{
    public interface IRepository<T>where T : class
    {
        Task<T> GetById(int id);
        Task<bool> Add(T entity);
        Task<bool> DeleteById(int id);
        Task<bool> UpdateById(T entity);
        IEnumerable<T> GetAll();
    }
}
