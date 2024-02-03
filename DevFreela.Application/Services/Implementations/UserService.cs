using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;

namespace DevFreela.Application.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly DevFreelaDbContext _dbContext;

        public UserService(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(CreateUserInputModel inputModel)
        {
            var user = new User(inputModel.FullName, inputModel.Email, inputModel.BirthDate);

            _dbContext.Users.Add(user);

            return user.Id;
        }

        public UserViewModel GetUser(int id)
        {
            var user = _dbContext.Users.Find(x => x.Id == id);

            if (user == null)
            {
                return null;
            }

            var userViewModel = new UserViewModel(
                                    fullName: user.FullName,
                                    email: user.Email);

            return userViewModel;
        }
    }
}
