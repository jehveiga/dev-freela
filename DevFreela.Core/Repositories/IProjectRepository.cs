using DevFreela.Core.Entities;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public interface IProjectRepository
    {
        Task<List<Project>> GetAllAsync();

        Task<Project> GetByIdAsync(int id);
    }
}