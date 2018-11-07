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
                var floatData = FormatData(data);
                SendData(floatData, dbConn);
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }
        }

        private void SendData(float[] data, MySqlConnection dbConn)
        {
            var sqlStmt = new MySqlCommand
            {
                CommandText =
                    "INSERT INTO milkdata(date,milkings,milkamount,avrmilkamount) VALUES(?date,?milkings,?milkamount,?avrmilkamount)"
            };
            sqlStmt.Connection = dbConn;

            MySqlDbType[] dbTypes =
            {
                MySqlDbType.Int16,
                MySqlDbType.Float,
                MySqlDbType.Float
            };
            string[] tableVar =
            {
                "?milkings",
                "?milkamount",
                "?avrmilkamount"
            };
            sqlStmt.Parameters.Add("?date", MySqlDbType.Date).Value = DateTime.Today;
            for (var i = 0; i < data.Length; i++)
                if (i == 0)
                    sqlStmt.Parameters.Add(tableVar[i], dbTypes[i]).Value = (int) data[0];
                else
                    sqlStmt.Parameters.Add(tableVar[i], dbTypes[i]).Value = data[i];
            sqlStmt.ExecuteNonQuery();
            dbConn.Close();
        }

        private float[] FormatData(string[] data)
        {
            var output = new float[data.Length];

            for (var i = 0; i < data.Length; i++) output[i] = float.Parse(data[i]);
            return output;
        }
    }
}