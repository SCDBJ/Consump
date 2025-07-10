using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using HttpServers.Model.Salary;

namespace HttpServers.StoreProcedure
{
    public class ExecuteAutoAccount
    {
        string connectionString;
        public ExecuteAutoAccount()
        {
            connectionString = ConfigurationManager.AppSettings["ConnectionString"];
        }
        public int AutoAccountCommand()
        {
            SqlConnection myConnection = new SqlConnection(connectionString);
            if (myConnection.State != ConnectionState.Open)
            {
                myConnection.Open();
            }
            SqlCommand myCommand = new SqlCommand("AutoAccount", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            int resultValue = myCommand.ExecuteNonQuery();
            if (myConnection.State == ConnectionState.Open)
            {
                myConnection.Close();
            }
            return resultValue;
        }
    }
}
