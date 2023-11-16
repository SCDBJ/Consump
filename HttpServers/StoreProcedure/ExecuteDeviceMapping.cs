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
    public class ExecuteDeviceMapping
    {
        string connectionString = "";
        public ExecuteDeviceMapping()
        {
            connectionString = ConfigurationManager.AppSettings["ConnectionString"];
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public DataTable SelecDeviceMappingCommand(string type)
        {
            DataSet ds = new DataSet();

            SqlConnection myConnection = new SqlConnection(connectionString);
            if (myConnection.State != ConnectionState.Open)
            {
                myConnection.Open();
            }
            SqlCommand myCommand = new SqlCommand("GetDeviceMapping", myConnection);
            myCommand.Parameters.Add("@type", SqlDbType.VarChar);
            myCommand.Parameters["@type"].Value = type;
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
        public string InsertDeviceMappingCommand(string deviceName, string deviceNo, string deviceType)
        {
            SqlConnection myConnection = new SqlConnection(connectionString);
            if (myConnection.State != ConnectionState.Open)
            {
                myConnection.Open();
            }
            SqlCommand myCommand = new SqlCommand("AddDeviceMapping", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            myCommand.Parameters.Add("@deviceName", SqlDbType.VarChar);
            myCommand.Parameters["@deviceName"].Value = deviceName;
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
        public string DeleteDeviceMappingCommand(string deviceName, string deviceNo, string deviceType)
        {
            SqlConnection myConnection = new SqlConnection(connectionString);
            if (myConnection.State != ConnectionState.Open)
            {
                myConnection.Open();
            }
            SqlCommand myCommand = new SqlCommand("DeleteDeviceMapping", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            myCommand.Parameters.Add("@deviceName", SqlDbType.VarChar);
            myCommand.Parameters["@deviceName"].Value = deviceName;
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
        public string ChangeDeviceMappingCommand(string deviceName, string deviceNo, string deviceType)
        {
            SqlConnection myConnection = new SqlConnection(connectionString);
            if (myConnection.State != ConnectionState.Open)
            {
                myConnection.Open();
            }
            SqlCommand myCommand = new SqlCommand("ChangeDeviceMapping", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            myCommand.Parameters.Add("@deviceName", SqlDbType.VarChar);
            myCommand.Parameters["@deviceName"].Value = deviceName;
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
