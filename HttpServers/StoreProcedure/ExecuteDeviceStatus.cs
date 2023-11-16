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
    public class ExecuteDeviceStatus
    {
        string connectionString = "";
        public ExecuteDeviceStatus()
        {
            connectionString = ConfigurationManager.AppSettings["ConnectionString"];
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public DataTable SelecDeviceStatusCommand(string tag,string type)
        {
            DataSet ds = new DataSet();

            SqlConnection myConnection = new SqlConnection(connectionString);
            if (myConnection.State != ConnectionState.Open)
            {
                myConnection.Open();
            }
            SqlCommand myCommand = new SqlCommand("SelectDeviceStatus", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.Add("@tag", SqlDbType.VarChar);
            myCommand.Parameters["@tag"].Value = tag;
            myCommand.Parameters.Add("@type", SqlDbType.VarChar);
            myCommand.Parameters["@type"].Value = type;

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
    }
}
