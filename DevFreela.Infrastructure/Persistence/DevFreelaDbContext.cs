using DevFreela.Core.Entities;

namespace DevFreela.Infrastructure.Persistence
{
    public class DevFreelaDbContext
    {
        public DevFreelaDbContext()
        {
            // Listas que será usada para testes
            Projects =
            [
                new("Meu projeto ASPNET Core 1", "Minha Descrição de Projeto 1", 1, 1, 10000),
                new("Meu projeto ASPNET Core 2", "Minha Descrição de Projeto 2", 1, 1, 20000),
                new("Meu projeto ASPNET Core 3", "Minha Descrição de Projeto 3", 1, 1, 30000)
            ];

            Users =
            [
                new("Batman", "batman@empresawayne.com", new DateTime(1985, 1, 1)),
                new("Coringa", "coringa@loucuras.com", new DateTime(1982, 3, 13)),
                new("Robin", "robin@empresawayne.com", new DateTime(1997, 5, 25))
            ];

            Skills =
                [
                    new(".NET Core"),
                    new("C#"),
                    new("SQL"),
                ];
        }

        public List<Project> Projects { get; set; }
        public List<User> Users { get; set; }
        public List<Skill> Skills { get; set; }
    }
}
