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
    public class ExecuteUserCardInfo
    {
        string connectionString = "";
        public ExecuteUserCardInfo()
        {
            connectionString = ConfigurationManager.AppSettings["ConnectionString"];
        }
        public string AddUserCardInfoCommand(string cardID, string shipCode, string companyName)
        {
            SqlConnection myConnection = new SqlConnection(connectionString);
            if (myConnection.State != ConnectionState.Open)
            {
                myConnection.Open();
            }
            SqlCommand myCommand = new SqlCommand("AddUserCardInfo", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            myCommand.Parameters.Add("@companyName", SqlDbType.VarChar);
            myCommand.Parameters["@companyName"].Value = companyName;
            myCommand.Parameters.Add("@shipCode", SqlDbType.VarChar);
            myCommand.Parameters["@shipCode"].Value = shipCode;
            myCommand.Parameters.Add("@cardID", SqlDbType.VarChar);
            myCommand.Parameters["@cardID"].Value = cardID;

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
        public string UpdateUserCardInfoCommand(string cardID, string shipCode, string companyName, int ID)
        {
            SqlConnection myConnection = new SqlConnection(connectionString);
            if (myConnection.State != ConnectionState.Open)
            {
                myConnection.Open();
            }
            SqlCommand myCommand = new SqlCommand("UpdateUserCardInfo", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            myCommand.Parameters.Add("@companyName", SqlDbType.VarChar);
            myCommand.Parameters["@companyName"].Value = companyName;
            myCommand.Parameters.Add("@shipCode", SqlDbType.VarChar);
            myCommand.Parameters["@shipCode"].Value = shipCode;
            myCommand.Parameters.Add("@cardID", SqlDbType.VarChar);
            myCommand.Parameters["@cardID"].Value = cardID;
            myCommand.Parameters.Add("@ID", SqlDbType.VarChar);
            myCommand.Parameters["@ID"].Value = ID;

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
        public string DisableUserCardInfoCommand(string cardID, string type)
        {
            SqlConnection myConnection = new SqlConnection(connectionString);
            if (myConnection.State != ConnectionState.Open)
            {
                myConnection.Open();
            }
            SqlCommand myCommand = new SqlCommand("DisableUserCardInfo", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            myCommand.Parameters.Add("@cardID", SqlDbType.VarChar);
            myCommand.Parameters["@cardID"].Value = cardID;
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
        public DataTable GetAllCardsCommand()
        {
            DataSet ds = new DataSet();

            SqlConnection myConnection = new SqlConnection(connectionString);
            if (myConnection.State != ConnectionState.Open)
            {
                myConnection.Open();
            }
            SqlCommand myCommand = new SqlCommand("GetAllCardInfos", myConnection);
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
        public string InsertOpenGateCardNoCommand(string cardNo,string deviceNo,string deviceType)
        {
            SqlConnection myConnection = new SqlConnection(connectionString);
            if (myConnection.State != ConnectionState.Open)
            {
                myConnection.Open();
            }
            SqlCommand myCommand = new SqlCommand("InsertOpenGateCardNo", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            myCommand.Parameters.Add("@cardNo", SqlDbType.VarChar);
            myCommand.Parameters["@cardNo"].Value = cardNo;
            myCommand.Parameters.Add("@deviceNo", SqlDbType.VarChar);
            myCommand.Parameters["@deviceNo"].Value = deviceNo;
            myCommand.Parameters.Add("@deviceType", SqlDbType.VarChar);
            myCommand.Parameters["@deviceType"].Value = deviceType;

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
