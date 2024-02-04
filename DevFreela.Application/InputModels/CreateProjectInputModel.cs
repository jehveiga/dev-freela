namespace DevFreela.Application.InputModels
{
    public class CreateProjectInputModel
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int IdClient { get; set; }
        public int IdFreelancer { get; set; }
        public decimal? TotalCost { get; set; }
    }
}
