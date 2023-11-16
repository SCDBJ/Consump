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
    public class ExecuteDeviceInfo
    {
        string connectionString = "";
        public ExecuteDeviceInfo()
        {
            connectionString = ConfigurationManager.AppSettings["ConnectionString"];
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public DataTable SelecDeviceInfoCommand()
        {
            DataSet ds = new DataSet();

            SqlConnection myConnection = new SqlConnection(connectionString);
            if (myConnection.State != ConnectionState.Open)
            {
                myConnection.Open();
            }
            SqlCommand myCommand = new SqlCommand("GetDeviceInfo", myConnection);
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
        public string UpdateDeviceInfoCommand(string deviceNo, string deviceType,string deviceStatus)
        {
            SqlConnection myConnection = new SqlConnection(connectionString);
            if (myConnection.State != ConnectionState.Open)
            {
                myConnection.Open();
            }
            SqlCommand myCommand = new SqlCommand("UpdateDeviceInfo", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            myCommand.Parameters.Add("@deviceNo", SqlDbType.VarChar);
            myCommand.Parameters["@deviceNo"].Value = deviceNo;
            myCommand.Parameters.Add("@deviceType", SqlDbType.VarChar);
            myCommand.Parameters["@deviceType"].Value = deviceType;
            myCommand.Parameters.Add("@deviceStatus", SqlDbType.VarChar);
            myCommand.Parameters["@deviceStatus"].Value = deviceStatus;

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
