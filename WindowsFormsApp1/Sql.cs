using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    public class Sql
    {
        public bool Upload(string[] data)
        {
            var dbConn = DbConnection.Connect();
            try
            {
                dbConn.Open();
                data = FormatData(data);
                //SendData(data, dbConn);
                MessageBox.Show(data.ToString());
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return false;
            } 
        }

        private void SendData(string[] data, MySqlConnection dbConn)
        {
            
            throw new NotImplementedException();
        }

        private string[] FormatData(string[] data)
        {
            string[] output = new string[data.Length+1];
            output[0] = DateTime.Today.ToString("dd-MM-yyyy");
            for (int i = 0; i < data.Length; i++)
            {
                output[i + 1] = data[i];
            }
            return output;
        }
    }
}