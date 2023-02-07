using Microsoft.EntityFrameworkCore;
using kampong_goods.Models;
using System.Collections.Generic;

namespace kampong_goods
{
    public class FAQDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public FAQDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string faqString = _configuration.GetConnectionString("MyConnection");
            optionsBuilder.UseSqlServer(faqString);
        }

        public DbSet<FAQ> FAQs { get; set; }

        public DbSet<FAQCategory> FAQCategorys { get; set; }

    }
}
