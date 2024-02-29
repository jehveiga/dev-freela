using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DevFreelaDbContext _dbContext;

        public ProjectRepository(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Project>> GetAllAsync(string query)
        {
            IQueryable<Project> projects = _dbContext.Projects;

            if (!string.IsNullOrWhiteSpace(query))
            {
                projects = projects.Where(project =>
                    project.Title.Contains(query) ||
                    project.Description.Contains(query)

                );
            }

            return await projects.ToListAsync();
        }

        public async Task<Project> GetByIdAsync(int id) => await _dbContext.Projects.SingleOrDefaultAsync(p => p.Id == id);

        public async Task<Project> GetDetailsByIdAsync(int id) => await _dbContext.Projects
                .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .SingleOrDefaultAsync(p => p.Id == id);

        public async Task AddAsync(Project project)
        {
            await _dbContext.Projects.AddAsync(project);
            await SaveChangesAsync();
        }

        public async Task AddCommentAsync(ProjectComment projectComment)
        {
            await _dbContext.ProjectComments.AddAsync(projectComment);
            await SaveChangesAsync();
        }

        public async Task StartAsync(Project project)
        {
            await SaveChangesAsync();
        }

        public async Task FinishAsync(Project project)
        {
            await SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
