using System;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    public class _DbConnection
    {
        public static MySqlConnection Connect()
        {
            //this creates a database to connect to
            var dbConn = new MySqlConnection("server=ADRESS;database=DATABASENAME;uid=USERNAME;pwd=PASSWORD;");
            try
            {
                dbConn.Open();
                Console.WriteLine("Connection successful");
                dbConn.Close();
                return dbConn;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Connection failed error message: " + ex);
                throw;
            }
        }
    }
}