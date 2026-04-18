using Microsoft.EntityFrameworkCore;
using EventProject.Models;

namespace EventProject.Data;

public class MySqlDbContext : DbContext
{
    public MySqlDbContext(DbContextOptions<MySqlDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Event>  events { get; set; }
    
}