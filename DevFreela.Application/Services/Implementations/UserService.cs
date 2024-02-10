using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
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

        public UserViewModel GetUser(int id)
        {
            var user = _dbContext.Users.SingleOrDefault(x => x.Id == id);
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
