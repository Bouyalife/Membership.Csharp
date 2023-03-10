namespace Membership.Models
{

using Microsoft.EntityFrameworkCore;

    public class Context : DbContext
    {                                  //lägg till en context

        protected readonly IConfiguration Configuration;

        public Context(IConfiguration configuration)
        {
            Configuration = configuration;
        }
                                
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Member>()
                .Property(p => p.memberType)
                .HasDefaultValue("silver");
            modelBuilder.Entity<Member>()
                .Property(p => p.points)
                .HasDefaultValue(0);
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var connectionString = Configuration.GetConnectionString("MembershipApp");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        public DbSet<Member> Member { get; set; }
    }
}
