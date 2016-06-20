using System;
using System.Configuration;
using System.Data.Common;
using System.Threading.Tasks;
using DrawPub.Core.Log;
using MySql.Data.MySqlClient;


namespace DrawPub.Repository
{
    public class MySQLConnectionProvider : IConnectionProvider
    {
        private ILogBase logger;

        public MySQLConnectionProvider(ILogFactory logFactory)
        {
            logger = logFactory.CreateLog(typeof (MySQLConnectionProvider));
        }

        public async Task<DbConnection> GetAsync()
        {
            var connectionString = ConfigurationManager.AppSettings["User_Database"];

            MySqlConnection connection= new MySqlConnection();

            if (connectionString != null)
            {
                logger.DebugFormat("User_Database:{0}", connectionString);

                connection.ConnectionString = connectionString;
                return connection;
            }

            throw new ApplicationException(string.Format("There is no User_Database config for mysql connection_string"));
        }
    }
}