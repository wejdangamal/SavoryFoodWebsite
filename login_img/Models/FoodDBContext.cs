using Microsoft.EntityFrameworkCore;

namespace login_img.Models
{
    public class FoodDBContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-3RG99TA;Initial Catalog=AppDeliveryDB;Integrated Security=True");

            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderProduct>().HasKey(o => new { o.customer_id, o.productId });
            modelBuilder.Entity<Customer>().HasIndex(c => c.email).IsUnique();
            modelBuilder.Entity<Customer>().HasIndex(c => c.phone).IsUnique();
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Customer> customers { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<category> categories { get; set; }
        public DbSet<OrderProduct> orderProducts { get; set; }
        public DbSet<Admin> admins { get; set; }

    }
}
