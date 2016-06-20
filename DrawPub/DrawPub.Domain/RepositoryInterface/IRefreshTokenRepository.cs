using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawPub.Domain.RepositoryInterface
{
    public interface IRefreshTokenRepository
    {
        Task<Client> FindClient(string clientId);

        Task<bool> AddRefreshToken(RefreshToken token);

        Task<bool> RemoveRefreshToken(string refreshTokenId);

        Task<RefreshToken> FindRefreshToken(string refreshTokenId);

        List<RefreshToken> GetAllRefreshToken();
    }
}
