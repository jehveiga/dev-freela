using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DevFreela.Infrastructure.Persistence
{
    public class DevFreelaDbContext(DbContextOptions<DevFreelaDbContext> options) : DbContext(options)
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<UserSkill> UserSkills { get; set; }
        public DbSet<ProjectComment> ProjectComments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Adicionando todas as configurações da criação do banco registradas, através da herança da interface IEntityTypeConfiguration<T>
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}