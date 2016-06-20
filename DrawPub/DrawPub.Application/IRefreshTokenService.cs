using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrawPub.Domain;


namespace DrawPub.Application
{
    interface IRefreshTokenService
    {
        Task<Client> FindClient(string clientId);

        Task<bool> AddRefreshToken(RefreshToken token);

        Task<bool> RemoveRefreshToken(string  refreshTokenId);

        Task<RefreshToken> FindRefreshToken(string refreshTokenId);

        List<RefreshToken> GetAllRefreshToken();

    }
}
