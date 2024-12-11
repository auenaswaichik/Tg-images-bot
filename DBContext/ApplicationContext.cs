using Microsoft.EntityFrameworkCore;
using MyProj.Entities;

namespace MyProj.DBContext;

public class ApplicationContext : DbContext {

    public DbSet<ImageInfo> Images => Set<ImageInfo>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=printer.db");
    }

    	
    public ApplicationContext() => Database.EnsureCreated();

}

