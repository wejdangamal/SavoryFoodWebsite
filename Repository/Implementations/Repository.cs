using Microsoft.EntityFrameworkCore;
using Savory_Website.Data;
using System.Linq.Expressions;

namespace Savory_Website.Repository.Implementations
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly FoodDBContext context;
        private DbSet<T> entity;

        public Repository(FoodDBContext _context)
        {
            context = _context;
            entity = _context.Set<T>();
        }

        public async Task<bool> Add(T row)
        {
            if (row is not null)
                entity.Add(row);
            var result = await context.SaveChangesAsync();
            return result > 0;
        }


        public async Task<bool> DeleteById(int id)
        {
            var vaildId = await entity.FindAsync(id);
            if (vaildId != null)
            {
                entity.Remove(vaildId);
            }
            var result = await context.SaveChangesAsync();
            return result > 0;
        }
        public async Task<bool> Delete(T row)
        {
            if (row != null)
            {
                entity.Remove(row);
            }
            var result = await context.SaveChangesAsync();
            return result > 0;
        }

        public IEnumerable<T> GetAll(string[] includes = null)
        {
            var query = entity.AsQueryable();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return query.Select(x => x).ToList();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> search, string[] includes = null)
        {
            var query = entity.AsQueryable();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return query.Where(search).ToList();
        }

        public async Task<T> GetById(int id)
        {
            var entry = await entity.FindAsync(id);
            return entry;
        }

        public async Task<bool> Update(T entity)
        {
            var result = await context.SaveChangesAsync();
            return result > 0;
        }
        public void Dispose()
        {
            //close connection string || context of DB
            context.Dispose(); 
        }
    }
}
