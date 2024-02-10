using DevFreela.Core.DTOs;
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

        public async Task<List<SkillDto>> GetAllAsync() => await _dbContext.UserSkills
                                                                            .Include(x => x.Skill)
                                                                            .Select(userSkill => new SkillDto
                                                                            {
                                                                                Id = userSkill.Id,
                                                                                Description = userSkill.Skill.Description
                                                                            }).ToListAsync();
    }
}
