using HttpServers.Model.Salary;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpServers.Model;

namespace HttpServers.StoreProcedure
{
    public class ExecuteSalary
    {
        string connectionString;
        public ExecuteSalary()
        {
            connectionString = ConfigurationManager.AppSettings["ConnectionString"];
        }
        public int AddSalaryRecordCommand(SalaryItem salaryItem)
        {
            SqlConnection myConnection = new SqlConnection(connectionString);
            if (myConnection.State != ConnectionState.Open)
            {
                myConnection.Open();
            }
            SqlCommand myCommand = new SqlCommand("AddSalaryRecord", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            myCommand.Parameters.Add("@datacyear", SqlDbType.Int);
            myCommand.Parameters["@datacyear"].Value = salaryItem.datacyear;
            myCommand.Parameters.Add("@datacperiod", SqlDbType.NVarChar);
            myCommand.Parameters["@datacperiod"].Value = salaryItem.datacperiod;
            myCommand.Parameters.Add("@dataf_32", SqlDbType.Decimal);
            myCommand.Parameters["@dataf_32"].Value = salaryItem.dataf_32;
            myCommand.Parameters.Add("@dataf_131", SqlDbType.Float);
            myCommand.Parameters["@dataf_131"].Value = salaryItem.dataf_131;
            myCommand.Parameters.Add("@dataf_134", SqlDbType.Float);
            myCommand.Parameters["@dataf_134"].Value = salaryItem.dataf_134;
            myCommand.Parameters.Add("@dataf_40", SqlDbType.Decimal);
            myCommand.Parameters["@dataf_40"].Value = salaryItem.dataf_40;
            myCommand.Parameters.Add("@dataf_94", SqlDbType.Decimal);
            myCommand.Parameters["@dataf_94"].Value = salaryItem.dataf_94;
            myCommand.Parameters.Add("@dataf_95", SqlDbType.Decimal);
            myCommand.Parameters["@dataf_95"].Value = salaryItem.dataf_95;
            myCommand.Parameters.Add("@dataf_96", SqlDbType.Decimal);
            myCommand.Parameters["@dataf_96"].Value = salaryItem.dataf_96;
            myCommand.Parameters.Add("@dataf_97", SqlDbType.NVarChar);
            myCommand.Parameters["@dataf_97"].Value = salaryItem.dataf_97;
            myCommand.Parameters.Add("@dataf_63", SqlDbType.Decimal);
            myCommand.Parameters["@dataf_63"].Value = salaryItem.dataf_63;
            myCommand.Parameters.Add("@dataf_79", SqlDbType.Decimal);
            myCommand.Parameters["@dataf_79"].Value = salaryItem.dataf_79;
            myCommand.Parameters.Add("@dataf_158", SqlDbType.Decimal);
            myCommand.Parameters["@dataf_158"].Value = salaryItem.dataf_158;
            myCommand.Parameters.Add("@dataf_159", SqlDbType.Decimal);
            myCommand.Parameters["@dataf_159"].Value = salaryItem.dataf_159;
            myCommand.Parameters.Add("@dataf_5", SqlDbType.Decimal);
            myCommand.Parameters["@dataf_5"].Value = salaryItem.dataf_5;
            myCommand.Parameters.Add("@dataf_3", SqlDbType.Decimal);
            myCommand.Parameters["@dataf_3"].Value = salaryItem.dataf_3;
            myCommand.Parameters.Add("@dataf_157", SqlDbType.Decimal);
            myCommand.Parameters["@dataf_157"].Value = salaryItem.dataf_157;
            myCommand.Parameters.Add("@dataf_162", SqlDbType.Decimal);
            myCommand.Parameters["@dataf_162"].Value = salaryItem.dataf_162;
            myCommand.Parameters.Add("@dataf_163", SqlDbType.Decimal);
            myCommand.Parameters["@dataf_163"].Value = salaryItem.dataf_163;

            int resultValue = myCommand.ExecuteNonQuery();
            if (myConnection.State == ConnectionState.Open)
            {
                myConnection.Close();
            }
            return resultValue;
        }
        public string GetSalaryDateRecordCommand(int? datacyear, string? datacperiod)
        {
            DataSet ds = new DataSet();

            SqlConnection myConnection = new SqlConnection(connectionString);
            if (myConnection.State != ConnectionState.Open)
            {
                myConnection.Open();
            }
            SqlCommand myCommand = new SqlCommand("GetSalaryDateRecord", myConnection);
            myCommand.Parameters.Add("@datacyear", SqlDbType.Int);
            myCommand.Parameters["@datacyear"].Value = datacyear;
            myCommand.Parameters.Add("@datacperiod", SqlDbType.NVarChar);
            myCommand.Parameters["@datacperiod"].Value = datacperiod;
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.ExecuteNonQuery();

            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);
            adapter.Fill(ds);
            if (myConnection.State == ConnectionState.Open)
            {
                myConnection.Close();
            }
            if (ds.Tables.Count > 0&& ds.Tables[0].Rows.Count>0)
            {
                return "1";
            }
            return "0";
        }
        public string GetAllSalaryRecordCommand()
        {
            DataSet ds = new DataSet();

            SqlConnection myConnection = new SqlConnection(connectionString);
            if (myConnection.State != ConnectionState.Open)
            {
                myConnection.Open();
            }
            SqlCommand myCommand = new SqlCommand("GetAllSalaryRecord", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.ExecuteNonQuery();

            SqlDataReader adapter = myCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(adapter);
            List<SalaryItem> list = new List<SalaryItem>();
            if (dt.Rows.Count > 0)
            {
                SalaryItem salaryItem;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    salaryItem = new SalaryItem { datacyear = int.Parse(dt.Rows[i]["datacyear"].ToString()), datacperiod = dt.Rows[i]["datacperiod"].ToString(), dataf_131 = double.Parse(dt.Rows[i]["dataf_131"].ToString()), dataf_134 = double.Parse(dt.Rows[i]["dataf_134"].ToString()), dataf_157 = decimal.Parse(dt.Rows[i]["dataf_157"].ToString()), dataf_158 = decimal.Parse(dt.Rows[i]["dataf_158"].ToString()), dataf_159 = decimal.Parse(dt.Rows[i]["dataf_159"].ToString()), dataf_162 = decimal.Parse(dt.Rows[i]["dataf_162"].ToString()), dataf_163 = decimal.Parse(dt.Rows[i]["dataf_163"].ToString()), dataf_3 = decimal.Parse(dt.Rows[i]["dataf_3"].ToString()), dataf_32 = decimal.Parse(dt.Rows[i]["dataf_32"].ToString()), dataf_40 = decimal.Parse(dt.Rows[i]["dataf_40"].ToString()), dataf_5 = decimal.Parse(dt.Rows[i]["dataf_5"].ToString()), dataf_63 = decimal.Parse(dt.Rows[i]["dataf_63"].ToString()), dataf_79 = decimal.Parse(dt.Rows[i]["dataf_79"].ToString()), dataf_94 = decimal.Parse(dt.Rows[i]["dataf_94"].ToString()), dataf_95 = decimal.Parse(dt.Rows[i]["dataf_95"].ToString()), dataf_96 = decimal.Parse(dt.Rows[i]["dataf_96"].ToString()), dataf_97 = dt.Rows[i]["dataf_97"].ToString(), dataf_164 = decimal.Parse(dt.Rows[i]["dataf_159"].ToString()) + decimal.Parse(dt.Rows[i]["dataf_162"].ToString()) };
                    list.Add(salaryItem);
                }
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(list);
                return json;
            }
            if (myConnection.State == ConnectionState.Open)
            {
                myConnection.Close();
            }

            return null;
        }
    }
}
