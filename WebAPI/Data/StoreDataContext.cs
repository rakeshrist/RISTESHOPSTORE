using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Data
{
    public class StoreDataContext : DbContext
    {
        public StoreDataContext(DbContextOptions<StoreDataContext> options) : base(options)
        {

        }
        public DbSet<tbl_category> tbl_categories { get; set; }
        public DbSet<tbl_user> tbl_users { get; set; }
    }
}