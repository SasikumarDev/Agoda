using Agoda.Models;
using Microsoft.EntityFrameworkCore;

namespace Agoda.Data;

public class AgodaDbContext : DbContext
{
    public virtual DbSet<SiteUser> SiteUser { get; set; }
    public AgodaDbContext(DbContextOptions<AgodaDbContext> dbContextOptions) : base(dbContextOptions)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SiteUser>().HasKey(e => e.Sid).HasName("PK_Sid");
        modelBuilder.Entity<SiteUser>(e =>
        {
            e.Property(p=>p.Sid).HasColumnType("uniqueidentifier").HasColumnName("Sid").HasDefaultValueSql("newid()");
            e.Property(p=>p.Name).HasColumnType("nvarchar(30)").HasColumnName("Name").IsRequired();
            e.Property(p=>p.Email).HasColumnType("nvarchar(30)").HasColumnName("Email").IsRequired();
            e.Property(p=>p.Password).HasColumnType("nvarchar(90)").HasColumnName("Password").IsRequired();
            e.Property(p=>p.Status).HasColumnType("bit").HasColumnName("Status").HasDefaultValueSql("1").IsRequired();
        });
    }
}