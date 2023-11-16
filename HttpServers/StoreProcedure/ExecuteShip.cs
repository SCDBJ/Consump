using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServers.StoreProcedure
{
    public class ExecuteShip
    {
        string connectionString = "";
        public ExecuteShip()
        {
            connectionString = ConfigurationManager.AppSettings["ConnectionString"];
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public DataTable SelecShipCommand()
        {
            DataSet ds = new DataSet();

            SqlConnection myConnection = new SqlConnection(connectionString);
            if (myConnection.State != ConnectionState.Open)
            {
                myConnection.Open();
            }
            SqlCommand myCommand = new SqlCommand("GetShip", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.ExecuteNonQuery();

            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);
            adapter.Fill(ds);
            if (myConnection.State == ConnectionState.Open)
            {
                myConnection.Close();
            }
            if (ds.Tables.Count > 0)
            {
                DataTable dataTable = ds.Tables[0];
                return dataTable;
            }
            return null;
        }
        public string InsertShipCommand(string companyName,string shipName,string contact,string phone)
        {
            SqlConnection myConnection = new SqlConnection(connectionString);
            if (myConnection.State != ConnectionState.Open)
            {
                myConnection.Open();
            }
            SqlCommand myCommand = new SqlCommand("AddShip", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            myCommand.Parameters.Add("@companyName", SqlDbType.VarChar);
            myCommand.Parameters["@companyName"].Value = companyName;
            myCommand.Parameters.Add("@shipName", SqlDbType.VarChar);
            myCommand.Parameters["@shipName"].Value = shipName;
            myCommand.Parameters.Add("@contact", SqlDbType.VarChar);
            myCommand.Parameters["@contact"].Value = contact;
            myCommand.Parameters.Add("@phone", SqlDbType.VarChar);
            myCommand.Parameters["@phone"].Value = phone;

            SqlParameter output = myCommand.Parameters.Add("@result", SqlDbType.Int);
            output.Direction = ParameterDirection.Output;

            myCommand.ExecuteNonQuery();
            string resultValue = output.Value.ToString();
            if (myConnection.State == ConnectionState.Open)
            {
                myConnection.Close();
            }
            return resultValue;
        }
        public string DisableShipCommand(string companyName, string shipName,string type)
        {
            SqlConnection myConnection = new SqlConnection(connectionString);
            if (myConnection.State != ConnectionState.Open)
            {
                myConnection.Open();
            }
            SqlCommand myCommand = new SqlCommand("DisableShip", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            myCommand.Parameters.Add("@companyName", SqlDbType.VarChar);
            myCommand.Parameters["@companyName"].Value = companyName;
            myCommand.Parameters.Add("@shipName", SqlDbType.VarChar);
            myCommand.Parameters["@shipName"].Value = shipName;
            myCommand.Parameters.Add("@type", SqlDbType.VarChar);
            myCommand.Parameters["@type"].Value = type;


            SqlParameter output = myCommand.Parameters.Add("@result", SqlDbType.Int);
            output.Direction = ParameterDirection.Output;

            myCommand.ExecuteNonQuery();
            string resultValue = output.Value.ToString();
            if (myConnection.State == ConnectionState.Open)
            {
                myConnection.Close();
            }
            return resultValue;
        }
        public string ChangeShipCommand(string companyName, string shipName, string contact, string phone,string oldShipName)
        {
            SqlConnection myConnection = new SqlConnection(connectionString);
            if (myConnection.State != ConnectionState.Open)
            {
                myConnection.Open();
            }
            SqlCommand myCommand = new SqlCommand("ChangeShip", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            myCommand.Parameters.Add("@companyName", SqlDbType.VarChar);
            myCommand.Parameters["@companyName"].Value = companyName;
            myCommand.Parameters.Add("@shipName", SqlDbType.VarChar);
            myCommand.Parameters["@shipName"].Value = shipName;
            myCommand.Parameters.Add("@contact", SqlDbType.VarChar);
            myCommand.Parameters["@contact"].Value = contact;
            myCommand.Parameters.Add("@phone", SqlDbType.VarChar);
            myCommand.Parameters["@phone"].Value = phone;
            myCommand.Parameters.Add("@oldShipName", SqlDbType.VarChar);
            myCommand.Parameters["@oldShipName"].Value = oldShipName;

            SqlParameter output = myCommand.Parameters.Add("@result", SqlDbType.Int);
            output.Direction = ParameterDirection.Output;

            myCommand.ExecuteNonQuery();
            string resultValue = output.Value.ToString();
            if (myConnection.State == ConnectionState.Open)
            {
                myConnection.Close();
            }
            return resultValue;
        }
    }
}
