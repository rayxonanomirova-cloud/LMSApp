using Microsoft.EntityFrameworkCore;

namespace LMSApp.Infrastructure.Context
{
    public class LMSAppContext : DbContext
    {
        public LMSAppContext(DbContextOptions<LMSAppContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure your entity mappings here
        }
    }
}
