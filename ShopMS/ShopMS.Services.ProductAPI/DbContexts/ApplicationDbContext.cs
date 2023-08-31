using Microsoft.EntityFrameworkCore;
using ShopMS.Services.ProductAPI.Model;

namespace ShopMS.Services.ProductAPI.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

    }
}
