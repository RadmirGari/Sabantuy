using Microsoft.EntityFrameworkCore;
public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public DbSet<MyEntity> MyEntities { get; set; }
}

public class MyEntity
{
    public int Id { get; set; }
    public required string Name { get; set; }
}