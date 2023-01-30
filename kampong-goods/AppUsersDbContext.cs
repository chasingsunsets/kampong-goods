using kampong_goods.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace kampong_goods
{
    public class AppUsersDbContext: IdentityDbContext<AppUser>
    {

        private readonly IConfiguration _configuration;
        public AppUsersDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = _configuration.GetConnectionString("MyConnection"); optionsBuilder.UseSqlServer(connectionString);
        }


        public DbSet<StaffInfo> StaffInfos { get; set; }
        public DbSet<Request> Requests { get; set; }


        // for chat: relationships
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Message>()
                .HasOne<AppUser>(a => a.Sender)
                .WithMany(d => d.Messages)
                .HasForeignKey(d => d.UserId);
        }

        public DbSet<Message> Messages { get; set; }

    }

}
