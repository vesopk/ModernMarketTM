using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ModernMarketTM.Models;

namespace ModernMarketTM.Data
{
    public class ModernMarketTmDbContext : IdentityDbContext<User>
    {
        public DbSet<Type> Types { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<CategoryInstance> CategoryInstances { get; set; }

        public DbSet<UserInstance> UserInstances { get; set; }

        public DbSet<Order> Orders { get; set; }

        
        public ModernMarketTmDbContext(DbContextOptions<ModernMarketTmDbContext> options)
            : base(options)
        {
        }
    }
}
