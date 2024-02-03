using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;

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

            var projectsViewModel = projects.Select(p => new ProjectViewModel(id: p.Id, title: p.Title, createdAt: p.CreatedAt)).ToList();

            return projectsViewModel;
        }

        public ProjectDetailsViewModel GetById(int id)
        {
            var project = _dbContext.Projects.Find(x => x.Id == id);

            var projectDetailsViewModel = new ProjectDetailsViewModel(
                id: project.Id,
                title: project.Title,
                description: project.Description,
                totalCost: project.TotalCost,
                startedAt: project.StartedAt,
                finishedAt: project.FinisishedAt);

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

            return project.Id;
        }

        public void CreateComment(CreateCommentInputModel inputModel)
        {
            var comment = new ProjectComment(content: inputModel.Content,
                                             idProject: inputModel.IdProject,
                                             idUser: inputModel.IdUser);

            _dbContext.ProjectComments.Add(comment);
        }

        public void Update(UpdateProjectInputModel inputModel)
        {
            var project = _dbContext.Projects.Find(x => x.Id == inputModel.Id);

            project.Update(project.Title, project.Description, project.TotalCost);
        }

        public void Delete(int id)
        {
            var project = _dbContext.Projects.Find(x => x.Id == id);

            project.Cancel();
        }

        public void Start(int id)
        {
            var project = _dbContext.Projects.Find(x => x.Id == id);

            project.Start();
        }

        public void Finish(int id)
        {
            var project = _dbContext.Projects.Find(x => x.Id == id);

            project.Finish();
        }
    }
}
