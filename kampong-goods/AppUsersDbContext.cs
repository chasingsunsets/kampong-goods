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
            string connectionString = _configuration.GetConnectionString("MyConnection"); 
            optionsBuilder.UseSqlServer(connectionString);

            // Sorry team, I need to use SQLite to run on my machine :(
            // Love you lots <3 - Zhiyi
            // TODO: create a script to automatically detect machine type
            // e.g.
            // MacOS      SQLite
            // Windows    SQLServer
            // Linux      SQLServer
            // string sqliteConnectionString = _configuration.GetConnectionString("dev");
            // optionsBuilder.UseSqlite(sqliteConnectionString);
        }


        public DbSet<StaffInfo> StaffInfos { get; set; }

    }

}
