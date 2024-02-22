using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence.Repositories;
using NSubstitute;

namespace DevFreela.UnitTest.Application.Queries
{
    public class GetAllProjectsCommandHandlerTests
    {
        [Fact]
        public async Task ThreeProjectsExist_Executed_ReturnThreeProjectViewModels()
        {
            // Arrange
            var projects = new List<Project>
            {
                new("Nome do Teste 1", "Descricao de Teste 1", 1, 2, 10000),
                new("Nome do Teste 2", "Descricao de Teste 2", 1, 2, 20000),
                new("Nome do Teste 3", "Descricao de Teste 3", 1, 2, 30000),
            };

            var projectRepositoryMock = Substitute.For<IProjectRepository>();
            projectRepositoryMock.GetAllAsync().Returns(projects);

            var getAllProjectsQuery = new GetAllProjectsQuery("");
            var getAllProjectsQueryHandler = new GetAllProjectsQueryHandler(projectRepositoryMock);
            var cancellationToken = new CancellationToken();

            // Act
            var projectViewModelList = await getAllProjectsQueryHandler.Handle(getAllProjectsQuery, cancellationToken);

            // Assert
            Assert.NotNull(projectViewModelList);
            Assert.NotEmpty(projectViewModelList);
            Assert.Equal(projects.Count, projectViewModelList.Count);

            _ = projectRepositoryMock.Received().GetAllAsync();

        }
    }
}
