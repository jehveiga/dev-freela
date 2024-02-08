using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Services.Implementations
{
    public class ProjectService : IProjectService
    {
        private readonly DevFreelaDbContext _dbContext;

        public ProjectService(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<ProjectViewModel> GetAll(string query)
        {
            var projects = _dbContext.Projects;

            var projectsViewModel = projects.Select(p => new ProjectViewModel(p.Id, p.Title, p.CreatedAt)).ToList();

            return projectsViewModel;
        }

        public ProjectDetailsViewModel GetById(int id)
        {
            var project = _dbContext.Projects
                                    .Include(p => p.Cliente)
                                    .Include(p => p.Freelancer)
                                    .SingleOrDefault(p => p.Id == id);

            if (project == null)
                return null;

            var projectDetailsViewModel = new ProjectDetailsViewModel(
                id: project.Id,
                title: project.Title,
                description: project.Description,
                totalCost: project.TotalCost,
                startedAt: project.StartedAt,
                finishedAt: project.FinisishedAt,
                clientFullName: project.Cliente.FullName,
                freelancerFullName: project.Freelancer.FullName);

            return projectDetailsViewModel;
        }

        public int Create(CreateProjectInputModel inputModel)
        {
            var project = new Project(title: inputModel.Title,
                                      description: inputModel.Description,
                                      idClient: inputModel.IdClient,
                                      idFreelancer: inputModel.IdFreelancer,
                                      totalCost: inputModel.TotalCost);

            _dbContext.Projects.Add(project);
            _dbContext.SaveChanges();

            return project.Id;
        }

        public void CreateComment(CreateCommentInputModel inputModel)
        {
            var comment = new ProjectComment(content: inputModel.Content,
                                             idProject: inputModel.IdProject,
                                             idUser: inputModel.IdUser);

            _dbContext.ProjectComments.Add(comment);
            _dbContext.SaveChanges();
        }

        public void Update(UpdateProjectInputModel inputModel)
        {
            var project = _dbContext.Projects.SingleOrDefault(x => x.Id == inputModel.Id);

            project.Update(project.Title, project.Description, project.TotalCost);
            _dbContext.SaveChanges();

        }

        public void Delete(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(x => x.Id == id);

            project.Cancel();
            _dbContext.SaveChanges();

        }

        public void Start(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(x => x.Id == id);

            project.Start();
            _dbContext.SaveChanges();

        }

        public void Finish(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(x => x.Id == id);

            project.Finish();
            _dbContext.SaveChanges();

        }
    }
}
