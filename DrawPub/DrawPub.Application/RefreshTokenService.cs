using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrawPub.Domain;
using DrawPub.Domain.RepositoryInterface;


namespace DrawPub.Application
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenRepository _repository;

        public RefreshTokenService(IRefreshTokenRepository repository)
        {
            _repository = repository;
        }

        public Task<Client> FindClient(string clientId)
        {
            return _repository.FindClient(clientId);
        }

        public Task<bool> AddRefreshToken(RefreshToken token)
        {
            return _repository.AddRefreshToken(token);
        }

        public Task<bool> RemoveRefreshToken(string refreshTokenId)
        {
            return _repository.RemoveRefreshToken(refreshTokenId);
        }

        public Task<RefreshToken> FindRefreshToken(string refreshTokenId)
        {
            return _repository.FindRefreshToken(refreshTokenId);
        }

        public List<RefreshToken> GetAllRefreshToken()
        {
            return _repository.GetAllRefreshToken();
        }
    }
}
