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
    public class ExecuteSetPrice
    {
        string connectionString = "";
        public ExecuteSetPrice()
        {
            connectionString = ConfigurationManager.AppSettings["ConnectionString"];
            
        }
        public string SetPriceCommand(string waterPrice,string electricPrice,string companys,string effectTime, string endTime, string objectType)
        {
            SqlConnection myConnection = new SqlConnection(connectionString);
            if (myConnection.State != ConnectionState.Open)
            {
                myConnection.Open();
            }
            SqlCommand myCommand = new SqlCommand("SetPrice", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            myCommand.Parameters.Add("@waterPrice", SqlDbType.VarChar);
            myCommand.Parameters["@waterPrice"].Value = waterPrice;
            myCommand.Parameters.Add("@electricPrice", SqlDbType.VarChar);
            myCommand.Parameters["@electricPrice"].Value = electricPrice;
            myCommand.Parameters.Add("@companys", SqlDbType.VarChar);
            myCommand.Parameters["@companys"].Value = companys;
            myCommand.Parameters.Add("@effectTime", SqlDbType.VarChar);
            myCommand.Parameters["@effectTime"].Value = effectTime;
            myCommand.Parameters.Add("@endTime", SqlDbType.VarChar);
            myCommand.Parameters["@endTime"].Value = endTime;
            myCommand.Parameters.Add("@type", SqlDbType.VarChar);
            myCommand.Parameters["@type"].Value = objectType;

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
        public string UpdatePriceCommand(string waterPrice, string electricPrice, string companys, string effectTime, string endTime, string objectType,string ID)
        {
            SqlConnection myConnection = new SqlConnection(connectionString);
            if (myConnection.State != ConnectionState.Open)
            {
                myConnection.Open();
            }
            SqlCommand myCommand = new SqlCommand("UpdatePrice", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            myCommand.Parameters.Add("@waterPrice", SqlDbType.VarChar);
            myCommand.Parameters["@waterPrice"].Value = waterPrice;
            myCommand.Parameters.Add("@electricPrice", SqlDbType.VarChar);
            myCommand.Parameters["@electricPrice"].Value = electricPrice;
            myCommand.Parameters.Add("@companys", SqlDbType.VarChar);
            myCommand.Parameters["@companys"].Value = companys;
            myCommand.Parameters.Add("@effectTime", SqlDbType.VarChar);
            myCommand.Parameters["@effectTime"].Value = effectTime;
            myCommand.Parameters.Add("@endTime", SqlDbType.VarChar);
            myCommand.Parameters["@endTime"].Value = endTime;
            myCommand.Parameters.Add("@type", SqlDbType.VarChar);
            myCommand.Parameters["@type"].Value = objectType;
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
        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public DataTable GetSetPriceCommand(string type)
        {
            DataSet ds = new DataSet();

            SqlConnection myConnection = new SqlConnection(connectionString);
            if (myConnection.State != ConnectionState.Open)
            {
                myConnection.Open();
            }
            SqlCommand myCommand = new SqlCommand("GetSetPrice", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
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
