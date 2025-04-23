using AlphaProject.Core.Models;
using AlphaProject.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaProject.Core.Services;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _repository;

    public ProjectService(IProjectRepository repository)
    {
        _repository = repository;
    }

    public async Task CreateProjectAsync(Project project)
    {
        await _repository.AddAsync(project);
    }

    public async Task<IEnumerable<Project>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task SoftDeleteProjectAsync(Guid id)
    {
        
        await _repository.SoftDeleteAsync(id);
   
    }
}
