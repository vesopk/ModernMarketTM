using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ModernMarketTM.Models;

namespace ModernMarketTM.Data
{
    public class ModernMarketTmDbContext : IdentityDbContext<User>
    {

        public DbSet<Category> Categories { get; set; }

        public DbSet<CategoryInstance> CategoryInstances { get; set; }

        public DbSet<UserInstance> Orders { get; set; }

        public DbSet<Type> Types { get; set; }
        
        public ModernMarketTmDbContext(DbContextOptions<ModernMarketTmDbContext> options)
            : base(options)
        {
        }
    }
}
