using Microsoft.EntityFrameworkCore;
using kampong_goods.Models;

namespace kampong_goods
{
    public class VoucherDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public VoucherDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = _configuration.GetConnectionString("MyConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<Voucher> Vouchers { get; set; }
    }
}
