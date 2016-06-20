using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using DrawPub.Domain;
using DrawPub.Domain.RepositoryInterface;
using MySql.Data.MySqlClient;


namespace DrawPub.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IConnectionProvider _connectionProvider;


        public UserRepository(IConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public async Task<int> CreateUserAsync(string userName, string password)
        {
            string query = @"INSERT INTO
                            usercredential
                                (Id,
                                 UserName,
                                 Password,
                                 CreatedById,
                                 CreatedDate,
                                 ModifiedById,
                                 ModifiedDate)
                            VALUES
                                (?UserName,
                                ?Password,
                                ?CreatedById,
                                ?CreatedDate,
                                ?ModifiedById,
                                ?ModifiedDate);
                            Select LAST_INSERT_ID();";



            MySqlCommand command=new MySqlCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("UserName", userName);
            command.Parameters.AddWithValue("Password", password);
            command.Parameters.AddWithValue("CreatedById", 1);
            command.Parameters.AddWithValue("CreatedDate", DateTime.UtcNow);
            command.Parameters.AddWithValue("ModifiedById", 1);
            command.Parameters.AddWithValue("ModifiedDate", DateTime.UtcNow);

            var userId = "0";

            using (MySqlConnection connection = (MySqlConnection)await _connectionProvider.GetAsync())
            {
                command.Connection = connection;
                await connection.OpenAsync();

                userId = command.ExecuteScalar().ToString();

                connection.Close();
            }

            return Convert.ToInt32(userId);
        }

        public bool AddLoginAsync(string LoginProvider, string providerKey)
        {
            throw new NotImplementedException();
        }

        public async Task<int> AuthenticateAsync(string userName, string password)
        {
            int userId = 0;
            
            string query = @"SELECT
                                UserId
                             FROM 
                                usercredential
                              WHERE
                                Username = ?username
                              AND
                                Password = ?Password;";


            MySqlCommand command = new MySqlCommand();

            command.CommandText = query;

            command.Parameters.AddWithValue("UserName", userName);
            command.Parameters.AddWithValue("Password", password);

            using (MySqlConnection connection = (MySqlConnection)await _connectionProvider.GetAsync())
            {
                command.Connection = connection;
                await connection.OpenAsync();
                
                using (var reader = command.ExecuteReader())
                {
                    if(reader.Read())
                        userId= reader.GetInt32("UserId");
                    else
                    {
                        throw new AuthenticationException("Username or password is not correct!");
                    }
                }
               await connection.CloseAsync();
            }

            
            return userId;
        }

        public async Task<User> Get(int userId)
        {
           User user=new User(userId);

            string query = @"SELECT
                                FirstName,
                                Surname
                             FROM 
                                user
                              WHERE
                                UserId = ?UserId;";


            MySqlCommand command = new MySqlCommand();

            command.CommandText = query;

            command.Parameters.AddWithValue("UserId", userId);


            using (MySqlConnection connection = (MySqlConnection)await _connectionProvider.GetAsync())
            {
                command.Connection = connection;
                await connection.OpenAsync();

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user.FirstName = reader.GetString("FirstName");
                        user.Surname = reader.GetString("Surname");
                    }

                }
                await connection.CloseAsync();
            }


            var roles = await GetUserRole(user.Id);

            user.Roles = roles.ToList();

            return user;

        }

        public async Task<IList<string>> GetUserRole(int userId)
        {
            IList<string> roles = new List<string>();

            string query = @"SELECT
                                Role
                             FROM 
                                userrole
                              WHERE
                                UserId = ?UserId;";


            MySqlCommand command = new MySqlCommand();

            command.CommandText = query;

            command.Parameters.AddWithValue("UserId", userId);

            using (MySqlConnection connection = (MySqlConnection)await _connectionProvider.GetAsync())
            {
                command.Connection = connection;
                await connection.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        roles.Add(reader.GetString(0));
                    }
                }

                await connection.CloseAsync();
            }

            return roles;

        }

    }
}
