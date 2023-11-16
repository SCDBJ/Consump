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
    public class ExecuteGetCompany
    {
        string connectionString = "";
        public ExecuteGetCompany()
        {
            connectionString = ConfigurationManager.AppSettings["ConnectionString"];
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public DataTable SelecCompanyCommand()
        {
            DataSet ds = new DataSet();

            SqlConnection myConnection = new SqlConnection(connectionString);
            if (myConnection.State != ConnectionState.Open)
            {
                myConnection.Open();
            }
            SqlCommand myCommand = new SqlCommand("GetCompany", myConnection);
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
        public string InsertCompanyCommand(string companyName,string contact,string phone)
        {
            SqlConnection myConnection = new SqlConnection(connectionString);
            if (myConnection.State != ConnectionState.Open)
            {
                myConnection.Open();
            }
            SqlCommand myCommand = new SqlCommand("AddCompany", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            myCommand.Parameters.Add("@companyName", SqlDbType.VarChar);
            myCommand.Parameters["@companyName"].Value = companyName;
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
        public string DisableCompanyCommand(string companyName,string type)
        {
            SqlConnection myConnection = new SqlConnection(connectionString);
            if (myConnection.State != ConnectionState.Open)
            {
                myConnection.Open();
            }
            SqlCommand myCommand = new SqlCommand("DisableCompany", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            myCommand.Parameters.Add("@companyName", SqlDbType.VarChar);
            myCommand.Parameters["@companyName"].Value = companyName;
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
        public string ChangeCompanyCommand(string companyName, string contact, string phone, string oldCompanyName)
        {
            SqlConnection myConnection = new SqlConnection(connectionString);
            if (myConnection.State != ConnectionState.Open)
            {
                myConnection.Open();
            }
            SqlCommand myCommand = new SqlCommand("ChangeCompany", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            myCommand.Parameters.Add("@companyName", SqlDbType.VarChar);
            myCommand.Parameters["@companyName"].Value = companyName;
            myCommand.Parameters.Add("@contact", SqlDbType.VarChar);
            myCommand.Parameters["@contact"].Value = contact;
            myCommand.Parameters.Add("@phone", SqlDbType.VarChar);
            myCommand.Parameters["@phone"].Value = phone;
            myCommand.Parameters.Add("@oldCompanyName", SqlDbType.VarChar);
            myCommand.Parameters["@oldCompanyName"].Value = oldCompanyName;

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
