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
    Task SoftDeleteProjectAsync(Guid id);

}
