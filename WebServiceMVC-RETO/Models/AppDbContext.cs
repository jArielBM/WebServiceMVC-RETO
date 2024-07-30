namespace WebServiceMVC_RETO.Models;
using Microsoft.EntityFrameworkCore;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Cerveza> Cervezas { get; set; }

}

