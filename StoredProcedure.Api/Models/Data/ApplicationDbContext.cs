using Microsoft.EntityFrameworkCore;
using StoredProcedure.Api.Models.Entities;

namespace StoredProcedure.Api.Models.Data;

public class ApplicationDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    
    public ApplicationDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
    }
    public DbSet<Product> Product { get; set; }
}