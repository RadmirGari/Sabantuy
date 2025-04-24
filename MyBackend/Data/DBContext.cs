using Microsoft.EntityFrameworkCore;
using Data.Models;

namespace Data.DBContext{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        public DbSet<Section> sections { get; set; }

        public DbSet<Subscribers> subscribers { get; set; }

        public DbSet<Image> Images { get; set; }
    }
}

