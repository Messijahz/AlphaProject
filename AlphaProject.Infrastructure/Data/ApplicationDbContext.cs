using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AlphaProject.Core.Models;

namespace AlphaProject.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<AppUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Project> Projects { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<ProjectMember> ProjectMembers { get; set; }
    public DbSet<ProjectManager> ProjectManagers { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Status> Statuses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var adminRoleId = new Guid("11111111-1111-1111-1111-111111111111");
        var pmRoleId = new Guid("22222222-2222-2222-2222-222222222222");
        var memberRoleId = new Guid("33333333-3333-3333-3333-333333333333");

        modelBuilder.Entity<Role>().HasData(
            new Role { RoleId = adminRoleId, RoleName = "Admin" },
            new Role { RoleId = pmRoleId, RoleName = "ProjectManager" },
            new Role { RoleId = memberRoleId, RoleName = "ProjectMember" }
        );

        modelBuilder.Entity<Status>().HasData(
            new Status { StatusId = new Guid("44444444-4444-4444-4444-444444444444"), StatusName = "New" },
            new Status { StatusId = new Guid("55555555-5555-5555-5555-555555555555"), StatusName = "In Progress" },
            new Status { StatusId = new Guid("66666666-6666-6666-6666-666666666666"), StatusName = "Completed" }
        );

        modelBuilder.Entity<Project>()
            .HasOne(p => p.Customer)
            .WithMany()
            .HasForeignKey(p => p.CustomerId);

        modelBuilder.Entity<Project>()
            .HasOne(p => p.ProjectManager)
            .WithMany()
            .HasForeignKey(p => p.ProjectManagerId);

        modelBuilder.Entity<Project>()
            .Property(p => p.Budget)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Notification>()
            .HasOne(n => n.User)
            .WithMany()
            .HasForeignKey(n => n.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
