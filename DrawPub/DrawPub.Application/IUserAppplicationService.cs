using System.Threading.Tasks;
using DrawPub.Domain;


namespace DrawPub.Application
{
    public interface IUserAppplicationService
    {
        Task<int> CreateUserAsync(string userName, string password);

        Task<User> AuthenticateUserAsync(string userName, string password);

    }
}
