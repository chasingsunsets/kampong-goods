using kampong_goods.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace kampong_goods
{
    public class CustomersInfoDbContext: IdentityDbContext<CustomersInfo>
    {

        private readonly IConfiguration _configuration;
        public CustomersInfoDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = _configuration.GetConnectionString("MyConnection"); optionsBuilder.UseSqlServer(connectionString);
        }
    }

}
