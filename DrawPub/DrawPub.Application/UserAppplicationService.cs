using System;
using System.Threading.Tasks;
using DrawPub.Domain;
using DrawPub.Domain.RepositoryInterface;


namespace DrawPub.Application
{
    public class UserAppplicationService : IUserAppplicationService 
    {
        private readonly IUserRepository _userRepository;

        public UserAppplicationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<int> CreateUserAsync(string userName, string password)
        {
            return _userRepository.CreateUserAsync(userName, password);
        }

        public async Task<User> AuthenticateUserAsync(string userName, string password)
        {
            int userId= await _userRepository.AuthenticateAsync( userName,  password);

            User user = await _userRepository.Get(userId);

            return user;
        }
    }
}
