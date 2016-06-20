using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawPub.Domain.RepositoryInterface
{
    public  interface IUserRepository
    {
        /// <summary>
        /// Creating a user
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns>created userID</returns>
        Task<int> CreateUserAsync(string userName, string password);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="LoginProvider"></param>
        /// <param name="providerKey"></param>
        /// <returns></returns>
        bool AddLoginAsync(string LoginProvider, string providerKey);


        /// <summary>
        /// Get userId of the user if exists
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<int> AuthenticateAsync(string userName, string password);

        /// <summary>
        /// Get Firstname and lastname and user roles
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<User> Get(int userId);

        /// <summary>
        /// Get user roles
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IList<string>> GetUserRole(int userId);
    }
}
