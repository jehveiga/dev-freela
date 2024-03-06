using DevFreela.Core.Repositories;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(
            DevFreelaDbContext dbContext,
            IProjectRepository projects,
            IUserRepository users)
        {
            _dbContext = dbContext;
            Projects = projects;
            Users = users;
        }
        private readonly DevFreelaDbContext _dbContext;
        public IProjectRepository Projects { get; }

        public IUserRepository Users { get; }

        public async Task<int> CompleteAsync() => await _dbContext.SaveChangesAsync();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }
    }

}
