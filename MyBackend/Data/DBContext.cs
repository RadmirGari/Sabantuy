using Microsoft.EntityFrameworkCore;
using Model;
public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public DbSet<Section> sections { get; set; }

    public DbSet<Subscribers> subscribers { get; set; }
}

