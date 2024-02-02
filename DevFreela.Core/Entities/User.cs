namespace DevFreela.Core.Entities
{
    public class User : BaseEntity
    {
        public User(string fullName, string email, DateTime birthDate)
        {
            FullName = fullName;
            Email = email;
            BirthDate = birthDate;
            CreatedAt = DateTime.Now;
            Active = true;

            Skills = new List<UserSkill>();
            OwnedProjects = new List<Project>();
            FreelanceProjects = new List<Project>();
        }

        public string FullName { get; private set; }
        public string Email { get; private set; }
        public DateTime BirthDate { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool Active { get; set; }

        public IList<UserSkill> Skills { get; private set; }
        public IList<Project> OwnedProjects { get; private set; }
        public IList<Project> FreelanceProjects { get; set; }
    }
}
