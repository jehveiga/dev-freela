using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(
            DevFreelaDbContext dbContext,
            IProjectRepository projects,
            IUserRepository users,
            ISkillRepository skills)
        {
            _dbContext = dbContext;
            Projects = projects;
            Users = users;
            Skills = skills;
        }
        private IDbContextTransaction _transaction;
        private readonly DevFreelaDbContext _dbContext;
        public IProjectRepository Projects { get; }

        public IUserRepository Users { get; }

        public ISkillRepository Skills { get; }

        public async Task<int> CompleteAsync() => await _dbContext.SaveChangesAsync();

        #region Padrão de transações
        public async Task BeginTransactionAsync()
        {
            _transaction = await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            try
            {
                await _transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await _transaction.RollbackAsync();
                throw ex;
            }
        }
        #endregion

        #region Implementação do Dispose
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
        #endregion
    }

}
