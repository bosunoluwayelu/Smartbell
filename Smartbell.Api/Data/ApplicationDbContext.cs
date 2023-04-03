using Smartbell.Shared.Entities;

namespace Smartbell.Api.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base (options){}

        public DbSet<Config> Configs { get; set; }
        public DbSet<Ringtone> Ringtones { get; set; }
        public DbSet<Activity> Activities { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
