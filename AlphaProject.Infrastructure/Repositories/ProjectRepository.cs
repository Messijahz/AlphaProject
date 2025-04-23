using AlphaProject.Core.Models;
using AlphaProject.Core.Services.Interfaces;
using AlphaProject.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaProject.Infrastructure.Services;

public class ProjectRepository : IProjectRepository
{
    private readonly ApplicationDbContext _context;

    public ProjectRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Project project)
    {
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Project>> GetAllAsync()
    {
        return await _context.Projects
        .Where(p => !p.IsDeleted)
        .Include(p => p.Customer)
        .Include(p => p.ProjectMembers)
        .ToListAsync();
    }

    public async Task<Project?> GetByIdAsync(Guid id)
    {
        return await _context.Projects.FirstOrDefaultAsync(p => p.ProjectId == id);
    }

    public async Task SoftDeleteAsync(Guid id)
    {

        var project = await _context.Projects.FirstOrDefaultAsync(p => p.ProjectId == id);

        if (project != null)
        {
            project.IsDeleted = true;
            await _context.SaveChangesAsync();
        }
    }
}
