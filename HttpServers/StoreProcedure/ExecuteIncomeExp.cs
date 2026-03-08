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
    public class ExecuteIncomeExp
    {
        string connectionString;
        public ExecuteIncomeExp()
        {
            connectionString = ConfigurationManager.AppSettings["ConnectionString"];
        }
        public string GetIncomeExpMonthCommand(int incomeExpYear)
        {
            SqlConnection myConnection = new SqlConnection(connectionString);
            if (myConnection.State != ConnectionState.Open)
            {
                myConnection.Open();
            }
            SqlCommand myCommand = new SqlCommand("GetIncomeExpMonth", myConnection);
            myCommand.Parameters.Add("@incomeExpYear", SqlDbType.Int);
            myCommand.Parameters["@incomeExpYear"].Value = incomeExpYear;
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.ExecuteNonQuery();

            SqlDataReader sqlDataReader = myCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(sqlDataReader);
            List<IncomeExpMonth> list = new List<IncomeExpMonth>();
            if (dt.Rows.Count > 0)
            {
                IncomeExpMonth incomeExpMonth;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    incomeExpMonth = new IncomeExpMonth { fdate = dt.Rows[i]["fdate"].ToString(),  surplusAmount = decimal.Parse(dt.Rows[i]["surplusAmount"].ToString()) };
                    list.Add(incomeExpMonth);
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
        public string GetIncomeExpYearCommand()
        {
            SqlConnection myConnection = new SqlConnection(connectionString);
            if (myConnection.State != ConnectionState.Open)
            {
                myConnection.Open();
            }
            SqlCommand myCommand = new SqlCommand("GetIncomeExpYear", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.ExecuteNonQuery();

            SqlDataReader sqlDataReader = myCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(sqlDataReader);
            List<IncomeExpYear> list = new List<IncomeExpYear>();
            if (dt.Rows.Count > 0)
            {
                IncomeExpYear incomeExpYear;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    incomeExpYear = new IncomeExpYear { fyear = dt.Rows[i]["fyear"].ToString(), surplusAmount = decimal.Parse(dt.Rows[i]["surplusAmount"].ToString()) };
                    list.Add(incomeExpYear);
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
