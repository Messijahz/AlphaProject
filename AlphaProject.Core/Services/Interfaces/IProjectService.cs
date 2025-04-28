using AlphaProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaProject.Core.Services.Interfaces;

public interface IProjectService
{
    Task CreateProjectAsync(Project project);
    Task<IEnumerable<Project>> GetAllAsync();
    Task<Project?> GetByIdAsync(Guid id);
    Task UpdateProjectAsync(Project project);
    Task SoftDeleteProjectAsync(Guid id);

}
