using System.Data;
using domain.configs;
using domain.entitys;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;

namespace application.services.bases
{
    public class BaseService1 : DbRepository<baixiaosheng_1Context>
    {

        protected readonly IDbConnection dbConnection;
        public BaseService1(IOptions<ConnectionStringList> connectionStrings)
        {
            if (dbConnection == null)
            {
                dbConnection = new MySqlConnection(connectionStrings.Value.baixiaosheng1);
            }
        }
        public void Dispose()
        {
            dbConnection.Close();
        }
    }
}