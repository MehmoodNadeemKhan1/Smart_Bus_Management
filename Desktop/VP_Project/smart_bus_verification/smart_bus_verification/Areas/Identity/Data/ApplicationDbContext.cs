using smart_bus_verification.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using smart_bus_verification.Areas.Identity.Data;
using System.Reflection.Emit;

namespace smart_bus_verification.Areas.Identity.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Student> Students { get; set; }
    public DbSet<Expenditures> Expenditures { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ApplicationUser>()

                .HasOne(u => u.Student)
                .WithOne(s => s.applicationUser)
                .HasForeignKey<Student>(s => s.Roll)
                .IsRequired(false);

     

        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
    }

}

public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(x => x.RollNo).HasMaxLength(15);
    }
}