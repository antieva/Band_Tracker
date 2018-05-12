using System;
using MySql.Data.MySqlClient;
using BandTrackerApp;

namespace BandTrackerApp
{
    public class DB
    {
        public static MySqlConnection Connection()
        {
            MySqlConnection conn = new MySqlConnection(DBConfiguration.ConnectionString);
            return conn;
        }
    }
}
