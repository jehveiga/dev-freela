using DevFreela.Core.Entities;
using DevFreela.Core.Enums;

namespace DevFreela.UnitTest.Core.Entities
{
    public class ProjectTests
    {
        [Fact]
        public void TestIfProjectStartWorks()
        {
            var project = new Project("Nome de Teste", "Descricao de Teste", 1, 2, 10000);

            // Validaçao de inicialização do objeto
            Assert.Equal(ProjectStatus.Created, project.Status);
            Assert.Null(project.StartedAt);

            Assert.NotNull(project.Title);
            Assert.NotEmpty(project.Title);

            Assert.NotNull(project.Description);
            Assert.NotEmpty(project.Description);

            project.Start();

            // Validaçao após a chamada do método do Status do objeto 
            Assert.Equal(ProjectStatus.InProgress, project.Status);
            Assert.NotNull(project.StartedAt);
        }
    }
}
