namespace DevFreela.Application.InputModels
{
    public class CreateUserInputModel
    {
        public CreateUserInputModel(string fullName, string email, DateTime birthDate)
        {
            FullName = fullName;
            Email = email;
            BirthDate = birthDate;
        }

        public string FullName { get; private set; }
        public string Password { get; private set; }
        public string Email { get; private set; }
        public DateTime BirthDate { get; private set; }
    }
}
