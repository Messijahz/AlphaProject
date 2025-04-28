using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AlphaProject.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace AlphaProject.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ProjectMember> ProjectMembers { get; set; }
        public DbSet<ProjectManager> ProjectManagers { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Status> Statuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var adminRoleId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var pmRoleId = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var memberRoleId = Guid.Parse("33333333-3333-3333-3333-333333333333");

            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = adminRoleId, RoleName = "Admin" },
                new Role { RoleId = pmRoleId, RoleName = "ProjectManager" },
                new Role { RoleId = memberRoleId, RoleName = "ProjectMember" }
            );

            var startedStatusId = Guid.Parse("55555555-5555-5555-5555-555555555555");
            var completedStatusId = Guid.Parse("66666666-6666-6666-6666-666666666666");

            modelBuilder.Entity<Status>().HasData(
                new Status { StatusId = startedStatusId, StatusName = "Started" },
                new Status { StatusId = completedStatusId, StatusName = "Completed" }
            );

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
}
