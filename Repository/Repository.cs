using login_img.Models;
using Microsoft.EntityFrameworkCore;

namespace Savory_Website.Repository
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
            var result= await context.SaveChangesAsync();
            return result > 0;
        }

        public IEnumerable<T> GetAll()
        {
            var entities = entity.Select(x=>x).ToList();
            return entities;
        }

        public async Task<T> GetById(int id)
        {
            var entry = await entity.FindAsync(id);
            return entry;
        }

        public async Task<bool> UpdateById(T entity)
        {
            var result = await context.SaveChangesAsync();
            return result > 0;
        }
    }
}
