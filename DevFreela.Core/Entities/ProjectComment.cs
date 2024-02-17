namespace DevFreela.Core.Entities
{
    public class ProjectComment(string content, int idProject, int idUser) : BaseEntity
    {
        public string Content { get; private set; } = content;
        public int IdProject { get; private set; } = idProject;
        public Project? Project { get; private set; }
        public int IdUser { get; private set; } = idUser;
        public User? User { get; private set; }
        public DateTime CreatedAt { get; private set; }
    }
}