using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace HUYHOA_PS13016_ASM.Models
{
    public class DataContext: DbContext
    {
        public DataContext()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // You don't actually ever need to call this
        }
        public DataContext(DbContextOptions options) : base(options) { }

        public DbSet<Roles> Roles { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Carts> Carts { get; set; }
        public DbSet<CartDetails> CartDetails { get; set; } 
        public DbSet<Foods> Foods { get; set; }
        public DbSet<CategoryModel> CategoryModels { get; set; }

    }
}
