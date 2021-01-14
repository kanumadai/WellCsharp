using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.DbUtils
{
    static class DataSource
    {
        public static string connectStr = String.Format("server={0}; port={1};user={2};password={3}; database={4};",
                        ConfigurationManager.AppSettings["server"],
                        ConfigurationManager.AppSettings["port"],
                        ConfigurationManager.AppSettings["username"],
                        ConfigurationManager.AppSettings["password"],
                        ConfigurationManager.AppSettings["database"]);

        private static MySqlConnection conn = null;

        public static MySqlConnection getConnection()
        {
            
            if (conn == null)
            {
                conn = new MySqlConnection(connectStr);
            }
            return conn;
        }

    }
}
