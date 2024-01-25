using Caffeine.You.Need.Models;
using Microsoft.EntityFrameworkCore;

namespace Caffeine.You.Need.Data;
public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public DbSet<Coffee> Coffees { get; set; }
    public DbSet<Recommendation> Recommendations { get; set; }
}
