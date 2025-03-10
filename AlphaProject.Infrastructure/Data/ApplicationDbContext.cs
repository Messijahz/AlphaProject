using Microsoft.EntityFrameworkCore;
using AlphaProject.Core.Models;

namespace AlphaProject.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<ProjectMember> ProjectMembers { get; set; }
    public DbSet<ProjectManager> ProjectManagers { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<ChangeLog> ChangeLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Role>().HasData(
            new Role { RoleId = Guid.NewGuid(), RoleName = "Admin" },
            new Role { RoleId = Guid.NewGuid(), RoleName = "ProjectManager" },
            new Role { RoleId = Guid.NewGuid(), RoleName = "ProjectMember" }
        );

        modelBuilder.Entity<Status>().HasData(
            new Status { StatusId = Guid.NewGuid(), StatusName = "New" },
            new Status { StatusId = Guid.NewGuid(), StatusName = "In Progress" },
            new Status { StatusId = Guid.NewGuid(), StatusName = "Completed" }
        );

        modelBuilder.Entity<Project>()
            .HasOne(p => p.Customer)
            .WithMany()
            .HasForeignKey(p => p.CustomerId);

        modelBuilder.Entity<Project>()
            .HasOne(p => p.ProjectManager)
            .WithMany()
            .HasForeignKey(p => p.ProjectManagerId);
    }
}
