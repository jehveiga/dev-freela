using DevFreela.Core.Enums;

namespace DevFreela.Core.Entities
{
    public class Project(string title, string description, int idClient, int idFreelancer, decimal? totalCost) : BaseEntity
    {
        public string Title { get; private set; } = title;
        public string Description { get; private set; } = description;
        public int IdClient { get; private set; } = idClient;
        public User? Client { get; private set; }
        public int IdFreelancer { get; private set; } = idFreelancer;
        public User? Freelancer { get; private set; }
        public decimal? TotalCost { get; private set; } = totalCost;
        public DateTime CreatedAt { get; private set; } = DateTime.Now;
        public DateTime? StartedAt { get; private set; }
        public DateTime? FinisishedAt { get; private set; }
        public ProjectStatus Status { get; private set; } = ProjectStatus.Created;
        public IList<ProjectComment> Comments { get; private set; } = new List<ProjectComment>();

        public void Cancel()
        {
            if (Status == ProjectStatus.Created || Status == ProjectStatus.InProgress)
                Status = ProjectStatus.Cancelled;
        }

        public void Finish()
        {
            if (Status == ProjectStatus.PaymentPending)
            {
                Status = ProjectStatus.Finished;
                FinisishedAt = DateTime.Now;
            }
        }

        public void Start()
        {
            if (Status == ProjectStatus.Created)
            {
                Status = ProjectStatus.InProgress;
                StartedAt = DateTime.Now;
            }
        }

        public void SetPaymentPerding()
        {
            if (Status == ProjectStatus.InProgress)
            {
                Status = ProjectStatus.PaymentPending;
                FinisishedAt = DateTime.Now;
            }

        }

        public void Update(string title, string description, decimal? totalCost)
        {
            Title = title;
            Description = description;
            TotalCost = totalCost;
        }
    }
}