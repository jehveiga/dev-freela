﻿using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Core.Entities;
using DevFreela.Core.Models;
using DevFreela.Core.Repositories;
using NSubstitute;

namespace DevFreela.UnitTest.Application.Queries
{
    public class GetAllProjectsCommandHandlerTests
    {
        [Fact]
        public async Task ThreeProjectsExist_Executed_ReturnThreeProjectViewModels()
        {
            // Arrange
            var projects = new PaginationResult<Project>
            {
                Data = new List<Project>{
                    new("Nome do Teste 1", "Descricao de Teste 1", 1, 2, 10000),
                    new("Nome do Teste 2", "Descricao de Teste 2", 1, 2, 20000),
                    new("Nome do Teste 3", "Descricao de Teste 3", 1, 2, 30000)
                }
            };


            string queryMock = string.Empty;
            var projectRepositoryMock = Substitute.For<IProjectRepository>();
            projectRepositoryMock.GetAllAsync(queryMock).Returns(projects);

            var getAllProjectsQuery = new GetAllProjectsQuery { Query = "", Page = 1 };
            var getAllProjectsQueryHandler = new GetAllProjectsQueryHandler(projectRepositoryMock);
            var cancellationToken = new CancellationToken();

            // Act
            var paginationProjectViewModelList = await getAllProjectsQueryHandler.Handle(getAllProjectsQuery, cancellationToken);

            // Assert
            Assert.NotNull(paginationProjectViewModelList);
            Assert.NotEmpty(paginationProjectViewModelList.Data);
            Assert.Equal(projects.Data.Count, paginationProjectViewModelList.Data.Count);

            _ = projectRepositoryMock.Received().GetAllAsync(queryMock);

        }
    }
}
