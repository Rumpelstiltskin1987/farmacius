using Microsoft.EntityFrameworkCore;
using farmacius.Models;



namespace farmacius.DataBase
{
    public class FarmaciusContext : DbContext
    {
        public FarmaciusContext(DbContextOptions<FarmaciusContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
    }

}
