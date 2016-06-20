
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using DrawPub.Domain;
using DrawPub.Domain.RepositoryInterface;
using MySql.Data.MySqlClient;


namespace DrawPub.Repository
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly IConnectionProvider _connectionProvider;

        public RefreshTokenRepository(IConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public async Task<Client> FindClient(string clientId)
        {
            Client client=new Client();
            client.Id = clientId;

            string query = @"SELECT
                                Id,
                                Secret,
                                Name,
                                ApplicationType,
                                Active,
                                RefreshTokenLifeTime,
                                AllowedOrigin
                             FROM 
                                refreshtoken
                              WHERE
                                clientId = ?clientId;";


            MySqlCommand command = new MySqlCommand();

            command.CommandText = query;

            command.Parameters.AddWithValue("clientId", clientId);
            

            using (MySqlConnection connection = (MySqlConnection) await _connectionProvider.GetAsync())
            {
                command.Connection =connection;
                await connection.OpenAsync();

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        client.Secret = reader.GetString("Secret");
                        client.Name = reader.GetString("Name");
                        client.ApplicationType =(ApplicationTypes) reader.GetInt16("ApplicationType");
                        client.Active = reader.GetBoolean("Active");
                        client.RefreshTokenLifeTime = reader.GetInt32("RefreshTokenLifeTime");
                        client.AllowedOrigin = reader.GetString("AllowedOrigin");
                    }
                    else
                    {
                        throw new ApplicationException(string.Format("ClientId does not exist!", clientId));
                    }

                }

                await connection.CloseAsync();
            }


            return client;
        }

        public Task<bool> AddRefreshToken(RefreshToken token)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> RemoveRefreshToken(string refreshTokenId)
        {
            throw new System.NotImplementedException();
        }

        public Task<RefreshToken> FindRefreshToken(string refreshTokenId)
        {
            throw new System.NotImplementedException();
        }

        public List<RefreshToken> GetAllRefreshToken()
        {
            throw new System.NotImplementedException();
        }
    }
}
