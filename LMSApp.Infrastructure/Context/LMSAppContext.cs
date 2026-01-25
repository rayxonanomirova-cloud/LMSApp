using LMSApp.Domain.Entities.Auth;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LMSApp.Infrastructure.Context
{
    public class LMSAppContext : IdentityDbContext<ApplicationUser,ApplicationRole,Guid>
    {
        public LMSAppContext(DbContextOptions<LMSAppContext> options) : base(options)
        { }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Configure your entity mappings here

            builder.ApplyConfigurationsFromAssembly(typeof(LMSAppContextSeed).Assembly).Seed();
        }

        public DbContext DbContext => this;
    }
}
