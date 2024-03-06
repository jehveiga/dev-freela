using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        private readonly DevFreelaDbContext _dbContext;

        public SkillRepository(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Skill>> GetAllAsync() => await _dbContext.Skills.ToListAsync();
        public async Task AddSkillFromProjectAsync(Project project)
        {
            var words = project.Description.Split(' ');
            var length = words.Length;
            var skill = $"Project {project.Id} - {words[length - 1]}";
            // "1 - Marketplace"

            await _dbContext.Skills.AddAsync(new Skill(skill));
        }
    }
}