using DevFreela.Application.Commands.CreateProject;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence.Repositories;
using NSubstitute;

namespace DevFreela.UnitTest.Application.Commands
{
    public class CreateProjectCommandHandlerTests
    {
        [Fact]
        public async Task InputDataIsOk_Executed_ReturnProjectId()
        {
            // Arrange
            var projectRepositoryMock = Substitute.For<IProjectRepository>();

            var createProjectCommand = new CreateProjectCommand
            {
                Title = "Titulo de Teste",
                Description = "Description de Teste",
                IdClient = 1,
                IdFreelancer = 2,
                TotalCost = 5000
            };

            var createProjectCommandHandler = new CreateProjectCommandHandler(projectRepositoryMock);
            var cancellationToken = new CancellationToken();
            // Act
            var id = await createProjectCommandHandler.Handle(createProjectCommand, cancellationToken);

            // Assert
            Assert.True(id >= 0);

            await projectRepositoryMock.Received().AddAsync(Arg.Any<Project>());
        }
    }
}
