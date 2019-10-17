using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using domain.models.dto;
using MySql.Data.MySqlClient;

namespace domain.repository
{
    public class MyDapper : IDisposable
    {
        public static string baixiaosheng1 = "Server=39.100.98.114;Database=baixiaosheng_1;User=guo;Password=Yaya123...";
        private readonly IDbConnection dbConnection;
        public MyDapper()
        {
            if (dbConnection == null)
            {
                dbConnection = new MySqlConnection(baixiaosheng1);
            }
        }
        public void Dispose()
        {
            dbConnection.Close();
        }
    }
}